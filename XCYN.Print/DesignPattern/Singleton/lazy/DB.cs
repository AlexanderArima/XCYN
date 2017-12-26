using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Singleton.lazy
{
    /// <summary>
    /// 饱汉式单例模式
    /// </summary>
    public class DB
    {

        /// <summary>
        /// 静态变量，相当于缓存，当CLR加载时自动加载
        /// </summary>
        private static DB _db = null;
        private static object _lockMe = new object();

        private DB()
        {
            System.Threading.Thread.Sleep(5000);
        }

        public static DB GetInstance()
        {
            //双检锁
            if (_db == null)
            {
                lock(_lockMe)
                {
                    if(_db == null)
                    {
                        Console.WriteLine("线程{0}，当前时间：{1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now);
                        _db = new DB();
                    }
                    else
                    {
                        Console.WriteLine("已存在实例");
                    }
                }
                
            }
            return _db;
        }

        public static DateTime Show()
        {
            return DateTime.Now;
        }
    }
}
