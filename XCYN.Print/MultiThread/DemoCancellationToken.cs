using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace XCYN.Print.MultiThread
{
    public class DemoCancellationToken
    {
        /// <summary>
        /// Token.IsCancellationRequested用以判断请求是否取消
        /// Cancel方法可以取消当前请求
        /// </summary>
        public void Fun1()
        {
            CancellationTokenSource c = new CancellationTokenSource();
            Task t = Task.Factory.StartNew(() => {
                while (!c.Token.IsCancellationRequested)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("hello world");
                }
            });

            Console.WriteLine("主线程");
            Thread.Sleep(3000);
            Console.WriteLine("取消工作线程");
            c.Cancel();//取消
        }

        /// <summary>
        /// CancelAfter延时取消
        /// </summary>
        public void Fun2()
        {
            CancellationTokenSource c = new CancellationTokenSource();
            Task t = Task.Factory.StartNew(() => {
                while (!c.Token.IsCancellationRequested)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("hello world");
                }
            });

            Console.WriteLine("主线程");
            c.CancelAfter(new TimeSpan(0, 0, 3));
            Console.WriteLine("3秒后取消工作线程");
        }

        /// <summary>
        /// Token.Register可以设置超时后的回调函数
        /// </summary>
        public void Fun3()
        {
            CancellationTokenSource c = new CancellationTokenSource();
            c.Token.Register(() => {
                Console.WriteLine("超时回调函数");
            });
            Task t = Task.Factory.StartNew(() => {
                while (!c.Token.IsCancellationRequested)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("hello world");
                }
            });

            Console.WriteLine("主线程");
            c.CancelAfter(new TimeSpan(0, 0, 3));
        }


        public void Fun4()
        {
            CancellationTokenSource c = new CancellationTokenSource();
            
            Task t = Task.Factory.StartNew(() => {
                try
                {
                    while(true)
                    {
                        if (c.Token.IsCancellationRequested)
                        {
                            throw new OperationCanceledException("释放资源");
                        }
                        else
                        {
                            Thread.Sleep(300);
                            Console.WriteLine("hello world");
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            },c.Token);

            Console.WriteLine("主线程");
            c.CancelAfter(new TimeSpan(0, 0, 3));
            
        }
    }
}
