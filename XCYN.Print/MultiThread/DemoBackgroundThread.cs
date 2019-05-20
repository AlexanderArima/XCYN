using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoBackgroundThread
    {
        /// <summary>
        /// 默认情况下由Thread创建的线程为前台线程
        /// </summary>
        public void Fun1()
        {
            Thread thread = new Thread(ReadFile);
            //thread.IsBackground = true;
            thread.Start();
            Console.WriteLine("程序执行完毕");
        }

        /// <summary>
        /// 默认情况下由线程池创建的线程为后台线程
        /// </summary>
        public void Fun2()
        {
            ThreadPool.QueueUserWorkItem(ReadFile);
            Console.WriteLine("程序执行完毕");
        }

        public void Fun3()
        {
            Task task = new Task(()=> {
                ReadFile(null);
            });
            task.Start();
            task.ContinueWith((parma) => {
                Console.WriteLine("程序执行完毕");
            });
          
            
        }

        public void Fun4()
        {
            Task.Factory.StartNew(() => {
                Thread.CurrentThread.IsBackground = false;
                ReadFile(null);
            });
            Console.WriteLine("程序执行完毕");
        }

        private static void ReadFile(object obj)
        {
            Thread.Sleep(5000);
            Console.WriteLine("正在读取文件");
        }
    }
}
