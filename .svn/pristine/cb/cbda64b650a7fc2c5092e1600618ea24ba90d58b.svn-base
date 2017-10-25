using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Redis
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.0.110:6379,192.168.0.113:6379,192.168.0.114:6379");

            var db = redis.GetDatabase();

            var batch = db.CreateBatch();

            batch.StringSetAsync("name", "jack");

            batch.HashSetAsync("person", "age", "18");

            batch.SetAddAsync("tag", "china");

            batch.Execute();

            Console.WriteLine("success");

        }
    }
}
