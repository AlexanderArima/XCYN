using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoTaskCreationOptions
    {
        /// <summary>
        /// AttachedToParent附加线程到父级
        /// </summary>
        public void Fun1()
        {
            
            Task t = new Task(() => {
                Task t1 = new Task(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("工作线程1:{0}", DateTime.Now);
                },TaskCreationOptions.AttachedToParent);

                Task t2 = new Task(() => {
                    Thread.Sleep(3000);
                    Console.WriteLine("工作线程2:{0}", DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                t1.Start();
                t2.Start();
            });

            t.Start();
            t.Wait();//等待t1,t2两个线程执行完成

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Fun2()
        {

            Task t = new Task(() => {
                Task t1 = new Task(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("工作线程1:{0}", DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                Task t2 = new Task(() => {
                    Thread.Sleep(3000);
                    Console.WriteLine("工作线程2:{0}", DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                t1.Start();
                t2.Start();
            }, TaskCreationOptions.DenyChildAttach);

            t.Start();
            t.Wait();

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// 长时间执行的线程任务需使用LongRunning，这样就不会在线程池中执行而是在线程中执行了
        /// </summary>
        public void Fun3()
        {

            Task t = new Task(() => {
                Task t1 = new Task(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("工作线程1:{0}", DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                Task t2 = new Task(() => {
                    Thread.Sleep(3000);
                    Console.WriteLine("工作线程2:{0}", DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                t1.Start();
                t2.Start();
            }, TaskCreationOptions.LongRunning);

            t.Start();
            t.Wait();

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// PerferFairness会将执行的线程放在全局队列中调用，而不是本地队列中
        /// </summary>
        public void Fun4()
        {

            Task t = new Task(() => {
                Task t1 = new Task(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("工作线程1:{0}", DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                Task t2 = new Task(() => {
                    Thread.Sleep(3000);
                    Console.WriteLine("工作线程2:{0}", DateTime.Now);
                }, TaskCreationOptions.AttachedToParent);

                t1.Start();
                t2.Start();
            }, TaskCreationOptions.PreferFairness);

            t.Start();
            t.Wait();

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }
    }
}
