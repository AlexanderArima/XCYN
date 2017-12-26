using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Print.DesignPattern.StaticFactoryMethod;

namespace XCYN.Test
{
    /// <summary>
    /// DesignPattern 的摘要说明
    /// </summary>
    [TestClass]
    public class DesignPattern
    {

        [TestMethod]
        public void TestMethod1()
        {
            Handler user = new Handler();
            user.Insert();
        }

        [TestMethod]
        public void MyTestMethod()
        {
            XCYN.Print.DesignPattern.TemplateMethod.Handler handler = new XCYN.Print.DesignPattern.TemplateMethod.Handler();
            handler.HanderQuestion();

        }
    }
}
