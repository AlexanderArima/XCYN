using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common;
using XCYN.Print.Generics;
using XCYN.Winform.Model.MeiTuan.EF;
using System.Linq;

namespace XCYN.Test.Common
{
    [TestClass]
    public class EncryptTestCase
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

        [TestMethod]
        public void MyTestMethod2()
        {
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                var query = from a in db.T_City
                            where a.State == true
                            select a; 
                var list = query.ToList();
            }
        }
    }
}
