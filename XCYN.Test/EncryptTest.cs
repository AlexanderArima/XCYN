using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common;
using XCYN.Print.Generics;

namespace XCYN.Test
{
    [TestClass]
    public class EncryptTest
    {
        [TestMethod]
        public void Encrypt()
        {
            var target = DESEncrypt.Encrypt("Cheng");
            var source = DESEncrypt.Decrypt(target);
            Assert.AreEqual("Cheng", source);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var num = TStudent<string>.num;
            Assert.AreEqual(2, num);
        }
    }
}
