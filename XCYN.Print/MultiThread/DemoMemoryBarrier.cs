using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoMemoryBarrier
    {
        /// <summary>
        /// MemoryBarrier
        /// 在此方法之前的内存写入都要及时从cpu cache中更新到 memory
        /// 在此方法之后的内存读取都要从memory中读取，而不是cpu cache
        /// </summary>
        public void Fun1()
        {
            var isStop = false;
            var t = new Thread(() =>
            {
                var isSuccess = false;
                while (!isStop)
                {
                    Thread.MemoryBarrier();
                    isSuccess = !isSuccess;
                }
            });

            t.Start();

            Thread.Sleep(1000);
            isStop = true;
            t.Join();

            Console.WriteLine("主线程执行结束！");
            Console.ReadLine();
        }

        /// <summary>
        /// VolatileRead效果和MemoryBarrier相似
        /// </summary>
        public void Fun2()
        {
            var isStop = 0;

            var t = new Thread(() =>
            {
                var isSuccess = false;
                while (isStop == 0)
                {
                    Thread.VolatileRead(ref isStop);
                    isSuccess = !isSuccess;
                }
            });

            t.Start();

            Thread.Sleep(1000);
            isStop = 1;
            t.Join();

            Console.WriteLine("主线程执行结束！");
            Console.ReadLine();
        }
    }
}
