using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform
{
    public partial class Spider : Form
    {
        public Spider()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = textBox1.Text;
            SpiderWebSite(url);
        }

        private void SpiderWebSite(string url)
        {
            var watch = new Stopwatch();
            watch.Start();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*";
            request.ContentType = "text/html;charset=UTF-8";//定义文档类型及编码
            request.AllowAutoRedirect = false;//禁止自动跳转
            //设置User-agent，伪装成QQ浏览器
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.4549.400 QQBrowser/9.7.12900.400";
            request.Timeout = 5000;
            request.KeepAlive = true;
        }
    }
}
