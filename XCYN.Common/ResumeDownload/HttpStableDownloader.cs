
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Common.ResumeDownload
{
    public class HttpStableDownloader
    {
        /*
         * 注意！！！
         * 这个变量改成你自己愿意保存的位置。或者你自行改成数据库等存储操作，甚至是内存变量数组list保存也行
         * **/
        static string GetSaveResumeDownInfoDir => PathHelper.ApplicationPath + @"\ResumeDownload";

        /// <summary>
        /// 中文：添加Resume模式的回调，根据ResumeDownlaodInfo.supportResumeDownload = true才添加这个。
        /// En：Add your Resume Mod callback, according to ResumeDownlaodInfo.supportResumeDownload = true
        /// </summary>
        public ResumeDownloadProgressDelegate ResumeProgressDelegate { set; get; } = null;

        /// <summary>
        /// 中文：添加NotResume Size回调，根据是否支持Resume才添加这个。ResumeDownlaodInfo.supportResumeDownload = false
        /// En：Add your not Resume Mod callback, according to ResumeDownlaodInfo.supportResumeDownload = false
        /// </summary>
        public NotResumeDownloadSizeChangeDelegate NotResumeSizeChangeDelegate { set; get; } = null;

        /// <summary>
        /// 中文：添加最近7.5s内的速度回调
        /// En：Add recently 7.5s speed callback
        /// </summary>
        public SpeedMonitorDelegate CurrentSpeedDelegate { set; get; } = null;

        /// <summary>
        /// 整体下载速度监控
        /// En: full download progress speed monitor.
        /// </summary>
        public SpeedFullDelegate FullSpeedDelegate { set; get; } = null;

        private const long DEFAULT_MINI_DOWN_SPEED = 7000L;// byte/s
        private const long DEFAULT_CURRENT_SPEED_RETRY = 2500;

        /// <summary>
        /// 是否在速度过低进行取消下载
        /// En：If speed is too slow cancel this download
        /// </summary>
        public bool ConsideSpeed { set; get; } = true;
        /// <summary>
        /// 取消下载的临界值
        /// En: The mini download Speed to cancel download
        /// </summary>
        public long MiniDownloadSpeed { set; get; } = DEFAULT_MINI_DOWN_SPEED;
        public long MiniDownloadSpeedTimeout { set; get; } = 12000;

        /// <summary>
        /// 最多重试次数。因为会断掉。
        /// Because of failed, max try num.
        /// </summary>
        public int MaxTryCount { set; get; } = -1;

        #region CODE
        public static string ParseDownCode(int code)
        {
            switch (code)
            {
                case DOWN_CODE_ERROR_NO_URL:
                    return @"下载地址错误";
                case DOWN_CODE_ERROR_REMOTE_FILESIZE:
                    return @"远程下载大小信息错误";
                case DOWN_CODE_ERROR_TRY_NUM_MAX:
                    return "下载失败1，重试次数太多仍然没有成功";
                case DOWN_CODE_CONTINUE_RETRY:
                    return @"下载失败2，重试。一般作为内部使用。";
                case DOWN_CODE_ERROR_MANUAL_CANCELED:
                    return @"DOWN_CODE_ERROR_MANUAL_CANCELED";
                case DOWN_CODE_ERROR_SPEED_TOO_LOW:
                    return @"DOWN_CODE_ERROR_SPEED_TOO_LOW";
                case DOWN_CODE_SUCCESS:
                    return @"下载完成";
                case DOWN_CODE_NORMAL_FULL:
                    return @"文件已下载";
                case DOWN_CODE_PROCESSING:
                    return @"下载中";
            }
            return "" + code;
        }

        /// <summary>
        /// 下载地址错误
        /// </summary>
        public const int DOWN_CODE_ERROR_NO_URL = -1;
        /// <summary>
        /// 远程下载大小信息错误
        /// </summary>
        public const int DOWN_CODE_ERROR_REMOTE_FILESIZE = -2;
        /// <summary>
        /// 下载失败1，重试次数太多仍然没有成功
        /// </summary>
        public const int DOWN_CODE_ERROR_TRY_NUM_MAX = -3;

        /// <summary>
        /// 下载失败2，重试。一般作为内部使用。
        /// </summary>
        public const int DOWN_CODE_CONTINUE_RETRY = -4;

        public const int DOWN_CODE_ERROR_MANUAL_CANCELED = -5;

        public const int DOWN_CODE_ERROR_SPEED_TOO_LOW = -6;
        /// <summary>
        /// 下载完成
        /// </summary>
        public const int DOWN_CODE_SUCCESS = 2;
        /// <summary>
        /// 文件大小已经足够，不用下载
        /// </summary>
        public const int DOWN_CODE_NORMAL_FULL = 1;

        /// <summary>
        /// 正在下载中。。。
        /// </summary>
        public const int DOWN_CODE_PROCESSING = 0;
        #endregion
        /// <summary>
        /// 0表示正在工作；-1表示停止下来并下载失败；1表示下载停止并下载成功
        /// </summary>
        public static int IsResumeCodeMeansEnd(int code)
        {
            switch (code)
            {
                case DOWN_CODE_ERROR_NO_URL:
                case DOWN_CODE_ERROR_REMOTE_FILESIZE:
                case DOWN_CODE_ERROR_TRY_NUM_MAX:
                case DOWN_CODE_ERROR_SPEED_TOO_LOW:
                    return -1;
                case DOWN_CODE_CONTINUE_RETRY:
                case DOWN_CODE_PROCESSING:
                    return 0;
                case DOWN_CODE_SUCCESS:
                case DOWN_CODE_NORMAL_FULL:
                    return 1;
            }
            return 0;
        }

        const int BYTE_BUFF_SIZE = 2048;

        private int CalMaxTryNum(long totalsize, bool supportResm)
        {
            if (!supportResm)
            {
                return 5;
            }

            if (MaxTryCount != -1)
            {
                return MaxTryCount;
            }

            if (totalsize >= 20 * 1024 * 1024)
            {
                return 150;
            }

            if (totalsize >= 5 * 1024 * 1024)
            {
                return 100;
            }

            if (totalsize >= 3 * 1024 * 1024)
            {
                return 80;
            }

            if (totalsize >= 1024 * 1024)
            {
                return 70;
            }

            if (totalsize >= 300 * 1024)
            {
                return 50;
            }

            if (totalsize >= 100 * 1024)
            {
                return 40;
            }

            return 30;
        }

        private int CalTimeOut(int currentTryNum, bool supportResume)
        {
            if (!supportResume)
            {
                return currentTryNum > 3 ? 2800 : 2200;
            }
            //实测晚高峰github，20次即2200延迟，能够稍微动一下。

            if (currentTryNum < 8)
            {
                return 2000;
            }

            if (currentTryNum < 15)
            {
                return 2500;
            }

            return 2800;
        }

        private void MarkupSleepWhenRetry(int currentTryNum, bool supportResume)
        {
            if (!supportResume)
            {
                Thread.Sleep(5000);
                return;
            }
            if (currentTryNum < 20)
            {
                if (currentTryNum % 2 == 1)
                {
                    Thread.Sleep(3000);
                }
            }
            else
            {
                Thread.Sleep(3000);
            }
        }

        private const string TAG = " HttpDownloader: ";
#if DEBUG
        private const bool DEBUG = true;
#else
        private const bool DEBUG = false;
#endif

        private readonly string _url, _savepath, _etag;
        private readonly long _filesize;
        //private DownloadSpeedHelper _speedHelper;

        public HttpStableDownloader(string url, string savepath, string etag, long filesize)
        {
            _url = url;
            _savepath = savepath;
            _etag = etag;
            _filesize = filesize;
        }

        public delegate void ResumeDownloadProgressDelegate(int progress, int code);
        public delegate void NotResumeDownloadSizeChangeDelegate(long receivedKB, int code);
        public delegate void SpeedMonitorDelegate(float bytesPerSecond);
        public delegate void SpeedFullDelegate(long filesize, long totalDownloadAddSize, long totalTime);

        private static string GenerateLines(int num)
        {
            string w = " |";
            for (int i = 0; i < num - 1; i++)
            {
                w += "|";
            }
            return w;
        }

        public void NotResumeDownloadAsync()
        {
            Task t = new Task(() => {
                NotResumeDownload();
            });

            t.Start();
        }

        public void ResumeDownloadAsync()
        {
            Task t = new Task(() => {
                ResumeDownload();
            });

            t.Start();
        }

        public int NotResumeDownload()
        {
            if (_url == null || _url.Length <= 2)
            {
                NotResumeSizeChangeDelegate?.Invoke(-1, DOWN_CODE_ERROR_NO_URL);
                return DOWN_CODE_ERROR_NO_URL;
            }
            int r;
            string MagicCode = Guid.NewGuid().ToString().Substring(0, 6);
            int curTryNum = 1;
            do
            {
                r = DownloadInThreadInner(false, MagicCode, curTryNum);
                curTryNum++;
            } while (r == DOWN_CODE_CONTINUE_RETRY);
            return r;
        }

        public int ResumeDownload()
        {
            if (_url == null || _url.Length <= 2)
            {
                ResumeProgressDelegate?.Invoke(-1, DOWN_CODE_ERROR_NO_URL);
                return DOWN_CODE_ERROR_NO_URL;
            }

            if (_filesize <= 0)
            {
                ResumeProgressDelegate?.Invoke(-1, DOWN_CODE_ERROR_REMOTE_FILESIZE);
                return DOWN_CODE_ERROR_REMOTE_FILESIZE;
            }

            int r;
            string MagicCode = Guid.NewGuid().ToString().Substring(0, 6);
            int curTryNum = 1;
            do
            {
                r = DownloadInThreadInner(true, MagicCode, curTryNum);
                curTryNum++;
            } while (r == DOWN_CODE_CONTINUE_RETRY);
            return r;
        }

        private long _initTime, _totalAddFileSize = 0;

        /// <summary>
        /// 下载文件（同步）  支持断点续传
        /// 必须在外部判断这个网址支持accept range。并传入size
        /// </summary>
        /// <param name="url">文件url</param>
        /// <param name="savepath">本地保存路径</param>
        /// <param name="filesize">下载文件大小</param>
        /// <param name="progress">下载进度（百分比）</param>
        private int DownloadInThreadInner(bool supportResume, string magicCod, int tryNum)
        {
            string debugLines = null;
            string logtag = null;
            string logtagAndLine = null;
            if (tryNum == 1) //全局速度监控
            {
                _initTime = GetTimestampM();
            }

            if (!supportResume)
            {
                MiniDownloadSpeed = DEFAULT_MINI_DOWN_SPEED;
            }

            if (DEBUG)
            {
                debugLines = GenerateLines(tryNum);
                logtag = magicCod + "[" + supportResume + "]-" + TAG;
                logtagAndLine = logtag + debugLines;

                if (tryNum == 1)
                {
                    Debug.WriteLine(logtag + "「resume    url " + _url);
                    Debug.WriteLine(logtagAndLine + "savePath " + _savepath);
                    Debug.WriteLine(logtagAndLine + "filesize " + _filesize);
                    Debug.WriteLine(logtagAndLine + "maxtryNum " + CalMaxTryNum(_filesize, supportResume));
                }
                else
                {
                    Debug.WriteLine(logtag + "notResume url " + _url);
                    Debug.WriteLine(logtagAndLine + "savePath " + _savepath);
                    Debug.WriteLine(logtagAndLine + "filesize " + _filesize);
                }
            }

            HttpUtilWrapper.InitServerCerValidationCbBe4CreateRequest();
            //打开上次下载的文件
            long lStartPos = 0;
            FileStream fs = null;
            bool newfile = true;

            if (File.Exists(_savepath))
            {
                //第一次进来就有size，说明是之前的了。要进行判断，而直接来的是循环，则不用
                if ((!supportResume) || (tryNum == 1 && !Data_CanItResume(_savepath, _filesize, _etag, _url)))
                {
                    File.Delete(_savepath);
                }
                else
                {
                    newfile = false;
                    fs = File.OpenWrite(_savepath);
                    lStartPos = fs.Length;
                    if (DEBUG) Debug.WriteLine(logtagAndLine + "已经存在 " + lStartPos);
                    fs.Seek(lStartPos, SeekOrigin.Current);//移动文件流中的当前指针
                }
            }


            if (tryNum == 1 && supportResume)
            { //else表示文件不存在；刚刚开始下载。我们就存储信息
                Data_SaveResumeInfo(_savepath, _filesize, _etag, _url);
            }

            if (newfile)
            {
                string direName = Path.GetDirectoryName(_savepath);
                if (!Directory.Exists(direName))//如果不存在保存文件夹路径，新建文件夹
                {
                    Directory.CreateDirectory(direName);
                }
                fs = new FileStream(_savepath, FileMode.Create);
                lStartPos = 0;
                if (DEBUG) Debug.WriteLine(logtagAndLine + "新创建");
            }

            if (_filesize > 0 && supportResume)
            {
                ResumeProgressDelegate?.Invoke((int)(lStartPos * 100 / _filesize), DOWN_CODE_PROCESSING);
            }

            HttpWebRequest request = null;
            long totalSize = 0;
            long curSize = 0;
            try
            {
                if (_filesize == lStartPos && supportResume)
                {
                    //下载完成
                    if (DEBUG) Debug.WriteLine(logtagAndLine + "不用下载咯！");
                    fs.Close();
                    ResumeProgressDelegate?.Invoke(100, DOWN_CODE_NORMAL_FULL);
                    Data_DeleteResumeInfo(_etag, _savepath);
                    FullSpeedDelegate?.Invoke(_filesize, 0, GetTimestampM() - _initTime);
                    return DOWN_CODE_NORMAL_FULL;
                }

                MarkupSleepWhenRetry(tryNum, supportResume); //**补偿休眠。防止过于频繁请求。被拒绝。

                request = (HttpWebRequest)WebRequest.Create(_url);
                var to = CalTimeOut(tryNum, supportResume); //**动态的超时时间；越重试次数越多越延迟高
                request.Timeout = to;
                request.ReadWriteTimeout = to;
                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36";

                if (!supportResume) HttpUtilWrapper.SetCookieUnSafe(request);

                if (lStartPos > 0)
                {
                    request.AddRange((int)lStartPos);//设置Range值，断点续传
                }

                //向服务器请求，获得服务器回应数据流
                WebResponse respone = request.GetResponse();
                totalSize = respone.ContentLength + (supportResume ? lStartPos : 0);
                if (totalSize == 0 && supportResume)
                {
                    Debug.WriteLine(logtag + " 错误！ totalsize为0");
                    ResumeProgressDelegate?.Invoke(-1, DOWN_CODE_ERROR_REMOTE_FILESIZE);
                    FullSpeedDelegate?.Invoke(_filesize, 0, GetTimestampM() - _initTime);
                    return DOWN_CODE_ERROR_REMOTE_FILESIZE;
                }
                long eachPerSize = totalSize / 240;
                if (eachPerSize <= 0)
                {
                    eachPerSize = 1024;
                    if (totalSize == -1)
                    {
                        totalSize = _filesize;
                    }
                }

                long eachPerSizeMach = 0;

                if (DEBUG && supportResume) Debug.WriteLine(logtagAndLine + "totalSize:" + totalSize + " content length: " + (respone.ContentLength));
                curSize = lStartPos;
                if (DEBUG && supportResume) Debug.WriteLine(logtagAndLine + "curSize:" + curSize + " alreadyDown percent:" + (int)(curSize * 100 / totalSize));
                //progress = (int)(curSize / totalSize * 100);
                if (supportResume) ResumeProgressDelegate?.Invoke((int)(curSize * 100 / totalSize), DOWN_CODE_PROCESSING);
                else NotResumeSizeChangeDelegate?.Invoke(0, DOWN_CODE_PROCESSING);
                Stream ns = respone.GetResponseStream();

                byte[] nbytes = new byte[BYTE_BUFF_SIZE];
                int nReadSize = 0;

                int inWhileCount = 0;
                long bytesReceived = 0; //总共接受的字节

                long lastTimeMs = GetTimestampM();
                long firstTimeMs = lastTimeMs;
                long curTimeMs, deltaTs;
                long AddedFileSize = 0L;

                if (!supportResume)
                {
                    _totalAddFileSize = 0;
                }

                long lastCurSize = lStartPos;

                while (true)
                {
                    nReadSize = ns.Read(nbytes, 0, BYTE_BUFF_SIZE);
                    if (nReadSize <= 0)
                    {
                        break;
                    }

                    curSize += nReadSize;
                    eachPerSizeMach += nReadSize;
                    bytesReceived += nReadSize;
                    fs.Write(nbytes, 0, nReadSize);

                    AddedFileSize += nReadSize;
                    _totalAddFileSize += nReadSize;

                    curTimeMs = GetTimestampM();
                    deltaTs = curTimeMs - firstTimeMs;
                    //Debug.WriteLine("deltats " + deltaTs + " AddedFileSize " + AddedFileSize + " speed= " + speed);
                    if (deltaTs > MiniDownloadSpeedTimeout)
                    {//计算速度
                        var sp = (AddedFileSize * 1000 / deltaTs);
                        if (AddedFileSize < 300 ||
                            (MiniDownloadSpeed > 0L && sp < MiniDownloadSpeed))
                        {
                            throw new Exception("SpeedError"); //速度不够停止。
                        }
                    }

                    if (eachPerSizeMach > eachPerSize)
                    {
                        eachPerSizeMach = 0;
                        if (inWhileCount++ % 2 == 0)
                        {
                            if (supportResume) ResumeProgressDelegate?.Invoke((int)(curSize * 100 / totalSize), DOWN_CODE_PROCESSING);
                            if (!supportResume) NotResumeSizeChangeDelegate?.Invoke(curSize / 1024, DOWN_CODE_PROCESSING);
                        }
                    }

                    if (curTimeMs > lastTimeMs + 7500)
                    {
                        //增加当前速度监控
                        var currentSpeed = (curSize - lastCurSize) * 1000 / (curTimeMs - lastTimeMs);
                        CurrentSpeedDelegate?.Invoke(currentSpeed / 1024);

                        lastCurSize = curSize;
                        lastTimeMs = curTimeMs;
                        Debug.WriteLine(logtagAndLine + "curSpeedKB(" + (currentSpeed / 1024) + ")cur/total/filesize(" + curSize + "/" + totalSize + "/" + _filesize + ")");

                        if (deltaTs > MiniDownloadSpeedTimeout &&
                            (AddedFileSize < 300 || currentSpeed < DEFAULT_CURRENT_SPEED_RETRY))
                        {//计算速度
                            if (ConsideSpeed)
                            {
                                Debug.WriteLine("》》SpeedRetry重试！");
                                throw new Exception("SpeedRetry"); //速度不够重试。
                            }
                        }
                    }
                }
                fs.Flush();
                ns.Close();
                fs.Close();
                if (curSize != totalSize && supportResume)//文件长度不等于下载长度，下载出错
                {
                    throw new Exception();
                }

                curSize = -1; //完成了！标记为-1
                if (request != null)
                {
                    request.Abort();
                }
                if (supportResume) ResumeProgressDelegate?.Invoke(100, DOWN_CODE_SUCCESS);
                if (!supportResume) NotResumeSizeChangeDelegate?.Invoke(curSize / 1024, DOWN_CODE_SUCCESS);
                if (DEBUG) Debug.WriteLine(logtag + " 」下载正确完成!!!");
                if (supportResume) Data_DeleteResumeInfo(_etag, _savepath);

                FullSpeedDelegate?.Invoke(_filesize, _totalAddFileSize, GetTimestampM() - _initTime);
                return DOWN_CODE_SUCCESS;
            }
            catch (Exception e)
            {
                bool speedError = e.Message == "SpeedError";
                bool speedRetry = e.Message == "SpeedRetry";

                if (DEBUG) Debug.WriteLine(logtagAndLine + "下载失败！！！！" + e);
                if (request != null)
                {
                    request.Abort();
                }

                fs.Close();

                if (speedError || tryNum > CalMaxTryNum(_filesize, supportResume))
                {
                    if (DEBUG && speedError) Debug.WriteLine(logtag + " 」重试次数已经超标咯！");
                    var code = speedError ? DOWN_CODE_ERROR_SPEED_TOO_LOW : DOWN_CODE_ERROR_TRY_NUM_MAX;
                    if (speedRetry)
                    {
                        code = DOWN_CODE_CONTINUE_RETRY;
                    }
                    if (totalSize > 0 && supportResume) ResumeProgressDelegate?.Invoke((int)(curSize * 100 / totalSize), code);
                    if (!supportResume) NotResumeSizeChangeDelegate?.Invoke(-1, code);

                    FullSpeedDelegate?.Invoke(_filesize, _totalAddFileSize, GetTimestampM() - _initTime);
                    return code;
                }
                else
                {
                    tryNum++;
                    if (DEBUG) Debug.WriteLine(logtagAndLine + "重试次数( " + tryNum);
                }

                if (totalSize > 0 && supportResume) ResumeProgressDelegate?.Invoke((int)(curSize * 100 / totalSize), DOWN_CODE_CONTINUE_RETRY);
                if (!supportResume) NotResumeSizeChangeDelegate?.Invoke(-1, DOWN_CODE_CONTINUE_RETRY);
                return DOWN_CODE_CONTINUE_RETRY;
            }
        }

        private string FormatSize(long bytes)
        {
            if (bytes >= 1024)
            {
                return (bytes / 1024 + "KB");
            }
            else
            {
                return "" + bytes;
            }
        }

        /// <summary>
        /// startpos指的是之前没有下载完cache掉的文件大小
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filesize"></param>
        /// <param name="etag"></param>
        private static void Data_SaveResumeInfo(string lastFilePath, long lastFileSize, string etag, string link)
        {
            if (etag == null || etag.Length == 0)
            {
                return;
            }
            var dir = GetSaveResumeDownInfoDir;
            var file = Path.Combine(dir, "inf_" + Path.GetFileNameWithoutExtension(lastFilePath));
            File.WriteAllText(file, lastFilePath + "\n" + lastFileSize + "\n" + link + "\n" + etag);
            Debug.WriteLine(TAG + "DATA: Save ResumeData." + lastFilePath);
        }

        private static void Data_DeleteResumeInfo(string etag, string savepath)
        {
            if (etag == null || etag.Length == 0)
            {
                return;
            }
            var dir = GetSaveResumeDownInfoDir;
            var file = Path.Combine(dir, "inf_" + Path.GetFileNameWithoutExtension(savepath));
            if (File.Exists(file)) File.Delete(file);
            Debug.WriteLine(TAG + "DATA: Delete ResumeData." + savepath);
        }

        private static bool Data_CanItResume(string newFilePath, long newTotalFileSize, string newEtg, string newLink)
        {
            if (newEtg == null || newEtg.Length == 0)
            {
                return false;
            }
            var dir = GetSaveResumeDownInfoDir;
            var file = Path.Combine(dir, "inf_" + Path.GetFileNameWithoutExtension(newFilePath));
            if (!File.Exists(file))
            {
                Debug.WriteLine(TAG + "DATA: can it resume false 1! " + newFilePath);
                return false;
            }

            var lines = File.ReadAllLines(file);
            if (lines != null && lines.Length >= 4)
            {
                string oldFilePath = lines[0];
                long.TryParse(lines[1], out long oldfilesize);
                string oldLink = lines[2];
                string oldEtag = lines[3];
                if (oldFilePath == newFilePath
                    && oldfilesize == newTotalFileSize
                    && newLink == oldLink
                    && oldEtag == newEtg)
                {
                    Debug.WriteLine(TAG + "DATA: can it resume true! " + newFilePath);
                    return true;
                }
            }
            Debug.WriteLine(TAG + "DATA: can it resume false 2! " + newFilePath);
            return false;
        }

        static long GetTimestampM()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1);
            return (long)ts.TotalMilliseconds;
        }
    }
}
