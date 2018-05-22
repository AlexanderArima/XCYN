using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common;
using System.IO;

namespace XCYN.Test.Common
{
    /// <summary>
    /// CompressTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CompressTestCase
    {
        public CompressTestCase()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Compress()
        {
            var num = CompressHelper.Compress("1234567890");

            var result = CompressHelper.Decompress(num);

            Assert.AreEqual("1234567890", result);
        }

        [TestMethod]
        public void Compress2()
        {
            var str = "1234567890";
            
            var num = CompressHelper.Compress(Encoding.UTF8.GetBytes(str));

            var result = CompressHelper.Decompress(num);

            Assert.AreEqual("1234567890", Encoding.UTF8.GetString(result));
        }

        [TestMethod]
        public void Compress3()
        {
            FileInfo file = new FileInfo(@"D:\锋利的SQL.sql");
            CompressHelper.Compress(file);
        }
        
        [TestMethod]
        public void Compress4()
        {
            DirectoryInfo dir = new DirectoryInfo(@"D:\123");
            CompressHelper.Compress(dir);
        }

        [TestMethod]
        public void Compress5()
        {
            FileInfo file = new FileInfo(@"D:\锋利的SQL.sql");
            FileInfo file_dest = new FileInfo(@"D:\123.sql");
            CompressHelper.Compress(file, file_dest);
        }

        [TestMethod]
        public void Decompress()
        {
            FileInfo file = new FileInfo(@"D:\锋利的SQL.sql.gz");
            CompressHelper.Decompress(file);
        }
        
        [TestMethod]
        public void Decompress2()
        {
            FileInfo file = new FileInfo(@"D:\123.sql.gz");
            FileInfo file2 = new FileInfo(@"D:\456.sql");
            CompressHelper.Decompress(file, file2);
        }

        [TestMethod]
        public void Decompress3()
        {
            DirectoryInfo dir = new DirectoryInfo(@"D:\123\");
            DirectoryInfo dest = new DirectoryInfo(@"D:\456\");
            CompressHelper.Compress(dir, dest);
        }
    }
}
