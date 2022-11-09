using XCYN.Common;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace XCYN.Common.ResumeDownload
{
    public class RemoteLinkDownloadInfo
    {
        public string timestampstr;
        public long timestamp;
        public long filesize;
        public string eTag;
        public string AcceptRanges;

        public override string ToString()
        {
            return "size=" + filesize + ",ts=" + timestampstr;
        }
    }

    public class RemoteLinkDownloadWithLinkInfo : RemoteLinkDownloadInfo
    {
        public string link;
        public bool isUsed;
        public int isInit;
        public bool supportResumeDownload;

        public RemoteLinkDownloadWithLinkInfo()
        {
            isInit = -1;
        }

        public RemoteLinkDownloadWithLinkInfo(RemoteLinkDownloadInfo info, string downloadlInk)
        {
            if (info != null)
            {
                timestampstr = info.timestampstr;
                timestamp = info.timestamp;
                filesize = info.filesize;
                eTag = info.eTag;
                AcceptRanges = info.AcceptRanges;
                supportResumeDownload = (info.AcceptRanges == null || filesize <= 0) ? false
                    : info.AcceptRanges.ToLower().Contains("byte");
            }

            link = downloadlInk;
            isUsed = false;
            isInit = 1;
        }

        public override string ToString()
        {
            return string.Format("size={0}, ts={1}, link={2}", filesize, timestampstr, link);
        }

        public string GetPrintTxt()
        {
            return string.Format("{0}, {1}, size({2}), suppoortResume({3})", link, eTag, filesize, supportResumeDownload);
        }
    }

    public class HttpUtilWrapper
    {
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受    
            return true;
        }

        private static bool mInitXXXBe4XXed = false;
        public static void InitServerCerValidationCbBe4CreateRequest()
        {
            if (mInitXXXBe4XXed) return;
            if (mInitXXXBe4XXed) return;
            mInitXXXBe4XXed = true;
            //处理HttpWebRequest访问https有安全证书的问题（ 请求被中止: 未能创建 SSL/TLS 安全通道。）
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true; //这行与上述那行等价,设置一个回调用于ssl验证
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            //.net3.5 SecurityProtocolType.Tls;
        }

        public static void SetCookieUnSafe(HttpWebRequest req)
        {
            //创建证书文件，此证书可能是私有网站证书的添加方式，这种共有网站使用的是系统标准的证书，不需要指定也可以访问
            //对于使用私人证书的情景，可以使用下边的方式，指定对应的证书地址
            //有人说需要使用X509Certificate2类代替X509Certificate，但是并未验证，而且需要考虑程序用户权限问题
            //X509Certificate2 objx509 = new X509Certificate2("F://1234.cer");
            //request.ClientCertificates.Add(objx509);

            //cookie部分，如果cookie中需要校验用户密码，可以按照下边的方式进行创建，访问curse的网页，只要填空就可以，验证是没问题的
            CookieContainer objcok = new CookieContainer();
            //objcok.Add(new Uri("http://testurl"), new Cookie("键", "值"));
            //objcok.Add(new Uri("http://testurl"), new Cookie("键", "值"));
            //objcok.Add(new Uri("http://testurl"), new Cookie("sidi_sessionid", "360A748941D055BEE8C960168C3D4233"));

            req.CookieContainer = objcok;
        }

        /// <summary>
        ///  下载指定文件，并返回该文件的信息，这些信息主要是用于断点续传.
        /// </summary>
        /// <param name="downloadUrl"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public static RemoteLinkDownloadInfo GetRemoteLinkDownloadInfo(string downloadUrl, int retryCount = 0)
        {
            InitServerCerValidationCbBe4CreateRequest();
            try
            {
                // 与指定URL创建HTTP请求       
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(downloadUrl);
                request.Timeout = 4 * 1000;
                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36";
                SetCookieUnSafe(request);
                // 获取对应HTTP请求的响应      
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string[] etag = null;// response.Headers.GetValues("ETag");
                string[] AcceptRangess = null;//
                int i = 0;
                foreach (var k in response.Headers.AllKeys)
                {
                    if (k == "ETag")
                    {
                        etag = response.Headers.GetValues(k);
                        i++;
                    }
                    else if (k == "Accept-Ranges")
                    {
                        AcceptRangess = response.Headers.GetValues(k);
                        i++;
                    }
                    if (i == 2)
                    {
                        break;
                    }
                }

                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                TimeSpan toNow = response.LastModified.Subtract(dtStart);
                long timeStamp = toNow.Ticks;
                timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4)) / 1000;
                var r = new RemoteLinkDownloadInfo
                {
                    AcceptRanges = AcceptRangess?[0],
                    filesize = response.ContentLength,
                    timestamp = timeStamp,
                    eTag = etag?[0].Replace("\"", ""),
                    timestampstr = response.LastModified.ToString()
                };

                request.Abort();
                response.Close();
                return r;
            }
            catch (Exception ex)
            {
                if (retryCount > 2)
                {
                    Thread.Sleep(8 * 1000);
                }
                else
                {
                    Thread.Sleep(4 * 1000);
                }

                if (retryCount <= 4)
                {
                    return GetRemoteLinkDownloadInfo(downloadUrl, ++retryCount);
                }
                else
                {
                    Log4NetHelper.Error("下载文件时出错：" + ex.Message, ex);
                }
            }

            return null;
        }
    }
}
