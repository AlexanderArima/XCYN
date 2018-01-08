using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common.NoSql.redis;
using System.Threading;

namespace XCYN.Test
{
    /// <summary>
    /// RedisCommandTest 的摘要说明
    /// </summary>
    [TestClass]
    public class RedisCommandTest
    {

        public RedisCommand _command = new RedisCommand();

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
        
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //初始化字符串
            RedisCommandTest test = new RedisCommandTest();
            test._command.StringSet("myStr", "hello world");

            test._command.StringSet("myCount", "0");
        }

        #endregion

        #region 字符串(String)

        [TestMethod]
        public void StringAppend()
        {
            var len = _command.StringAppend("myStr", "wuhan");

            Assert.AreEqual(16, len);
        }

        [TestMethod]
        public void StringDesc()
        {
            var num = _command.StringDecr("myCount");

            Assert.AreEqual(-1, num);
            
        }

        [TestMethod]
        public void StringDescBy()
        {
            var num = _command.StringDecrBy("myCount",10);

            Assert.AreEqual(-10, num);
        }

        [TestMethod]
        public void StringGet()
        {
            var str = _command.StringGet("myStr");

            Assert.AreEqual("hello world", str);

            var str2 = _command.StringGet(new string[3] { "myStr", "myCount","noExist" });

            Assert.AreEqual(3, str2.Count);
        }

        [TestMethod]
        public void StringGetRange()
        {
            RedisCommand command = new RedisCommand();
            var str = command.StringGetRange("myStr", 0, 3);

            Assert.AreEqual("hell", str);
        }

        [TestMethod]
        public void StringGetSet()
        {
            var str = _command.StringGetSet("myCount", "10");
            Assert.AreEqual("0", str);

            Assert.AreEqual("10", _command.StringGet("myCount"));

        }

        [TestMethod]
        public void StringIncr()
        {
            var count = _command.StringIncr("myCount");

            Assert.AreEqual(1, count);
        }
        
        [TestMethod]
        public void StringIncrBy()
        {
            var count = _command.StringIncrBy("myCount", 10);

            Assert.AreEqual(10, count);
        }

        [TestMethod]
        public void StringIncrByFloat()
        {
            var count = _command.StringIncrBy("myCount", 0.1);

            Assert.AreEqual(0.1, count);
        }

        [TestMethod]
        public void StringSet()
        {
            string[] keys = new string[2]
            {
                "myCount2" ,
                "myCount3"
            };
            string[] values = new string[2]
            {
                "2",
                "3"
            };
            _command.StringSet(keys, values);
            var myCount2 = _command.StringGet("myCount2");
            var myCount3 = _command.StringGet("myCount3");

            Assert.AreEqual("2", myCount2);
            Assert.AreEqual("3", myCount3);
        }

        [TestMethod]
        public void StringSetNX()
        {
            string[] keys = new string[2]
            {
                "key2" ,
                "key3"
            };
            string[] values = new string[2]
            {
                "2",
                "3"
            };
            var flag = _command.StringSetNX(keys, values);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void StringSetEX()
        {
            _command.StringSetEX("key4", 1, "1");

            Thread.Sleep(1000);

            var str = _command.StringGet("key4");

            Assert.IsNull(str);

            _command.StringSetEX("key4", 2, "1");

            Thread.Sleep(1000);

            str = _command.StringGet("key4");

            Assert.IsNotNull(str);
        }

        #endregion

        #region 队列(List)

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
            var count = _command.ListInsert("mylist", RedisCommand.RedisCommandDirection.AFTER, "1", "4");

            Assert.AreEqual("6", count.ToString());

            var str = _command.ListRightPop("mylist");

            Assert.AreEqual("4", str);
        }

        [TestMethod]
        public void ListLeftPushX()
        {
            _command.ListLeftPushX("mylist2", "10");

            var value = _command.ListIndex("mylist2", 0);

            Assert.IsNull(value);

            _command.ListLeftPop("mylist2");
        }

        [TestMethod]
        public void ListRange()
        {
            var list = _command.ListRange("mylist", 0, -1);

            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void ListRemove()
        {
            var list = new string[2] { "4","4" };
            var count = _command.ListLeftPush("mylist", list);

            _command.ListRemove("mylist", 0, "4");
            Assert.AreNotEqual("4", _command.ListIndex("mylist", -1));
        }

        [TestMethod]
        public void ListSetByIndex()
        {
            _command.ListLeftPush("mylist", "1");
            _command.ListSetByIndex("mylist", 0, "7");
            var str = _command.ListIndex("mylist", 0);
            Assert.AreEqual("7", str);
        }

        [TestMethod]
        public void ListTrim()
        {
            _command.ListTrim("mylist", 0, 2);

            var len = _command.ListLength("mylist");

            Assert.AreEqual(3, len);
        }

        [TestMethod]
        public void ListRightPopLeftPush()
        {
            var len = _command.ListLength("mylist");

            var end = _command.ListIndex("mylist", (int)len - 1);

            _command.ListRightPopLeftPush("mylist", "mylist");

            var start = _command.ListIndex("mylist", 0);

            Assert.AreEqual(start, end);
        }

        #endregion

        #region 集合(Set)

        [TestMethod]
        public void SetAdd()
        {
            var myset = _command.SetAdd("myset", "5");

            Assert.AreNotEqual(true, myset);

            var array = new string[2]
            {
                "6",
                "7"
            };
            var myset2 = _command.SetAdd("myset", array);
            Assert.AreEqual(0, myset2);
            
        }

        [TestMethod]
        public void SetLength()
        {
            var len = _command.SetLength("myset");

            Assert.AreEqual(8, (int)len);
        }

        [TestMethod]
        public void SetContains()
        {
            var flag = _command.SetContains("myset", "1");

            Assert.IsTrue(flag);

            flag = _command.SetContains("myset", "100");

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void SetMembers()
        {
            var myset = _command.SetMembers("myset");
            Assert.AreEqual(8, myset.Count);
        }

        #endregion

    }

    
}
