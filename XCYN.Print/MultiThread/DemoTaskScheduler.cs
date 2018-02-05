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

namespace XCYN.Print.MultiThread
{
    public partial class DemoTaskScheduler : Form
    {
        public DemoTaskScheduler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refresh2();
        }

        /// <summary>
        /// 更新UI
        /// </summary>
        private async void Refresh2()
        {
            var result = await GreetingAsync("cheng");
            label1.Text = result;
        }
        
        /// <summary>
        /// 支持耗时的IO操作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static string Greeting(string name)
        {
            Thread.Sleep(3000);
            return string.Format("hello，{0}", name);
        }

        /// <summary>
        /// 异步线程等待
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static Task<string> GreetingAsync(string name)
        {
            return Task.Run<string>(() => {
                return Greeting(name);
            });
        }

        /// <summary>
        /// async和await关键字
        /// </summary>
        /// <returns></returns>
        public async Task<string> Fun3()
        {
            Console.WriteLine("主线程1");
            var t = await Task.Run(() => {
                Thread.Sleep(3 * 1000);
                Console.WriteLine("工作线程1");
                return "hello world";
            });
            var t2 = await Task.Run(() => {
                Console.WriteLine("工作线程2");
                return "hello world";
            });
            return t.ToString() + t2.ToString();
        }

        /// <summary>
        /// 在UI线程上做耗时的操作会卡死
        /// </summary>
        private void Fun1()
        {
            Task task = new Task(() => {
                Thread.Sleep(1000 * 10);
                label1.Text = "Hello World";
            });
            task.Start(TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 在工作线程做耗时的操作，在UI线程中做改变UI的操作，用户体验更好
        /// </summary>
        private void Fun2()
        {
            Task task = new Task(() => {
                Thread.Sleep(1000 * 10);
            });
            task.ContinueWith((obj) => {
                label1.Text = "Hello World";
            }, TaskScheduler.FromCurrentSynchronizationContext());
            task.Start();
        }
    }
}
