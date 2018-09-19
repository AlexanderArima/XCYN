using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XCYN.Test.Print
{
    [TestClass]
    public class StringTestCase
    {
        /// <summary>
        /// String传给方法的参数，类似于值传递
        /// </summary>
        [TestMethod]
        public void TestReference()
        {
            string s = "";
            Run(s);
            Assert.AreEqual("", s); //s不会变成Ann，虽然string是引用类型，但当它作为一个参数传递时，传递的是一个值，而不是地址
        }

        public void Run(string s)
        {
            s = "Ann";
        }

        /// <summary>
        /// Compare方法返回三值，第一个参数比第二个参数小返回-1，大返回1，相等返回0
        /// </summary>
        [TestMethod]
        public void TestCompare()
        {
            string a = "1";
            string b = "10";
            var r = String.Compare(a, b);
            Assert.AreEqual(-1, r);

            string c = "1";
            r = string.Compare(a, c);
            Assert.AreEqual(0, r);

            r = string.Compare(b, a);
            Assert.AreEqual(1, r);
        }

        /// <summary>
        /// Equals方法返回二值，两个字符串相等返回true，否则返回false
        /// </summary>
        [TestMethod]
        public void TestEquals()
        {
            string a = "1";
            string b = "10";
            string c = "1";
            var r = a.Equals(b);
            Assert.IsFalse(r);
            r = a.Equals(c);
            Assert.IsTrue(r);
        }

        /// <summary>
        /// 字符串连接的三种方式：Concat()，+，Format()
        /// </summary>
        [TestMethod]
        public void TestConcat()
        {
            var r = String.Concat("hello ", "world ");
            Assert.AreEqual("hello world ", r);
            r = "hello " + "world ";
            Assert.AreEqual("hello world ", r);
            r = string.Format("{0}{1}", "hello ", "world ");
            Assert.AreEqual("hello world ", r);
        }

        /// <summary>
        /// Join方法可以将一个集合转成字符串，在编写SQL语句时非常有用
        /// </summary>
        [TestMethod]
        public void TestJoin()
        {
            char[] c = new char[]
            {
                'h','e','l','l','o',' ','w','o','r','l','d'
            };
            var r = string.Join("",c);
            Assert.AreEqual("hello world", r);
        }

    }
}
