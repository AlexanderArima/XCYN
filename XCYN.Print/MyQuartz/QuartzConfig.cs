using Common.Logging;
using Common.Logging.Simple;
using Quartz;
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
        /// 在配置文件中添加以下配置
        ///<configSections>
        ///<section name = "quartz" type="System.Configuration.NameValueSectionHandler, System, Version=1.0.5000.0,Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
        ///</configSections>
        ///<quartz>
        ///<add key = "quartz.threadPool.threadCount" value="30"/>
        ///</quartz>
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

        /// <summary>
        /// 通过quartz.config配置文件修改ThreadCount的大小
        /// 在配置文件中添加以下配置
        /// quartz.threadPool.ThreadCount = 10
        /// 将该配置文件设置为复制到编译目录
        /// </summary>
        public static void Fun3()
        {
            var stdFactory = new StdSchedulerFactory();
            var std = stdFactory.GetScheduler();
            std.Start();
            var meta = std.GetMetaData();
            var threadPool = meta.ThreadPoolSize;
            Console.WriteLine("threadPoolSize:{0}", threadPool);
        }

        /// <summary>
        /// 通过环境变量修改ThreadCount的大小
        /// </summary>
        public static void Fun4()
        {
            Environment.SetEnvironmentVariable("quartz.threadPool.ThreadCount", "50");
            var stdFactory = new StdSchedulerFactory();
            var std = stdFactory.GetScheduler();
            std.Start();
            var meta = std.GetMetaData();
            var threadPool = meta.ThreadPoolSize;
            Console.WriteLine("threadPoolSize:{0}", threadPool);
        }

        #region 通过配置文件，定义Job和Trigger，以达到快速发布的效果(无需重新编译代码)

        /*
         * job_scheduling_data_2_0.xsd 这个文件是XML的架构文件，打开quartz.config文件，点击XML -> 架构，
         * 添加这个文件后，再在XML中就会有代码提示
         * **/

        /// <summary>
        /// 通过读取配置文件的方式设置Job和Trigger
        /// </summary>
        public static void Fun5()
        {
            var factory = new StdSchedulerFactory(new System.Collections.Specialized.NameValueCollection()
            {
                //从默认的xml文件中读取
                {"quartz.plugin.xml.type","Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin,Quartz" },
                //从自定义的xml文件中读取
                //{"quartz.plugin.xml.fileNames","quartz_jobs1.xml" },
                //ScanInterval属性表示，每隔几秒扫描一次配置文件，如果配置文件发生改成就重新加载(实测可以换Trigger和Job)，默认不扫描配置文件
                {"quartz.plugin.xml.ScanInterval","2" }
            });

            IScheduler scheduler = factory.GetScheduler();

            LogManager.Adapter = new TraceLoggerFactoryAdapter()
            {
                Level = LogLevel.All
            };

            scheduler.Start();
        }
        #endregion

    }
}
