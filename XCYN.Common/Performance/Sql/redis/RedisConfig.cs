using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common.Sql.redis
{
    public class RedisConfig
    {
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        public string WriteServerList { get; set; }

        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        public string ReadServerList { get; set; }

        /// <summary>
        /// 最大写链接数
        /// </summary>
        public string MaxWritePoolSize { get; set; }

        /// <summary>
        /// 最大读链接数
        /// </summary>
        public string MaxReadPoolSize { get; set; }

        /// <summary>
        /// 自动重启
        /// </summary>
        public string AutoStart { get; set; }

        private static RedisConfig _redisConfig = null;

        private static object _lock = new object();

        private RedisConfig()
        {
            
        }

        public static RedisConfig GetInstance()
        {
            if(_redisConfig == null)
            {
                lock(_lock)
                {
                    if(_redisConfig == null)
                    {
                        var dict = (IDictionary)ConfigurationManager.GetSection("redis");
                        _redisConfig = new RedisConfig()
                        {
                            WriteServerList = dict.Contains("WriteServerList") ? dict["WriteServerList"].ToString() : "",
                            ReadServerList = dict.Contains("ReadServerList") ? dict["ReadServerList"].ToString() : "",
                            MaxWritePoolSize = dict.Contains("MaxWritePoolSize") ? dict["MaxWritePoolSize"].ToString() : "",
                            MaxReadPoolSize = dict.Contains("MaxReadPoolSize") ? dict["MaxReadPoolSize"].ToString() : "",
                            AutoStart = dict.Contains("AutoStart") ? dict["AutoStart"].ToString() : "",
                        };
                    }
                }
                
            }
            return _redisConfig;
        }

    }
}
