using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoParallel
    {
        /// <summary>
        /// Parallel.For是用来并行计算的，不要在其中Break，因为会出现Bug
        /// </summary>
        public void Fun1()
        {
            Parallel.For(0, 100, (item,loop) =>
            {
                if(item == 10)
                {
                    loop.Break();
                    return;
                }
                Console.WriteLine(item);
            });
        }

        /// <summary>
        /// 重载方法
        /// </summary>
        public void Fun2()
        {
            int totalNum = 0;
            Parallel.For(0, 100, localInit: () => {
                //起始值为0
                return 0;
            },body:(current,loop,total)=> {
                //迭代增加
                total += current;
                return total;
            },localFinally:(total)=> {
                //每个线程执行完之后再执行这里
                Interlocked.Add(ref totalNum, total);
            });
            Console.WriteLine(totalNum);
        }

        public void Fun3()
        {
            int result = 0;
            Parallel.For<int>(0, 100, localInit: () =>
            {
                return 0;
            }, body: (current, loop, total) => {
                total += (int)current;
                return total;
            },localFinally:(total) => {
                Interlocked.Add(ref result, total);
            });
            Console.WriteLine(result);
        }

        /// <summary>
        /// Parallel.Foreach能遍历所有类型，Parallel.For只能遍历数组
        /// </summary>
        public void Fun4()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>()
            {
                { 1, 100 },
                { 2, 200 },
                { 3, 300 },
            };

            Parallel.ForEach(dict, (source) => {
                Console.WriteLine(source.Key +":"+source.Value);
            });
        }

        public void Fun5()
        {
            Parallel.Invoke(() => {
                Console.WriteLine("工作线程1,id:{0}",Thread.CurrentThread.ManagedThreadId);
            }, () => {
                Console.WriteLine("工作线程2,id:{0}", Thread.CurrentThread.ManagedThreadId);
            });
        }
    }
}
