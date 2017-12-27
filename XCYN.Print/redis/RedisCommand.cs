using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.redis
{

    
    public class RedisCommand
    {
        private string _conn = "192.168.1.111:6379";

        public static void GetIP()
        {
            var redis = RedisConfig.GetInstance();
            
        }

        #region 通用命令

        public bool Del(string key)
        {
            using(var client = ConnectionMultiplexer.Connect(_conn))
            {
                return client.GetDatabase().KeyDelete(key);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DelBeta(string key)
        {
            return RedisManager.WriteDataBase().KeyDelete(key);
        }

        public long Del(string[] keys)
        {
            var rkeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                rkeys[i] = keys[i];
            }
            using (var client = ConnectionMultiplexer.Connect(_conn))
            {
                return client.GetDatabase().KeyDelete(rkeys);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long DelBeta(string[] keys)
        {
            var rkeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                rkeys[i] = keys[i];
            }
            return RedisManager.WriteDataBase().KeyDelete(rkeys);
            
        }

        #endregion

        #region String命令
        public bool StringSet(string key,string value)
        {
            using (var client = ConnectionMultiplexer.Connect(_conn))
            {
                //KeyValuePair<string, object> dict = new KeyValuePair<string, object>(key,value);
                bool flag = client.GetDatabase().StringSet(key: key, value: value);
                return flag;
            }
        }

        public string StringGet(string key)
        {
            using (var client = ConnectionMultiplexer.Connect(_conn))
            {
                return client.GetDatabase().StringGet(key);
            }
        }

        #endregion

        #region List命令

        public long ListLeftPush(string key, string value)
        {
            using (var client = ConnectionMultiplexer.Connect(_conn))
            {
                return client.GetDatabase().ListLeftPush(key, value);
            }
        }

        public long ListLeftPush(string key, string[] values)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            using (var client = ConnectionMultiplexer.Connect(_conn))
            {
                return client.GetDatabase().ListLeftPush(key, rvalues);
            }
        }

        public string ListLeftPop(string key)
        {
            using(var client = ConnectionMultiplexer.Connect(_conn))
            {
                return client.GetDatabase().ListLeftPop(key);
            }
        }

        /// <summary>
        /// 返回List的长度，如果key不存在返回0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            using (var client = ConnectionMultiplexer.Connect(_conn))
            {
                 return client.GetDatabase().ListLength(key);
            }
        }

        #endregion

    }
}
