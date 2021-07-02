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

namespace XCYN.Winform.Demo
{
    /// <summary>
    /// 使用Async和Await执行异步方法的Demo
    /// </summary>
    public partial class AsyncAndAwaitForm : Form
    {

        private int count = 0;

        public AsyncAndAwaitForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        public async Task Refresh()
        {
            // 执行以下两个操作
            var t1 = GetTitleAsync();
            var t2 = GetNowTimeAsync();

            // 异步等待两个方法的完成
            var title = await t1;
            var time = await t2;

            // 更新用户界面
            label1.Text = title;
            label2.Text = time;
        }

        public Task<string> GetTitleAsync()
        {
            var task = Task.Run(() => {
                count++;
                Thread.Sleep(3000);
                return count.ToString();
            });

            return task;
        }

        public Task<string> GetNowTimeAsync()
        {
            var task = Task.Run(() => {
                count++;
                Thread.Sleep(6000);
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            });

            return task;
            return new Task<string>(() =>
            {
                // 模拟执行一个耗时6s的操作
                Thread.Sleep(6000);
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            });
        }

        //public async Task<string> GetTitleAsync()
        //{
        //    Thread.Sleep(3000);
        //    //return new Task<string>(() => {
        //        //// 模拟执行一个耗时3s的操作
        //    return "标题1";
        //    //});
        //}

        //public async Task<string> GetNowTimeAsync()
        //{
        //    //return new Task<string>(() => {
        //        //// 模拟执行一个耗时6s的操作
        //        Thread.Sleep(6000);
        //        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    //});
        //}
    }
}
