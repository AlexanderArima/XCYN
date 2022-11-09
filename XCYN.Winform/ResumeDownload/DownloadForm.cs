using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common;
using XCYN.Common.ResumeDownload;

namespace XCYN.Winform.ResumeDownload
{
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputURLForm form = new InputURLForm();
            form.GetURLAction += str =>
            {
                this.PrintLog(string.Format("开始下载，下载地址：{0}", str));

                // 下载到本地
                DownloadTask(str, PathHelper.ApplicationPath + @"ResumeDownload\Test.exe");

            };
            if(form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        /// <summary>
        /// 启动一个线程下载文件.s
        /// </summary>
        /// <param name="link1">文件远程下载地址.</param>
        /// <param name="savePathFile">下载文件的本地路径.</param>
        private void DownloadTask(string link1, string savePathFile)
        {
            Task t = new Task(() => {
                var info1 = HttpUtilWrapper.GetRemoteLinkDownloadInfo(link1);
                var downInfo1 = new RemoteLinkDownloadWithLinkInfo(info1, link1);
                var downloader = new HttpStableDownloader(link1, savePathFile, downInfo1.eTag, downInfo1.filesize);
                if (downInfo1.supportResumeDownload)
                {
                    downloader.ResumeProgressDelegate = (progress, errorCode) =>
                    {
                        PrintLog(string.Format("下载进度：{0}%，状态：{1}", progress, HttpStableDownloader.ParseDownCode(errorCode)));
                        if (HttpStableDownloader.IsResumeCodeMeansEnd(errorCode) == 1)
                        {
                            // 下载完成
                            PrintLog("下载完成");
                        }
                    };
                    downloader.ResumeDownload();
                }
                else
                {
                    downloader.NotResumeSizeChangeDelegate = (receivedKB, errorCode) =>
                    {
                        PrintLog(string.Format("接收字节数：{0}kb，返回码：{1}", receivedKB, HttpStableDownloader.ParseDownCode(errorCode)));
                        if (HttpStableDownloader.IsResumeCodeMeansEnd(errorCode) == 1)
                        {
                            // 下载完成
                            PrintLog("下载完成");
                        }
                    };
                    downloader.NotResumeDownload();
                }
            });
            t.Start();
        }

        /// <summary>
        /// 打印日志.
        /// </summary>
        /// <param name="str"></param>
        private void PrintLog(string str)
        {
            this.textBox1.Invoke(new Action(() =>
            {
                this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + " " + string.Format("{0} \r\n", str) + this.textBox1.Text;
            }));
        }

        private void DownloadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
