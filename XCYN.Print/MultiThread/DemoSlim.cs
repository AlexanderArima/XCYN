#define DEBUG
//#undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    /// <summary>
    /// 混合模式锁
    /// </summary>
    public class DemoSlim
    {
        //同时有两个线程同时运行
        public static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 10);
        static int num = 0;

        /// <summary>
        /// SemaphoreSlim信号量混合模式
        /// </summary>
        public void Fun1()
        {
            for (int i = 0; i < 100; i++)
            {
                semaphoreSlim.Wait();
                Thread.Sleep(100);
#if DEBUG
#warning Debug is define  
                Console.WriteLine(num++);
#endif
                semaphoreSlim.Release();
            }
        }

        static ManualResetEventSlim slim = new ManualResetEventSlim(true);

        public void Fun2()
        {
            string s = "a";
            string ss = $"s = {s.ToLower()}";
            
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    slim.Wait();
                    Console.WriteLine(num++);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            slim.Set();
        }

        public static int _num = 0;
        public static object sync = new object();
        public static Person p = new Person();

        public void Fun3()
        {
            for (int i = 0; i < 500000; i++)
            {
                int age = p.Age;
            }
            Console.WriteLine(p.Age);
        }
    }

    public class Person
    {
        private int _age;

        public int Age { get { return Interlocked.Increment(ref _age); } set { Age = value; } }

        public static object sync = new object();
    }

}
