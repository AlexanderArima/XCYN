using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.AsyncDemo
{
    public partial class ConfigureAwaitForm : Form
    {
        public ConfigureAwaitForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // 执行一个耗时操作
            this.textBox1.Text = "Loading....";
            var str = await this.DownloadWebPage().ConfigureAwait(false);

            // 由于前面使用ConfigureAwait(false)，此时任务可能会在非UI线程中操作，这时如果直接对控件进行修改
            // 程序会出错，因为不能再非UI线程中对控件进行修改，所以要使用Invoke方法，让这次的修改发生在UI线程上
            this.Invoke(new Action(() =>
            {
                this.textBox1.Text = str;
            }));

            // 第二种方法，不进行线程切换，仍在UI线程中修改控件
            // var str = await this.DownloadWebPage();
            // this.textBox1.Text = str;
        }

        public async Task<string> DownloadWebPage()
        {
            HttpClient obj = new HttpClient();
            return await obj.GetStringAsync("http://www.github.com");
        }
    }
}
