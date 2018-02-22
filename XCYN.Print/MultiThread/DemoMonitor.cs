using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoMonitor
    {

        private static object lockMe = new object();

        private static int num = 0;

        /// <summary>
        /// Monitor监视锁
        /// </summary>
        public static void Fun1()
        {
            for (int i = 0; i < 100; i++)
            {
                var b = false;
                try
                {
                    Monitor.Enter(lockMe,ref b);
                    Console.WriteLine(num++);
                    //Monitor.Exit(lockMe);
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    if (b) Monitor.Exit(lockMe);
                }
            }
        }


        /// <summary>
        /// Lock语法糖，等同于上个方法，不需要再写try...catch和if判断了
        /// </summary>
        public static void Fun2()
        {
            for (int i = 0; i < 100; i++)
            {
                lock(lockMe)
                {
                    Console.WriteLine(num++);
                }
            }
        }

        public void Fun3()
        {
            bool flag = false;
            Monitor.TryEnter(lockMe, 500, ref flag);
            if(flag)
            {
                try
                {
                    Thread.Sleep(1000);
                    //被锁定，同步访问对象
                    for (int i = 0; i < 2000; i++)
                    {
                        Console.WriteLine(num++);
                    }
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    Monitor.Exit(lockMe);
                    Console.WriteLine("释放锁");
                }
            }
            else
            {
                //没有被锁定，超时了，执行其他操作
                Console.WriteLine("未被锁定，或者超时");
            }
        }
    }
}
