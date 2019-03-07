using System;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common;

namespace XCYN.Test.Common
{
    [TestClass]
    public class Log4NetHelpTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            XmlConfigurator.Configure();
            Log4NetHelper.Debug("测试Debug方法");

        }
    }
}
