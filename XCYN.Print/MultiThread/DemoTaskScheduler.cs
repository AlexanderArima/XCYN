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
            Fun2();
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
