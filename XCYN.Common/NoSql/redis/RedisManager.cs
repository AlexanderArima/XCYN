using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common.NoSql.redis
{
    public class RedisManager
    {

        private static ConnectionMultiplexer _read_instance = null;

        private static ConnectionMultiplexer _write_instance = null;

        private static object _lock = new object();

        private RedisManager()
        {

        }

        /// <summary>
        /// 读取连接实例
        /// </summary>
        /// <returns></returns>
        public static ConnectionMultiplexer GetReadInstance()
        {
            if (_read_instance == null)
            {
                lock(_lock)
                {
                    if(_read_instance == null)
                    {
                        ConfigurationOptions options = new ConfigurationOptions();
                        var config = RedisConfig.GetInstance();
                        var list_host = config.ReadServerList.Split(',');
                        for (int i = 0; i < list_host.Count(); i++)
                        {
                            if (list_host[i].Length > 0)
                            {
                                options.EndPoints.Add(config.ReadServerList);
                            }
                        }
                        _read_instance = ConnectionMultiplexer.Connect(options);
                    }
                }
               
            }
            return _read_instance;
        }

        public static IDatabase ReadDataBase()
        {
            return GetReadInstance().GetDatabase();
        }

        /// <summary>
        /// 写入连接实例
        /// </summary>
        /// <returns></returns>
        public static ConnectionMultiplexer GetWriteInstance()
        {
            if (_write_instance == null)
            {
                lock (_lock)
                {
                    if (_write_instance == null)
                    {
                        var config = RedisConfig.GetInstance();
                        ConfigurationOptions options = new ConfigurationOptions();
                        //获取写服务器的列表
                        var list_host = config.WriteServerList.Split(',');
                        for (int i = 0; i < list_host.Count(); i++)
                        {
                            if(list_host[i].Length > 0)
                            {
                                options.EndPoints.Add(config.WriteServerList);
                            }
                        }
                        _write_instance = ConnectionMultiplexer.Connect(options);
                    }
                }
                
            }
            return _write_instance;
        }

        public static IDatabase WriteDataBase()
        {
            return GetWriteInstance().GetDatabase();
        }
    }
}
