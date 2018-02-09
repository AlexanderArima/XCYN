using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoLock
    {

        #region 用户模式锁

        private volatile bool isStop = false;

        /// <summary>
        /// 易变结构(一个线程读，一个写，在release的某种情况下，会有debug)
        /// volatile关键字：
        /// 1.不可以底层对代码进行优化。。。
        /// 2.我的read和write都是从memrory中读取。。。【我读取的都是最新的】
        /// </summary>
        public void Fun1()
        {
            var t = new Thread(()=> {
                var isSuccess = false;
                while (!isStop)
                {
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
        /// 互锁结构:Interlocked(可以做一些简单的计算)
        /// Increment：自增操作  Decrement：自减操作  Add：增加指定的值
        /// Exchange： 赋值      CompareExchange： 比较赋值
        /// </summary>
        public void Fun2()
        {
            int sum = 0;
            Interlocked.Increment(ref sum);//自增
            Interlocked.Decrement(ref sum);//自减
            Interlocked.Add(ref sum, 10);//增加
            Interlocked.Exchange(ref sum, -10);//交换
            Interlocked.CompareExchange(ref sum, 100, -10);//比较相等则替换
            Console.WriteLine(sum);
        }

        static SpinLock spinLock = new SpinLock();

        int num = 0;

        /// <summary>
        /// 旋转锁SpinLock 
        /// 特殊的业务逻辑让thread在用户模式下进行自选，欺骗cpu当前thread正在运行中。。。。
        /// </summary>
        public void Fun3()
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    bool f = false;
                    spinLock.TryEnter(ref f);
                    Console.WriteLine(num++);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    spinLock.Exit();
                }
            }
           
        }

        #endregion
    }
}
