using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Print.DesignPattern.StaticFactoryMethod;
using XCYN.Print.DesignPattern.Prototype.Deep;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace XCYN.Test
{
    /// <summary>
    /// DesignPattern 的摘要说明
    /// </summary>
    [TestClass]
    public class DesignPattern
    {

        [TestMethod]
        public void TestMethod1()
        {
            Handler user = new Handler();
            user.Insert();
        }

        [TestMethod]
        public void MyTestMethod()
        {
            XCYN.Print.DesignPattern.TemplateMethod.Handler handler = new XCYN.Print.DesignPattern.TemplateMethod.Handler();
            handler.HanderQuestion();

        }

        /// <summary>
        /// 原型模式
        /// </summary>
        [TestMethod]
        public void PrototypeMethod()
        {
            //深拷贝
            Person p = new Person()
            {
                name = "张三",
                age = 18,
                address = new Address()
                {
                    province = "湖北",
                    city = "武汉"
                }
            };
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, p);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化
                var p2 = (Person)bf.Deserialize(ms);
            }


        }
    }
}
