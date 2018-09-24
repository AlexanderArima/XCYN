using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XCYN.Test.Print
{
    /// <summary>
    /// StringBuilderTestCase 的摘要说明
    /// </summary>
    [TestClass]
    public class StringBuilderTestCase
    {

        static StringBuilder str = new StringBuilder();

        public StringBuilderTestCase()
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
        public static void MyClassCleanup() {
            str = new StringBuilder();
        }
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
        public void TestAppendFormat()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
            str.AppendFormat("{0} {1}", "hello", "world");
            Assert.AreEqual("hello world", str.ToString());
        }

        /// <summary>
        /// 从指定位置开始，移除一定长度的字符串，如果起始位置或长度小于0，或起始位置+长度超过字符串的长度，则抛出ArguementOutOfRangeException
        /// </summary>
        [TestMethod]
        public void TestRemove()
        {
            str.AppendFormat("{0} {1}", "hello", "world");
            str.Remove(0, 6);
            Assert.AreEqual("world", str.ToString());
        }

        /// <summary>
        /// Insert()可以在任意位置插入字符串，数字，对象。如果插入的下标超过待插入字符串的长度，会抛出ArguementOutOfRangeException
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            str.Append("中国");
            str.Insert(0, "你好，");
            Assert.AreEqual("你好，中国", str.ToString());
        }

        [TestMethod]
        public void TestReplace()
        {
            str.Append("Good Morning");
            str.Replace("Morning", "Afternoon",5,str.Length - 5);
            Assert.AreEqual("Good Afternoon", str.ToString());
            try
            {
                str.Clear();
                str.Replace(null, "");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(true);
            }
            try
            {
                str.Clear();
                str.Replace("", "");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
