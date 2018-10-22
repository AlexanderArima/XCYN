using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Core.Print.Cache
{
    public class MyCache
    {
        /// <summary>
        /// 设置过期时间
        /// </summary>
        public void Fun1()
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = 100
            });

            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(1);
                cache.Set(i, i,new MemoryCacheEntryOptions() {
                    Size = 5
                });
                Console.WriteLine(cache.Count);
            }
        }

        /// <summary>
        /// 缓存释放后回调函数
        /// </summary>
        public void Fun2()
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10),
            };
            cacheOptions.RegisterPostEvictionCallback((key, value, reason, obj) => {
                //key表示缓存的键名，value表示缓存的值，reason表示过期原因，obj表示传入的参数
                Console.WriteLine("缓存已释放");
            });
            cache.Set(1, 1, cacheOptions);
            Console.WriteLine("已设置缓存，10s后过期");
            while (true)
            {
                cache.Get(1);
            }
           
        }

        /// <summary>
        /// 被动过期，设置固定时间过期
        /// </summary>
        public void Fun3()
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            string[] str = new string[] { "Apple", "Orange", "Peach" };
            cache.Set(1, str, DateTimeOffset.Now.AddSeconds(5));
            while (true)
            {
                var arr = cache.Get(1) as string[];
                if(arr != null)
                {
                    Thread.Sleep(1000);
                    foreach (var item in arr)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("缓存已过期");
                    break;
                }
            }
        }

        /// <summary>
        /// 主动过期
        /// </summary>
        public void Fun4()
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("UserName", "Cheng");
            //设置取消标记
            CancellationTokenSource token = new CancellationTokenSource();
            //cancellationToken.CancelAfter(5);
            MemoryCacheEntryOptions entryOptions = new MemoryCacheEntryOptions();
            //注册过期标记
            entryOptions.AddExpirationToken(new CancellationChangeToken(token.Token));
            entryOptions.RegisterPostEvictionCallback((key, value, reason, obj) => {
                Console.WriteLine("用户名过期了");
            });
            cache.Set("1", dict, entryOptions);

            //5s后过期
            var i = 1;
            while (i <= 5)
            {
                var d = cache.Get("1") as Dictionary<string,string>;
                Console.WriteLine(d["UserName"]);
                Thread.Sleep(1000);
                i++;
            }
            token.Cancel();
        }

        /// <summary>
        /// 如果获取不到，则创建
        /// </summary>
        public void Fun5()
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
       
        }


        
    }
}
