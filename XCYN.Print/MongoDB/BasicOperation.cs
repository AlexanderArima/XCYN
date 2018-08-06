using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MongoDB
{
    public class BasicOperation
    {
        public void Fun1()
        {
            //获取一个连接
            var client = new MongoClient("mongodb://localhost:27017");

            //获取一个数据库
            var db = client.GetDatabase("School");
            
            //获取一个集合
            var collection =  db.GetCollection<Employee>("Room");

            //插入数据
            collection.InsertOne(new Employee()
            {
                Name = "Zhong",
            });

            var list = collection.Find(m => true).ToList();
            foreach (var item in list)
            {
                Console.WriteLine("_id:{0},Name:{1}",item._id,item.Name);
            }
        }

        /// <summary>
        /// 调用MongoHelper
        /// </summary>
        public void Fun2()
        {
            MongoHelper helper = new MongoHelper("mongodb://localhost:27017", "Test");
            
            helper.InsertOne("T_Employee", new Employee()
            {
                _id = ObjectId.GenerateNewId(),
                Name = "JimCheng",
                Country = "USA"
            });

            var list = helper.Find<Employee>("T_Employee", m => true);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        public void Fun3()
        {
            //var objectID1 = new ObjectId();
            //byte[] bytes = new byte[] { 0x01, 0x02, 0x03, 0x01, 0x02, 0x03, 0x01, 0x02, 0x03, 0x01, 0x02, 0x03 };
            //var objectID2 = new ObjectId(bytes);
            //string str = "123";
            //var objectID3 = new ObjectId(str);
            //var objectID4 = new ObjectId(new DateTime(123443111),1,1,1);
            //var objectID5 = new ObjectId(1,1,1,1);
            var newid = ObjectId.GenerateNewId();
        }

        public void Fun4()
        {
            MongoHelper _helper = new MongoHelper("mongodb://localhost:27017", "Test");
            var res = _helper.InsertOneAsync("T_Employee", new Employee()
            {
                _id = ObjectId.GenerateNewId(),
                Name = "Arima",
                Country = "JAP"
            });
            res.ContinueWith((s) => {
                Console.WriteLine("调用完成");
            });
        }
    }
    
    public class Employee
    {
        public Employee() { }
        public Employee(ObjectId _id,string Name,string Country)
        {
            this._id = _id;
            this.Name = Name;
            this.Country = Country;
        }

        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public override string ToString()
        {
            return string.Format("_id:{0},Name:{1},Country:{2}",_id,Name,Country);
        }
    }

    public class Employee2
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
