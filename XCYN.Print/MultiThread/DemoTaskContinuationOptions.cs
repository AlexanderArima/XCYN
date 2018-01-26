using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoTaskContinuationOptions
    {
        /// <summary>
        /// TaskContinuationOptions.LazyCancellation方法可在任务取消后，仍然按照顺序执行
        /// </summary>
        public void Fun1()
        {
            CancellationTokenSource cancel = new CancellationTokenSource();
            cancel.Cancel();

            Task t1 = new Task(() => {
                Thread.Sleep(1000);
               
                Console.WriteLine("工作线程1,pid:{0},时间:{1}",Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            });

            //Task t2 = t1.ContinueWith((obj) => {
                
            //    Console.WriteLine("工作线程2，pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);

            //}, cancellationToken:cancel.Token);

            Task t2 = t1.ContinueWith((obj) =>
            {

                Console.WriteLine("工作线程2，pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);

            }, cancellationToken: cancel.Token,
               continuationOptions: TaskContinuationOptions.LazyCancellation,
               scheduler: TaskScheduler.Current);

            Task t3 = t2.ContinueWith((obj) => {
                Console.WriteLine("工作线程3，pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            });

            t1.Start();
            
            Console.WriteLine("主线程:pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }

        /// <summary>
        /// TaskContinuationOptions.ExecuteSynchronously方法可让后续的任务延续本任务。这样可以防止线程切换
        /// </summary>
        public void Fun2()
        {

            Task t1 = new Task(() => {
                Thread.Sleep(1000);

                Console.WriteLine("工作线程1,pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            });

            Task t2 = t1.ContinueWith((obj) =>
            {
                //t2的线程和t1线程相同
                Console.WriteLine("工作线程2，pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);

            },TaskContinuationOptions.ExecuteSynchronously);

            Task t3 = t2.ContinueWith((obj) => {
                Console.WriteLine("工作线程3，pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            });

            t1.Start();

            Console.WriteLine("主线程:pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }

        /// <summary>
        /// TaskContinuationOptions.NotOnRanToCompletion方法如果前面的任务是非完成状态，则执行
        /// 如果前面的任务是完成状态，则不执行
        /// </summary>
        public void Fun3()
        {

            Task t1 = new Task(() => {
                Thread.Sleep(1000);

                Console.WriteLine("工作线程1,pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
                //throw new Exception();
            });

            //如果t1没有抛出异常则不执行t2，如果t1抛出异常则执行t2
            Task t2 = t1.ContinueWith((obj) =>
            {
                Console.WriteLine("工作线程2，pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);

            }, TaskContinuationOptions.NotOnRanToCompletion);
            

            t1.Start();

            Console.WriteLine("主线程:pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }

        /// <summary>
        /// TaskContinuationOptions.OnlyOnRanToCompletion方法如果前面的任务是非完成状态，则不执行
        /// 如果前面的任务是完成状态，则执行
        /// </summary>
        public void Fun4()
        {

            Task t1 = new Task(() => {
                Thread.Sleep(1000);

                Console.WriteLine("工作线程1,pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
                throw new Exception();
            });

            //如果t1没有抛出异常则执行t2，如果t1抛出异常则不执行t2
            Task t2 = t1.ContinueWith((obj) =>
            {
                Console.WriteLine("工作线程2，pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            
            t1.Start();

            Console.WriteLine("主线程:pid:{0},时间:{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }
    }
}
