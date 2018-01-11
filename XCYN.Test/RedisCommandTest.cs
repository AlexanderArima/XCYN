using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common.NoSql.redis;
using System.Threading;
using System.Collections;

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

        /// <summary>
        /// 在运行每个测试之前，使用 TestInitialize 来运行代码
        /// </summary>
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //初始化字符串
            RedisCommandTest test = new RedisCommandTest();
            test._command.StringSet("myStr", "hello world");

            test._command.StringSet("myCount", "0");

            test._command.KeyDelete("myList");
            List<string> list = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                list.Add(i.ToString());
            }
            test._command.ListLeftPush("myList", list.ToArray());

            test._command.KeyDelete("mySet");
            test._command.SetAdd("mySet", list.ToArray());

            List<string> list2 = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                list2.Add(i.ToString());
            }
            test._command.KeyDelete("mySet2");
            test._command.SetAdd("mySet2", list2.ToArray());

            test._command.KeyDelete("mySet3");

            test._command.KeyDelete("mySet4");
            list = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i.ToString());
            }
            test._command.SetAdd("mySet4", list.ToArray());

            test._command.KeyDelete("myHash");
            test._command.HashSet("myHash", "name", "w");
            test._command.HashSet("myHash", "age", "1");
            test._command.HashSet("myHash", "year", "2017");
            test._command.HashSet("myHash", "month", "1");

            _command.KeyDelete("mySortedSet");
            var flag = _command.SortedSetAdd("mySortedSet", new string[] { "wuhan", "hangzhou" ,"beijing"}, new int[] { 1, 2, 3 });
        }

        #endregion

        #region 键(key)

        [TestMethod]
        public void KeyExists()
        {
            var str = _command.KeyExists("myStr");
            Assert.IsTrue(str);
        }

        [TestMethod]
        public void KeyExpire()
        {
            var flag = _command.KeyExpire("myStr", 10);
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void KeyExpire2()
        {
            var flag2 = _command.KeyExpireAt("myStr", DateTime.Now.AddSeconds(20).Ticks);
            Assert.IsTrue(flag2);
        }

        [TestMethod]
        public void Keys()
        {
            var keys = _command.Keys("*");
            Assert.AreEqual(9, keys.Count);
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

        [TestMethod]
        public void StringSetRange()
        {
            var len = _command.StringSetRange("myStr", 6, "Redis");

            Assert.AreEqual(11, len);

            var myStr = _command.StringGet("myStr");

            Assert.AreEqual("hello Redis", myStr);
        }

        [TestMethod]
        public void StringLength()
        {
            var len = _command.StringLength("myStr");

            Assert.AreEqual(11, len);
        }

        #endregion

        #region 队列(List)

        [TestMethod]
        public void ListIndex()
        {
            var str = _command.ListIndex("myList", 6);

            Assert.AreEqual("0", str);
        }

        [TestMethod]
        public void ListLeftInsert()
        {
            var count = _command.ListInsert("myList", RedisCommand.RedisCommandDirection.AFTER, "1", "123");

            Assert.AreEqual(8, count);

            var str = _command.ListRightPop("myList");

            Assert.AreEqual("0", str);
        }

        [TestMethod]
        public void ListLeftPushX()
        {
            _command.ListLeftPushX("myList", "10");

            var value = _command.ListIndex("myList", 0);

            Assert.AreEqual("10", value);
        }

        [TestMethod]
        public void ListRange()
        {
            var list = _command.ListRange("myList", 0, -1);

            Assert.AreEqual(7, list.Count);
        }

        [TestMethod]
        public void ListRemove()
        {
            _command.ListRemove("myList", "4", 1);
            Assert.AreEqual(6, _command.ListLength("myList"));
        }

        [TestMethod]
        public void ListSetByIndex()
        {
            _command.ListSetByIndex("myList", 0, "8");
            var str = _command.ListIndex("myList", 0);
            Assert.AreEqual("8", str);
        }

        [TestMethod]
        public void ListTrim()
        {
            _command.ListTrim("myList", 0, 2);

            var len = _command.ListLength("myList");

            Assert.AreEqual(3, len);
        }

        [TestMethod]
        public void ListRightPopLeftPush()
        {
            var len = _command.ListLength("myList");

            var end = _command.ListIndex("myList", (int)len - 1);

            _command.ListRightPopLeftPush("myList", "myList");

            var start = _command.ListIndex("myList", 0);

            Assert.AreEqual(start, end);
        }

        #endregion

        #region 集合(Set)

        [TestMethod]
        public void SetAdd()
        {
            //Set不能有重复的元素
            var mySet = _command.SetAdd("mySet", "5");

            Assert.AreNotEqual(true, mySet);

            //有一个元素重复，则插入一条
            var array = new string[2]
            {
                "6",
                "7"
            };
            var mySet2 = _command.SetAdd("mySet", array);
            Assert.AreEqual(1, mySet2);
            
        }

        [TestMethod]
        public void SetDiff()
        {
            var list = _command.SetDiff("mySet", "mySet2");

            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void SetDiff2()
        {
            string[] list_key = new string[2]
            {
                "mySet",
                "mySet2"
            };

            var list = _command.SetDiff(list_key);

            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void SetDiffStore()
        {
            var num = _command.SetDiffStore("mySet", "mySet2", "mySet3");

            Assert.AreEqual(4, num);
        }

        [TestMethod]
        public void SetDiffStore2()
        {
            string[] list_key = new string[2]
           {
                "mySet",
                "mySet2"
           };
            var num = _command.SetDiffStore(list_key, "mySet3");

            Assert.AreEqual(4, num);
        }

        [TestMethod]
        public void SetInter()
        {
            var list = _command.SetInter("mySet", "mySet2");

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void SetInter2()
        {
            string[] list_key = new string[2]
            {
                "mySet",
                "mySet2"
            };

            var list = _command.SetInter(list_key);

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void SetInterStore()
        {
            var num = _command.SetInterStore("mySet", "mySet2", "mySet3");

            Assert.AreEqual(3, num);
        }

        [TestMethod]
        public void SetInterStore2()
        {
            string[] list_key = new string[2]
           {
                "mySet",
                "mySet2"
           };
            var num = _command.SetInterStore(list_key, "mySet3");

            Assert.AreEqual(3, num);
        }

        [TestMethod]
        public void SetLength()
        {
            var len = _command.SetLength("mySet");

            Assert.AreEqual(7, (int)len);
        }

        [TestMethod]
        public void SetContains()
        {
            var flag = _command.SetContains("mySet", "1");

            Assert.IsTrue(flag);

            flag = _command.SetContains("mySet", "100");

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void SetMembers()
        {
            var mySet = _command.SetMembers("mySet");
            Assert.AreEqual(7, mySet.Count);
        }

        [TestMethod]
        public void SetMove()
        {
            var flag = _command.SetMove("mySet", "mySet2", "3");
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void SetPop()
        {
            var value = _command.SetPop("mySet");
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void SetRandMember()
        {
            var str = _command.SetRandMember("mySet");
            var flag = _command.SetContains("mySet", str);
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void SetRandMembers()
        {
            var list = _command.SetRandMembers("mySet", 2);
            foreach (var item in list)
            {
                var flag = _command.SetContains("mySet", item.ToString());
                Assert.IsTrue(flag);
            }
        }

        [TestMethod]
        public void SetRemove()
        {
            var flag = _command.SetRemove("mySet", "0");
            Assert.IsTrue(flag);

            flag = _command.SetRemove("mySet", "9");
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void SetRemove2()
        {
            var flag = _command.SetRemove("mySet", new string[] { "0","1" });
            Assert.IsTrue(flag > 0);
        }

        [TestMethod]
        public void SetUnion()
        {
            var list = _command.SetUnion("mySet", "mySet2");
            Assert.AreEqual(7, list.Length);
        }

        [TestMethod]
        public void SetUnion2()
        {
            var list = _command.SetUnion(new string[] { "mySet", "mySet2" });
            Assert.AreEqual(7, list.Length);
        }

        [TestMethod]
        public void SetUnionStore()
        {
            var len = _command.SetUnionStore("mySet3", "mySet", "mySet2");
            Assert.AreEqual(7, len);
           
        }

        [TestMethod]
        public void SetUnionStore2()
        {
            var len = _command.SetUnionStore("mySet3", new string[] { "mySet", "mySet2" });
            Assert.AreEqual(7, len);

        }

        [TestMethod]
        public void SetScan()
        {
            var list = _command.SetScan("mySet4", "*", 10);
            Assert.AreEqual(100000, list.Length);
        }

        [TestMethod]
        public void SetScan2()
        {
            var list = _command.SetScan("mySet4", "*", 10,0);
            Assert.AreEqual(100000, list.Length);
        }

        #endregion

        #region 哈希表

        [TestMethod]
        public void HashDelete()
        {
            var flag = _command.HashDelete("myHash", "age");
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void HashDelete2()
        {
            var len = _command.HashDelete("myHash", new string[] { "age", "year" });
            Assert.AreEqual(2, (int)len);
        }

        [TestMethod]
        public void HashExists()
        {
            var flag = _command.HashExists("myHash", "age");
            Assert.IsTrue(flag);

            flag = _command.HashExists("myHash", "birthday");
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void HashIncrement()
        {
            var num = _command.HashIncrement("myHash", "age", 1);
            Assert.AreEqual(2, num);

            num = _command.HashIncrement("myHash", "id", 1);
            Assert.AreEqual(1, num);

            var num2 = _command.HashIncrement("myHash", "age", 0.1);
            Assert.AreEqual(2.1, num2);
        }

        [TestMethod]
        public void HashKeys()
        {
            var keys = _command.HashKeys("myHash");
            Assert.AreEqual(4, keys.Length);
        
        }

        [TestMethod]
        public void HashLength()
        {
            var len = _command.HashLength("myHash");
            Assert.AreEqual(4, len);
        }

        [TestMethod]
        public void HashSetNX()
        {
            var flag = _command.HashSetNX("myHash", "year", "2012");
            Assert.IsTrue(flag);

            flag = _command.HashSetNX("myHash", "birthday", "2012");
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void HashGet()
        {
            var age = _command.HashGet("myHash", "age");
            Assert.AreEqual("1", age);
        }

        [TestMethod]
        public void HashGetAll()
        {
            var dict = _command.HashGetAll("myHash");
            Assert.AreEqual("1", dict["age"]);
        }

        [TestMethod]
        public void HashSet()
        {
            var flag = _command.HashSet("myHash", "name", "xie");
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void HashSet2()
        {
            string[] field = new string[]
            {
                "name","birthday"
            };
            string[] values = new string[]
            {
                "xie","1990-04-24"
            };
            _command.HashSet("myHash", field, values);
            var name = _command.HashGet("myHash", "name");
            Assert.AreEqual("xie", name);
            var birthday = _command.HashGet("myHash", "birthday");
            Assert.AreEqual("1990-04-24", birthday);
        }

        [TestMethod]
        public void HashSet3()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", "xie");
            dict.Add("birthday", "1990-04-24");
            _command.HashSet("myHash", dict);
            var name = _command.HashGet("myHash", "name");
            Assert.AreEqual("xie", name);
            var birthday = _command.HashGet("myHash", "birthday");
            Assert.AreEqual("1990-04-24", birthday);
        }

        [TestMethod]
        public void HashValues()
        {
            var values = _command.HashValues("myHash");
            Assert.AreEqual(4, values.Length);
        }

        [TestMethod]
        public void HashScan()
        {
            var list = _command.HashScan("myHash", "*", 10);
            Assert.AreEqual(4, list.Count);
        }
        #endregion

        #region 有序集合

        [TestMethod]
        public void SortedSetAdd()
        {
            var flag = _command.SortedSetAdd("mySortedSet", new string[] { "wuhan","hangzhou" }, new int[] { 1,2 });
            Assert.IsTrue(flag == 0);
        }

        [TestMethod]
        public void SortedSetLength()
        {
            var len = _command.SortedSetLength("mySortedSet");
            Assert.AreEqual(3, len);

            len = _command.SortedSetLength("mySchool");
            Assert.AreEqual(0, len);
        }

        [TestMethod]
        public void SortedSetCount()
        {
            var len = _command.SortedSetCount("mySortedSet", 0, 100);
            Assert.AreEqual(3, len);

            len = _command.SortedSetCount("mySortedSet", 0, 2);
            Assert.AreEqual(2, len);
        }

        [TestMethod]
        public void SortedSetIncrement()
        {
            var score =_command.SortedSetIncrBy("mySortedSet", "wuhan", 100);
            Assert.AreEqual(101, score);

            score = _command.SortedSetIncrBy("mySortedSet", "tianjing", 100);
            Assert.AreEqual(100, score);
        }

        #endregion
    }
}
