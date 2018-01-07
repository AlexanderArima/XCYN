using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common.NoSql.redis;

namespace XCYN.Test
{
    /// <summary>
    /// RedisCommandTest 的摘要说明
    /// </summary>
    [TestClass]
    public class RedisCommandTest
    {

        RedisCommand _command = new RedisCommand();

        public RedisCommandTest()
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
        public void StringGetRange()
        {
            RedisCommand command = new RedisCommand();
            var str = command.StringGetRange("myStr", 0, 3);

            Assert.AreEqual("this", str);
        }

        [TestMethod]
        public void StringGetSet()
        {
            var str = _command.StringGetSet("mycount", "0");
            Assert.AreEqual("0", str);
            var str2 = _command.StringGet("mycount");
            Assert.AreEqual("0", str2);
        }

        [TestMethod]
        public void ListIndex()
        {
            var str = _command.ListIndex("mylist", 0);

            Assert.AreEqual("2", str);

            var str2 = _command.ListIndex("mylist", 2);

            Assert.IsNotNull(str2);
        }

        [TestMethod]
        public void ListLeftInsert()
        {
            var count = _command.ListLeftInsert("mylist", RedisCommand.RedisCommandDirection.AFTER, "1", "4");

            Assert.AreEqual("6", count.ToString());

            var str = _command.ListRightPop("mylist");

            Assert.AreEqual("4", str);
        }
    }
}
