using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using XCYN.Print.MongoDB;

namespace XCYN.Test.Print.MongoDB
{
    [TestClass]
    public class MongoHelperTest
    {
        MongoHelper _helper = null;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _helper = new MongoHelper("mongodb://localhost:27017", "Test");
        }

        [TestMethod]
        public void TestMethod1()
        {
            string f = @"E:\download\ASP.NETWebAPI_jb51.rar";
            MongoHelper helper = new MongoHelper("MyFS");
            var obj = helper.FilePut(f);
            Assert.AreNotEqual(0, obj);
        }

        [TestMethod]
        public void TestDelete()
        {
            string f = "5b50314041b99b2ff027451b";
            MongoHelper helper = new MongoHelper("MyFS");
            helper.FileDelete(f);
        }

        [TestMethod]
        public void TestGet()
        {
            string f = "5b5028b63af9150c200c6e7c";
            MongoHelper helper = new MongoHelper("MyFS");
            FileStream stream = new FileStream(@"C:\2.exe", FileMode.Create);
            helper.FileGet(f, stream);
        }

        [TestMethod]
        public void TestInsertOne()
        {
            _helper.InsertOne("T_Employee", new Employee()
            {
                _id = ObjectId.GenerateNewId(),
                Name = "Jack",
                Country = "USA"
            });

            var list = _helper.Find<Employee>("T_Employee", m => true);
            
        }

        [TestMethod]
        public void TestInsertMany()
        {
            List<Employee> list = new List<Employee>();
            Employee emp = new Employee();
            emp._id = ObjectId.GenerateNewId();
            emp.Name = "Lucy";
            emp.Country = "USA";
            list.Add(emp);
            Employee emp2 = new Employee();
            emp2._id = ObjectId.GenerateNewId();
            emp2.Name = "Hanson";
            emp2.Country = "GER";
            list.Add(emp2);
            _helper.InsertMany("T_Employee",list);
        }
    }
}
