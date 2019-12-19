using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MyQuartz
{
    /// <summary>
    /// Quartz从配置文件和构造函数读取配置项
    /// </summary>
    public class QuartzConfig
    {
        /// <summary>
        /// 通过构造函数的方式修改ThreadCount的大小
        /// ThreadCount表示线程池的最大连接数
        /// </summary>
        public static void Fun1()
        {
            var collection = new System.Collections.Specialized.NameValueCollection();
            collection.Add("quartz.threadPool.ThreadCount", "20");
            var stdFactory = new StdSchedulerFactory(collection);
            var std = stdFactory.GetScheduler();
            std.Start();
            var meta = std.GetMetaData();
            var threadPool = meta.ThreadPoolSize;
            Console.WriteLine("threadPoolSize:{0}", threadPool);
        }

        /// <summary>
        /// 通过App.config配置文件修改ThreadCount的大小
        /// </summary>
        public static void Fun2()
        {
            var stdFactory = new StdSchedulerFactory();
            var std = stdFactory.GetScheduler();
            std.Start();
            var meta = std.GetMetaData();
            var threadPool = meta.ThreadPoolSize;
            Console.WriteLine("threadPoolSize:{0}", threadPool);
        }
    }
}
