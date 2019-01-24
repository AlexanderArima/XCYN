using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common.Sql.redis
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
        /// 获取只读库
        /// </summary>
        /// <param name="db">库ID</param>
        /// 数据库的数量是可以配置的，默认情况下是16个。修改redis.conf下的databases指令：databases 64
        /// <returns></returns>
        public static IDatabase ReadDataBase(int db)
        {
            return GetReadInstance().GetDatabase(db);
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

        /// <summary>
        /// 获取只写库
        /// </summary>
        /// <param name="db">库ID</param>
        /// 数据库的数量是可以配置的，默认情况下是16个。修改redis.conf下的databases指令：databases 64
        /// <returns></returns>
        public static IDatabase WriteDataBase(int db)
        {
            return GetWriteInstance().GetDatabase(db);
        }

        /// <summary>
        /// 查找所有符合给定模式 pattern 的 key 。
        /// </summary>
        /// <returns></returns>
        public static ArrayList Keys(string key)
        {
            //迭代所有的节点
            var list_endpoint = GetReadInstance().GetEndPoints();
            ArrayList list_result = new ArrayList();
            for (int i = 0; i < list_endpoint.Length; i++)
            {
                ConfigurationOptions options = new ConfigurationOptions();
                options.EndPoints.Add(list_endpoint.ElementAt(i));
                var conn = ConnectionMultiplexer.Connect(options);
                var list_keys = conn.GetServer(list_endpoint.ElementAt(i)).Keys(pattern:key);
                //迭代所有的keys
                for (int j = 0; j < list_keys.Count(); j++)
                {
                    list_result.Add(list_keys.ElementAt(j));
                }
            }
            return list_result;
        }

        /// <summary>
        /// 查找所有符合给定模式 pattern 的 key 。
        /// </summary>
        /// <returns></returns>
        public static ArrayList Keys(string key,int db)
        {
            //迭代所有的节点
            var list_endpoint = GetReadInstance().GetEndPoints();
            ArrayList list_result = new ArrayList();
            for (int i = 0; i < list_endpoint.Length; i++)
            {
                ConfigurationOptions options = new ConfigurationOptions();
                options.EndPoints.Add(list_endpoint.ElementAt(i));
                var conn = ConnectionMultiplexer.Connect(options);
                var list_keys = conn.GetServer(list_endpoint.ElementAt(i)).Keys(pattern: key);
                //迭代所有的keys
                for (int j = 0; j < list_keys.Count(); j++)
                {
                    list_result.Add(list_keys.ElementAt(j));
                }
            }
            return list_result;
        }
    }
}
