using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoPLinq
    {
        /// <summary>
        /// AsParallel方法可以将串行的代码转换为并行
        /// </summary>
        public void Fun1()
        {
            var list = Enumerable.Range(0, 100).ToList();

            var query = from n in list.AsParallel()
                        select new
                        {
                            thread = Thread.CurrentThread.ManagedThreadId,
                            num = n
                        };

            foreach (var item in query)
            {
                Console.WriteLine("item:{0},threadID:{1}",item.num,item.thread);
            }
        }

        /// <summary>
        /// AsOrdered方法将并行的结果还是按照未排序的样子进行排序
        /// </summary>
        public void Fun2()
        {
            var list = Enumerable.Range(0, 100).ToList();

            var query = from n in list.AsParallel().AsOrdered()
                        select new
                        {
                            thread = Thread.CurrentThread.ManagedThreadId,
                            num = n
                        };

            foreach (var item in query)
            {
                Console.WriteLine("item:{0},threadID:{1}", item.num, item.thread);
            }
        }

        /// <summary>
        /// AsUnordered方法将按照未排序的方式排序
        /// </summary>
        public void Fun3()
        {
            var list = Enumerable.Range(0, 10).ToList();

            var query = from n in list.AsParallel().AsUnordered()
                        select new
                        {
                            thread = Thread.CurrentThread.ManagedThreadId,
                            num = n
                        };

            foreach (var item in query)
            {
                Console.WriteLine("item:{0},threadID:{1}", item.num, item.thread);
            }
        }

        /// <summary>
        /// AsSequential方法将并行的代码改成串行
        /// </summary>
        public void Fun4()
        {
            var list = Enumerable.Range(0, 100).ToList();

            var query = from n in list.AsParallel().AsSequential()
                        select new
                        {
                            thread = Thread.CurrentThread.ManagedThreadId,
                            num = n
                        };

            foreach (var item in query)
            {
                Console.WriteLine("item:{0},threadID:{1}", item.num, item.thread);
            }
        }

        /// <summary>
        /// WithCancellation设置取消操作
        /// </summary>
        public void Fun5()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            source.Cancel();

            var list = Enumerable.Range(0, 100).ToList();

            var query = from n in list.AsParallel().WithCancellation(token)
                        select new
                        {
                            thread = Thread.CurrentThread.ManagedThreadId,
                            num = n
                        };

            foreach (var item in query)
            {
                Console.WriteLine("item:{0},threadID:{1}", item.num, item.thread);
            }
        }

        /// <summary>
        /// WithDegreeOfParallelism 设置并行度
        /// WithExecutionMode 是否强制并行计算
        /// </summary>
        public void Fun6()
        {
            var list = Enumerable.Range(0, 100).ToList();

            var query = from n in list.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount)
                                                   .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                        select new
                        {
                            thread = Thread.CurrentThread.ManagedThreadId,
                            num = n
                        };

            foreach (var item in query)
            {
                Console.WriteLine("item:{0},threadID:{1}", item.num, item.thread);
            }
        }
    }
}
