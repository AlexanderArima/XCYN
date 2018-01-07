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

    
    class RedisCommand
    {

        public static void GetIP()
        {
            var redis = RedisConfig.GetInstance();
        }

        #region Del命令

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Del(string key)
        {
            return RedisManager.WriteDataBase().KeyDelete(key);
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long Del(string[] keys)
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

        /// <summary>
        /// 返回key的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value)
        {
            bool flag = RedisManager.WriteDataBase().StringSet(key: key, value: value);
            return flag;
        }

        public string StringGet(string key)
        {
            return RedisManager.ReadDataBase().StringGet(key);
        }

        #endregion

        #region List命令

        /// <summary>
        /// 从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListLeftPush(string key, string value)
        {
            return RedisManager.WriteDataBase().ListLeftPush(key, value);
        }

        /// <summary>
        /// 从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="values">插入的值数组</param>
        /// <returns></returns>
        public long ListLeftPush(string key, string[] values)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            return RedisManager.WriteDataBase().ListLeftPush(key, rvalues);
        }

        /// <summary>
        /// 往队列左边出队一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListLeftPop(string key)
        {
            return RedisManager.WriteDataBase().ListLeftPop(key);
        }

        /// <summary>
        /// 返回List的长度，如果key不存在返回0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return RedisManager.ReadDataBase().ListLength(key);
        }

        /// <summary>
        /// 获取指定下标的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ListIndex(string key, int index)
        {
            return RedisManager.ReadDataBase().ListGetByIndex(key, index);
        }

        #endregion

    }
}
