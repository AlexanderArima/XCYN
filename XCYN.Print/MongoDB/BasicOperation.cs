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
            var collection =  db.GetCollection<Room>("Room");

            //插入数据
            collection.InsertOne(new Room()
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
            MongoHelper helper = new MongoHelper("mongodb://localhost:27017", "School");

            helper.InsertOne<Room>("Room", new Room()
            {
                _id = Guid.NewGuid().ToString(),
                Name = "Jim",
            });

            var list = helper.Find<Room>("Room", m => true);

            foreach (var item in list)
            {
                Console.WriteLine("_id:{0} \nName:{1}", item._id, item.Name);
            }
        }
    }

    

    public class Room
    {
        public string _id { get; set; }
        public string Name { get; set; }
    }
}
