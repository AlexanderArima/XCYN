using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using log4net.Config;
using log4net;
using XCYN.Print.rabbitmq;

namespace XCYN.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            InitLog4Net();

            var logger = LogManager.GetLogger(typeof(UnitTest1));
            Console.WriteLine(logger.IsDebugEnabled);
            logger.Info("消息");
            logger.Warn("警告");
            logger.Error("异常");
            logger.Fatal("错误");

            Console.ReadLine();
        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        [TestMethod]
        public void YieldTest()
        {
            YieldTest y = new Test.YieldTest();
            y.WithYield();
            Console.ReadLine();
        }

        [TestMethod]
        public void NoYieldTest()
        {
            YieldTest y = new Test.YieldTest();
            y.WithNoYield();
            Console.ReadLine();
        }

        [TestMethod]
        public void PublishTest()
        {
            Publish.PublishPriority();
        }

    }
}
