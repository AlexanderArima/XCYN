using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询开始\r\n" + this.textBox1.Text;
            Query();
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询结束\r\n" + this.textBox1.Text;
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
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询开始\r\n" + this.textBox1.Text;
            var result = await QueryAsync();
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询结束\r\n" + this.textBox1.Text;

            // ===========================等待3s后继续=======================
            await Task.Delay(3000);

            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，添加开始\r\n" + this.textBox1.Text;
            await Insert();
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，添加结束\r\n" + this.textBox1.Text;
        }

        private async Task<List<string>> QueryAsync()
        {
            // 异步调用，Task.Delay返回Task，那么就可以使用await关键字调用，这样就是调用异步方法了
            // 调用异步方法的代价就是，需要将方法自身添加async关键字，并在方法的后缀添加Async标记
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "，开始查询");

            await Task.Delay(5000);

            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "，结束查询");
            return new List<string>()
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
            };
        }

        private async Task<int> QueryAsync2()
        {
            // 异步调用，Task.Delay返回Task，那么就可以使用await关键字调用，这样就是调用异步方法了
            // 调用异步方法的代价就是，需要将方法自身添加async关键字，并在方法的后缀添加Async标记
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "，开始查询");

            await Task.Delay(3000);

            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "，结束查询");
            return 10;
        }

        private async Task Insert()
        {
            await Task.Delay(1000);
        }

        /// <summary>
        /// 返回了空的Task对象
        /// </summary>
        /// <returns></returns>
        private Task GetNullTask()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 返回一个空的带泛型的Task对象
        /// </summary>
        /// <returns></returns>
        private Task<string> GetStringTask()
        {
            return Task.FromResult("Love");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 方法不使用async修饰，调用的异步方法也不使用await，而是在异步方法后继续调用GetAwaiter().GetResult()，但这种方法会引起线程的阻塞，不推荐使用
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询开始\r\n" + this.textBox1.Text;
            var result = QueryAsync().GetAwaiter().GetResult();
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询结束\r\n" + this.textBox1.Text;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询开始\r\n" + this.textBox1.Text;
            List<Task> list_task = new List<Task>();
            var t1 = QueryAsync();
            var t2 = QueryAsync2();
            list_task.Add(t1);
            list_task.Add(t2);

            // Task.WhenAll的使用场景是一个任务被拆成多个子任务，需要等待所有任务完成后才会继续执行
            Task result = Task.WhenAll(list_task);

            await result;

            // 获取各个任务的返回值，作为下一步任务的基础
            var result_t1 = t1.Result;
            var result_t2 = t2.Result;
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询结束\r\n" + this.textBox1.Text;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询开始\r\n" + this.textBox1.Text;
            List<Task> list_task = new List<Task>();
            var t1 = QueryAsync();
            var t2 = QueryAsync2();
            list_task.Add(t1);
            list_task.Add(t2);

            // Task.WhenAny的使用场景是一个任务被拆成多个子任务，只要某一个任务执行完成，就会继续执行
            Task result = Task.WhenAny(list_task);

            await result;
            
            if(t1.Status == TaskStatus.RanToCompletion)
            {
                var result_t1 = t1.Result;
            }

            if(t2.Status == TaskStatus.RanToCompletion)
            {
                var result_t2 = t2.Result;
            }

            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + "，查询结束\r\n" + this.textBox1.Text;
        }
    }
}
