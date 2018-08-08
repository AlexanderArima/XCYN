using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
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

        [TestMethod]
        public void TestUpdateOne()
        {
           var s = _helper.UpdateOne<Employee>("T_Employee", m => m.Name == "Money", "{$set: { Name: 'Jiao',Country: 'CHI'} }");
        }

        [TestMethod]
        public void TestUpdateOne2()
        {
            var obj = new
            {
                Name = "Zhu"
            };
            var s = _helper.UpdateOne<Employee>("T_Employee", m => m.Name == "Money", obj);
            Assert.AreEqual(1, s.MatchedCount);
        }

        [TestMethod]
        public async Task TestUpdateOneAsync()
        {
            var s = await _helper.UpdateOneAsync<Employee>("T_Employee", m => m.Name == "Zhu", "{$set: { Country: 'CHI'} }");
            Assert.AreEqual(1, s.ModifiedCount);
        }

        [TestMethod]
        public async Task TestUpdateOneAsync2()
        {
            var obj = new
            {
                Country = "GER"
            };
            var s = await _helper.UpdateOneAsync<Employee>("T_Employee", m => m.Name == "Kong", obj);
            Assert.AreEqual(1, s.ModifiedCount);
        }

        [TestMethod]
        public void TestUpdateMany()
        {
            _helper.InsertOne<Employee>("T_Employee", new Employee() {
                _id = ObjectId.GenerateNewId(),
                Name = "Kong",
                Country = "USA"
            });
            var s = _helper.UpdateMany<Employee>(
                "T_Employee",
                m => m.Name == "Kong", 
                "{$set: {Country: 'CHI'} }");
            Assert.AreEqual(2, s.ModifiedCount);
        }

        [TestMethod]
        public void TestUpdateMany2()
        {
            var obj = new
            {
                Country = "USA"
            };
            var s = _helper.UpdateMany<Employee>(
                "T_Employee",
                m => m.Name == "Kong",
                obj);
            Assert.AreEqual(2, s.ModifiedCount);
        }

        [TestMethod]
        public async Task TestUpdateAsyncMany()
        {
            var s = await _helper.UpdateManyAsync<Employee>(
                "T_Employee",
                m => m.Name == "Kong",
                "{$set: {Country: 'CHI'} }");
            Assert.AreEqual(2, s.ModifiedCount);
        }

        [TestMethod]
        public async Task TestUpdateAsyncMany2()
        {
            var obj = new
            {
                Country = "USA"
            };
            var s = await _helper.UpdateManyAsync<Employee>(
                "T_Employee",
                m => m.Name == "Kong",
                obj);
            Assert.AreEqual(2, s.ModifiedCount);
        }

        [TestMethod]
        public void TestFind()
        {
            var list = _helper.Find<Employee>("T_Employee", m => m.Country == "USA");
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public async Task TestFindAsync()
        {
            var list = await _helper.FindAsync<Employee>("T_Employee", m => m.Country == "USA");
            Assert.AreEqual(6, list.ToList().Count);

        }

        [TestMethod]
        public void TestCount()
        {
            var num = _helper.Count<Employee>("T_Employee", m => m.Country == "CHI");
            Assert.AreEqual(2, num);
        }

        [TestMethod]
        public async Task TestCountAsync()
        {
            var num = await _helper.CountAsync<Employee>("T_Employee", m => m.Country == "CHI");
            Assert.AreEqual(2, num);
        }

        [TestMethod]
        public void TestFindOneAndDelete()
        {
            var obj = _helper.FindOneAndDelete<Employee>("T_Employee", m => m.Country == "CHI");
            Assert.AreEqual("CHI", obj.Country);
        }

        [TestMethod]
        public async Task TestFindOneAndDeleteAsync()
        {
            var obj = await _helper.FindOneAndDeleteAsync<Employee>("T_Employee", m => m.Country == "CHI");
            Assert.AreEqual("CHI", obj.Country);
        }

        [TestMethod]
        public void TestFindOneAndUpdate()
        {
            var upd = new
            {
                Country = "CHI"
            };
            var obj = _helper.FindOneAndUpdate<Employee>("T_Employee", m => m.Name == "Kong", upd);
            Assert.AreEqual("USA", obj.Country);
        }

        [TestMethod]
        public void TestFindOneAndUpdate2()
        {
            var upd = "{$set:{Country:'CHI'}}";
            var obj = _helper.FindOneAndUpdate<Employee>("T_Employee", m =>( m.Name == "Kong" && m.Country == "USA"), upd);
            Assert.AreEqual("USA", obj.Country);
        }

        [TestMethod]
        public async Task TestFindOneAndUpdateAsync()
        {
            var upd = new
            {
                Country = "CHI"
            };
            var obj = await _helper.FindOneAndUpdateAsync<Employee>("T_Employee", m => m.Name == "Money", upd);
            Assert.AreEqual("GER", obj.Country);
        }

        [TestMethod]
        public async Task TestFindOneAndUpdateAsync2()
        {
            var upd = "{$set:{Country:'GER'}}";
            var obj = await _helper.FindOneAndUpdateAsync<Employee>("T_Employee", m => (m.Name == "Money"), upd);
            Assert.AreEqual("CHI", obj.Country);
        }

        [TestMethod]
        public void TestCreateCollection()
        {
            _helper.DropCollection("T_User");
            _helper.CreateCollection("T_User");
            var flag = _helper.GetListCollectionNames().Find(m => m.Contains("T_User"));
            Assert.IsNotNull(flag);
        }

        [TestMethod]
        public async Task TestCreateColletionAsync()
        {
            _helper.DropCollection("T_User");
            await _helper.CreateCollectionAsync("T_User").ContinueWith((obj)=> {
                var flag = _helper.GetListCollectionNames().Find(m => m.Contains("T_User"));
                Assert.IsNotNull(flag);
            });
        }

        [TestMethod]
        public async Task TestDropCollectionAsync()
        {
            await _helper.DropCollectionAsync("T_User").ContinueWith((obj) => {
                _helper.CreateCollectionAsync("T_User").ContinueWith((obj2) => {
                    var flag = _helper.GetListCollectionNames().Find(m => m.Contains("T_User"));
                    Assert.IsNotNull(flag);
                });
            });
        }

        [TestMethod]
        public void TestGetListCollectionName()
        {
            var flag = _helper.GetListCollectionNames().Find(m => m.Contains("T_Custom"));
            Assert.IsNull(flag);
            flag = _helper.GetListCollectionNames().Find(m => m.Contains("T_Employee"));
            Assert.IsNotNull(flag);
        }

        [TestMethod]
        public async Task TestGetListCollectionNameAsync()
        {
            await _helper.GetListCollectionNamesAsync().ContinueWith((obj)=> {
                var flag = obj.Result.ToList().Find(m => m.Contains("T_Custom"));
                Assert.IsNull(flag);
            });
            await _helper.GetListCollectionNamesAsync().ContinueWith((obj) => {
                var flag = obj.Result.ToList().Find(m => m.Contains("T_Employee"));
                Assert.IsNotNull(flag);
            });
        }

        [TestMethod]
        public void TestRenameCollection()
        {
            var flag = _helper.GetListCollectionNames().Find(m => m.Contains("T_Custom"));
            if(flag == null)
            {
                _helper.CreateCollection("T_Custom");
            }
            _helper.RenameCollection("T_Custom","Custom");
            flag = _helper.GetListCollectionNames().Find(m => m.Contains("Custom"));
            Assert.IsNotNull("Custom");
        }

        [TestMethod]
        public async Task TestRenameCollectionAsync()
        {
            var flag = _helper.GetListCollectionNames().Find(m => m.Contains("T_Custom"));
            if (flag == null)
            {
                _helper.CreateCollection("T_Custom");
            }
            var flag2 = _helper.GetListCollectionNames().Find(m => m.Contains("Custom"));
            if(flag2 != null)
            {
                _helper.DropCollection("Custom");
            }
            await _helper.RenameCollectionAsync("T_Custom", "Custom").ContinueWith((obj)=> {
                flag = _helper.GetListCollectionNames().Find(m => m.Contains("Custom"));
                Assert.IsNotNull("Custom");
            });
            
        }

        [TestMethod]
        public void TestGetListCollections()
        {
            var list = _helper.GetListCollections();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                foreach (var elem in item.Elements)
                {
                    if(elem.Value == "T_Employee")
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestGetListCollectionsAsync()
        {
            _helper.GetListCollectionsAsync().ContinueWith((obj)=> {
                var list = obj.Result.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    foreach (var elem in item.Elements)
                    {
                        if (elem.Value == "T_Employee")
                        {
                            Assert.IsTrue(true);
                            return;
                        }
                    }
                }
                Assert.IsTrue(false);
            });
        }
        

    }
}
