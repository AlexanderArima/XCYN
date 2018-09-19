using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XCYN.Test.Print
{
    [TestClass]
    public class CharTestCase
    {
        [TestMethod]
        public void TestMethod1()
        {
            char c = 'a';
            Assert.IsTrue(Char.IsLower(c));                //IsLower()判断传入的参数是否为小写字母
            Assert.AreEqual('A', Char.ToUpper(c));   //ToUpper()将参数转成大写字母，如果不是字母则返回传入的值
            char d = 'D';
            Assert.AreEqual('d', Char.ToLower(d));   //ToLower()将参数转成小写字母，如果不是字母则返回传入的值
            Assert.IsTrue(Char.IsUpper(d));              //IsUpper()判断传入的参数是否为大写字母
            char e = '0';
            Assert.IsTrue(Char.IsNumber(e));           //IsNumber()判断传入的参数是否为数字
            //将char转成asc码
            Assert.AreEqual(65, (int)'A');                  //需要记住的是A是65，a是97
            Assert.AreEqual(97, (int)'a');
        }
    }
}
