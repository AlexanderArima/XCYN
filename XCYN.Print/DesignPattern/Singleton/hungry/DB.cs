using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Singleton.hungry
{
    /// <summary>
    /// 饿汉式单例模式
    /// </summary>
    public class DB
    {

        /// <summary>
        /// 静态变量，相当于缓存，当CLR加载时自动加载
        /// </summary>
        private static DB _db = new DB();

        private DB()
        {
            System.Threading.Thread.Sleep(5000);
        }

        public static DB GetInstance()
        {
            return _db;
        }

        public static DateTime Show()
        {
            return DateTime.Now;
        }
    }
}
