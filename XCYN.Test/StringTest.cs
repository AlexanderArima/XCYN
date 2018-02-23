using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XCYN.Test
{
    [TestClass]
    public class StringTest
    {
        /// <summary>
        /// 字符串查找下标
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            string a = "abcdeafgh";
            //查找符合条件的字符或字符串位置
            var index = a.IndexOf("a");
            //在数组中第一个符合条件的字符位置
            var index2 = a.IndexOfAny(new char[] { 'y', 'b' });
        }

        /// <summary>
        /// 字符串插值$
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            int a = 1;
            int b = 2;
            string c = $"{a} + {b} = {a + b}";
            string d = string.Format("{0} + {1} = {2}", a, b, a + b);
        }

        public override string ToString() => $"x:y";
    }

    internal class Person
    {
        public int age;
        internal int age2;
        protected int age3;
        private int age4;
        
    }

    public class Person2
    {

    }

    
}
