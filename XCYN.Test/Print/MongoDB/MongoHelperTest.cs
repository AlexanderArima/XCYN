using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        public async Task TestInsertOneAsync()
        {
            await _helper.InsertOneAsync("T_Employee", new Employee()
            {
                _id = ObjectId.GenerateNewId(),
                Name = "Arima",
                Country = "JAP"
            });
        }

        [TestMethod]
        public async Task TestInsertManyAsync()
        {
            await _helper.InsertManyAsync("T_Employee", new List<Employee> {
                new Employee(ObjectId.GenerateNewId(),"Nex","USA"),
                new Employee(ObjectId.GenerateNewId(),"Money","GER"),
            });
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

        [TestMethod]
        public void TestDeleteOne()
        {
            var count = _helper.DeleteOne<Employee>("T_Employee", (m) => m.Name == "Arima");
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public async Task TestDeleteOneAsync()
        {
            var num = await _helper.DeleteOneAsync<Employee>("T_Employee", m => m.Name == "Nex");
            Assert.AreEqual(1, num.DeletedCount);
        }

        [TestMethod]
        public void TestDeleteMany()
        {
            var count = _helper.DeleteMany<Employee>("T_Employee", (m) => m.Name == "Arima");
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public async Task TestDeleteManyAsync()
        {
            var count = await _helper.DeleteManyAsync<Employee>("T_Employee", m => m.Name == "Nex");
            Assert.AreEqual(3, count.DeletedCount);
        }
    }
}
