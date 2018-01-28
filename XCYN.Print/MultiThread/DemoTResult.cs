using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoTResult
    {
        /// <summary>
        /// Task<TResult>可以在任务执行完之后返回值
        /// </summary>
        public void Fun1()
        {
            Task<int> t1 = Task<int>.Factory.StartNew(() => { return 1; });

            Task<int> t2 = Task<int>.Factory.StartNew(() => { return 10; });

            Console.WriteLine(t1.Result + t2.Result);
        }

        public void Fun2()
        {
            Task<int> t1 = Task<int>.Factory.StartNew(() => {
                Console.WriteLine("工作线程1");
                Thread.Sleep(3000);
                return 1;
            });

            var t2 = Task<int>.Run<int>(() =>
            {
                Console.WriteLine("工作线程2");
                return t1.Result;
            });

            var t3 = Task.Factory.StartNew(() => {
                Console.WriteLine("工作线程3");
            });
            Console.WriteLine("主线程");
            Console.WriteLine(t2.Result);
        }
    }
}
