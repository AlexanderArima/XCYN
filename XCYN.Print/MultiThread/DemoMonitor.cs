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
    }
}
