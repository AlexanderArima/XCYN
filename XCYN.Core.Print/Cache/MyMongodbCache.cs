using Microsoft.Extensions.Caching.MongoDB;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCYN.Core.Print.Cache
{
    public class MyMongodbCache
    {
        /// <summary>
        /// 用MongoDB实现分布式缓存，不同的用户用（Session+Key）来标识
        /// </summary>
        public void Fun1()
        {
            MongoDBCache cache = new MongoDBCache(new MongoDBCacheOptions() {
                ConnectionString = "mongodb://192.168.43.136:27017",
                DatabaseName = "MyDB",
                CollectionName = "Students"
            });

            cache.SetString("Name", "Key",new DistributedCacheEntryOptions() {
                AbsoluteExpiration = DateTime.Now.AddSeconds(10)
            });

            var s = cache.GetString("Name");
        }
    }
}
