using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.LogViewer
{
    //C#调用Js要带上这个
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class LogDetail : Form
    {

        public string Msg { get; set; }

        public LogDetail(string msg)
        {
            InitializeComponent();
            this.Msg = msg;
            string path3 = System.IO.Directory.GetCurrentDirectory();
            webBrowser1.Url = new Uri(string.Format("{0}\\Html\\LogDetail.html", path3));
            webBrowser1.ObjectForScripting = this;  //这句必须，不然js不能调用C#

            //Load事件
            HtmlDocument document = webBrowser1.Document;
            var windows = document.Window;
            windows.AttachEventHandler("onload", new EventHandler(window_load));
        }

        /// <summary>
        /// 窗体初始化加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void window_load(object sender, EventArgs e)
        {
            HtmlDocument document = webBrowser1.Document;
            document.GetElementById("main_div").InnerText = this.Msg;
        }

        private void LogDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
