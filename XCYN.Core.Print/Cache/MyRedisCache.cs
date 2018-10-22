using Microsoft.Extensions.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XCYN.Core.Print.Cache
{
    public class MyRedisCache
    {

        /// <summary>
        /// Redis实现分布式缓存
        /// </summary>
        public void Fun1()
        {
            RedisCache cache = new RedisCache(new RedisCacheOptions()
            {
                Configuration = "192.168.43.136" ,
                InstanceName = "Test"
            });

            cache.Set("Name", Encoding.UTF8.GetBytes("铁路运输"), new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10)
            });
            //Thread.Sleep(5000);
            var b = cache.Get("Name");
            if(b != null)
            {
                var s = Encoding.UTF8.GetString(b);
                Console.WriteLine("Name:" + s);
            }
            else
            {
                Console.WriteLine("获取不到Name缓存");
            }

            
        }
    }
}
