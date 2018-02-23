using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common;

namespace XCYN.Test
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void IsNumeric()
        {
            var flag = Utils.IsNumeric(int.MaxValue);

            Assert.IsTrue(flag);

            flag = Utils.IsNumeric(int.MinValue);

            Assert.IsTrue(flag);

            flag = Utils.IsNumeric(1234567890);

            Assert.IsTrue(flag);

            flag = Utils.IsNumeric(123456789012);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void IsNumber()
        {
            var flag = Utils.IsNumber("1234");

            Assert.IsTrue(flag);

            flag = Utils.IsNumber("abc");

            Assert.IsFalse(flag);

            flag = Utils.IsNumber("-56952");

            Assert.IsTrue(flag);

            flag = Utils.IsNumber("1.2");

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void IsDouble()
        {
            var flag = Utils.IsDouble("123.44");

            Assert.IsTrue(flag);

            flag = Utils.IsDouble("123");

            Assert.IsTrue(flag);

            flag = Utils.IsDouble("12.a");

            Assert.IsFalse(flag);
            

        }

        [TestMethod]
        public void IsValidEmail()
        {
            var flag = Utils.IsValidEmail("xiecheng900424@qq.com");

            Assert.IsTrue(flag);

            flag = Utils.IsValidEmail("aaaaaa");

            Assert.IsFalse(flag);

            flag = Utils.IsValidEmail("@123.con");

            Assert.IsFalse(flag);

            flag = Utils.IsValidEmail("123@q.");

            Assert.IsFalse(flag);
        }
    }
}
