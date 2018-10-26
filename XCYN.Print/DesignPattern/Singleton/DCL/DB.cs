using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Singleton.DCL
{
    /// <summary>
    /// 双检锁（DCL，即 double-checked locking）
    /// 是否 Lazy 初始化：是
    /// 是否多线程安全：是
    /// 实现难度：较复杂
    /// 描述：这种方式采用双锁机制，安全且在多线程情况下能保持高性能。
    /// getInstance() 的性能对应用程序很关键。
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
            //System.Threading.Thread.Sleep(5000);
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

        public DateTime Show()
        {
            return DateTime.Now;
        }
    }
}
