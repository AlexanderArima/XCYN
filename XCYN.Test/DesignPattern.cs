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
            User user = new User();
            user.Insert();
        }
    }
}
