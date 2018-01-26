using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoWait
    {
        /// <summary>
        /// Task.WaitAll方法等待所有线程执行完成之后再继续执行
        /// </summary>
        public void Fun1()
        {
            Task t1 = new Task(()=> {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}",DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });

            t1.Start();
            t2.Start();

            Task.WaitAll(t1,t2);

            Console.WriteLine("主线程:{0}",DateTime.Now);
        }

        /// <summary>
        /// Task.WaitAny方法只要有一个线程执行完之后再继续执行
        /// </summary>
        public void Fun2()
        {
            Task t1 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}", DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });

            t1.Start();
            t2.Start();

            Task.WaitAny(t1, t2);

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// Task.Wait方法等待当前线程执行完之后，再继续执行
        /// </summary>
        public void Fun3()
        {
            Task t1 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}", DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });
            
            t1.Start();
            t2.Start();
            t2.Wait();
            
            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// Task.ContinueWith方法可让任务完成后继续执行异步任务
        /// </summary>
        public void Fun4()
        {
            Task t1 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}", DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });

            t1.Start();
            t2.Start();
            
            t2.ContinueWith((obj) => {
                Console.WriteLine("工作线程3:{0}", DateTime.Now);
            });

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// Task.WhenAll方法当所有线程执行完毕后，再异步执行方法
        /// </summary>
        public void Fun5()
        {
            Task t1 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}", DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });

            t1.Start();
            t2.Start();

            Task.WhenAll(t1,t2).ContinueWith((obj)=> {
                Console.WriteLine("工作线程3:{0}", DateTime.Now);
            });

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// Task.WhenAny方法当任一线程执行完毕后，在异步执行方法
        /// </summary>
        public void Fun6()
        {
            Task t1 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}", DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });

            t1.Start();
            t2.Start();

            Task.WhenAny(t1, t2).ContinueWith((obj) => {
                Console.WriteLine("工作线程3:{0}", DateTime.Now);
            });

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// Task.Factory.ContinueWhenAll方法线程后执行完毕后，再异步执行方法
        /// </summary>
        public void Fun7()
        {
            Task t1 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}", DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });

            t1.Start();
            t2.Start();

            Task.Factory.ContinueWhenAll(new Task[2] { t1, t2 }, (obj) => {
                Console.WriteLine("工作线程3:{0}", DateTime.Now);
            });

            Console.WriteLine("主线程:{0}", DateTime.Now);
        }

        /// <summary>
        /// Task.Factory.ContinueWhenAny方法当任一线程后执行完后，再异步执行方法
        /// </summary>
        public void Fun8()
        {
            Task t1 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("工作线程1:{0}", DateTime.Now);
            });

            Task t2 = new Task(() => {
                Thread.Sleep(3000);
                Console.WriteLine("工作线程2:{0}", DateTime.Now);
            });

            t1.Start();
            t2.Start();

            Task.Factory.ContinueWhenAny(new Task[2] { t1, t2 }, (obj) => {
                Console.WriteLine("工作线程3:{0}", DateTime.Now);
            });

            Console.WriteLine("主线程:{0}", DateTime.Now);

            
        }
    }
}
