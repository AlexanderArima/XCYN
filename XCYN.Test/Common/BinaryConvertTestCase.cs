using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common;
using System.Diagnostics;

namespace XCYN.Test.Common
{
    /// <summary>
    /// BinaryConvertTestCase 的摘要说明
    /// </summary>
    [TestClass]
    public class BinaryConvertTestCase
    {
        public BinaryConvertTestCase()
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
        public void X2XTest()
        {
            // 测试36进制转16进制，16进制转32进制
            var string36 = "hb20211102";
            var string16 = BinaryConvert.X2X(string36, 36, 16);
            var string36_after = BinaryConvert.X2X(string16, 16, 36);
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("string16：" + string16);
            Assert.AreEqual(string36, string36_after);
        }

        [TestMethod]
        public void X2XTest02()
        {
            // 测试36进制转16进制，16进制转32进制
            var string36 = "hb20211102";
            var string2 = BinaryConvert.X2X(string36, 36, 2);
            var string36_after = BinaryConvert.X2X(string2, 2, 36);
            Debug.WriteLine("------------------------------------------------------------");
            Debug.WriteLine("string2：" + string2);
            Assert.AreEqual(string36, string36_after);
        }
    }
}
