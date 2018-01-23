using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common;

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
    }
}
