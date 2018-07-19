using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Print.MongoDB;

namespace XCYN.Test.Print.MongoDB
{
    [TestClass]
    public class MongoHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string f = @"E:\download\ASP.NETWebAPI_jb51.rar";
            MongoHelper helper = new MongoHelper("MyFS");
            var obj = helper.FilePut(f);
            Assert.AreNotEqual(0, obj);
        }

        [TestMethod]
        public void TestDelete()
        {
            string f = "5b50314041b99b2ff027451b";
            MongoHelper helper = new MongoHelper("MyFS");
            helper.FileDelete(f);
        }

        [TestMethod]
        public void TestGet()
        {
            string f = "5b5028b63af9150c200c6e7c";
            MongoHelper helper = new MongoHelper("MyFS");
            FileStream stream = new FileStream(@"C:\2.exe", FileMode.Create);
            helper.FileGet(f, stream);
        }
    }
}
