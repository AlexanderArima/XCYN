using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.AsyncDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 同步调用，UI界面会卡住
            this.textBox1.Text = "查询开始\r\n" + this.textBox1.Text;
            Query();
            this.textBox1.Text =  "查询结束\r\n" + this.textBox1.Text;
        }

        private List<string> Query()
        {
            Thread.Sleep(5000);
            return null;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // 异步调用，调用了有async的异步方法，为了让它实现异步调用，需使用await关键字，并且方法自身
            // 添加async关键字修饰
            this.textBox1.Text = "查询开始\r\n" + this.textBox1.Text;
            var result = await QueryAsync();
            this.textBox1.Text = "查询结束\r\n" + this.textBox1.Text;
        }

        private async Task<List<string>> QueryAsync()
        {
            // 异步调用，Task.Delay返回Task，那么就可以使用await关键字调用，这样就是调用异步方法了
            // 调用异步方法的代价就是，需要将方法自身添加async关键字，并在方法的后缀添加Async标记
            await Task.Delay(5000);
            return null;
        }
    }
}
