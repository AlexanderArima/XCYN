using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using log4net.Config;
using log4net;
using XCYN.Print.rabbitmq;
using System.Web;

namespace XCYN.Test
{
    [TestClass]
    public class Log4NetTestCase
    {
        [TestMethod]
        public void TestMethod1()
        {
            InitLog4Net();
            var logger = LogManager.GetLogger(typeof(Log4NetTestCase));
            logger.Info("消息",new NullReferenceException("名字不能为空"));
            logger.Warn("警告");
            logger.Error("异常");
            logger.Fatal("错误");
        }

        private static void InitLog4Net()
        {
            XmlConfigurator.Configure();
        }

        [TestMethod]
        public void YieldTest()
        {
            //YieldTest y = new Test.YieldTest();
            //y.WithYield();
            //Console.ReadLine();
        }

        [TestMethod]
        public void NoYieldTest()
        {
            //YieldTest y = new Test.YieldTest();
            //y.WithNoYield();
            //Console.ReadLine();
        }

        [TestMethod]
        public void PublishTest()
        {
            Publish.PublishBasic();
        }

    }
}
