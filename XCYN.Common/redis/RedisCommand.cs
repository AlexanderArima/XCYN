using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common.Sql.redis
{
    
    public class RedisCommand
    {

        #region Key命令

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            return RedisManager.WriteDataBase().KeyDelete(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db">库ID</param>
        /// <returns></returns>
        public bool KeyDelete(string key,int db)
        {
            return RedisManager.WriteDataBase(db).KeyDelete(key);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long KeyDelete(string[] keys)
        {
            var rkeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                rkeys[i] = keys[i];
            }
            return RedisManager.WriteDataBase().KeyDelete(rkeys);
            
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="db">库ID</param>
        /// <returns></returns>
        public long KeyDelete(string[] keys,int db)
        {
            var rkeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                rkeys[i] = keys[i];
            }
            return RedisManager.WriteDataBase(db).KeyDelete(rkeys);

        }

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return RedisManager.ReadDataBase().KeyExists(key);
        }

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db">库ID</param>
        /// <returns></returns>
        public bool KeyExists(string key,int db)
        {
            return RedisManager.ReadDataBase(db).KeyExists(key);
        }

        /// <summary>
        /// 查找所有符合pattern的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ArrayList Keys(string key)
        {
            return RedisManager.Keys(key);
        }
        
        /// <summary>
        /// 为给定 key 设置生存时间，当 key 过期时(生存时间为 0 )，它会被自动删除。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">过期时间(秒)</param>
        /// <returns></returns>
        public bool KeyExpire(string key,double value)
        {
            return RedisManager.WriteDataBase().KeyExpire(key, TimeSpan.FromSeconds(value));
        }

        /// <summary>
        /// 为给定 key 设置生存时间，当 key 过期时(生存时间为 0 )，它会被自动删除。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">时间戳</param>
        /// <returns></returns>
        public bool KeyExpireAt(string key, long value)
        {
            
            return RedisManager.WriteDataBase().KeyExpire(key,new DateTime(value,DateTimeKind.Local));
        }

        /// <summary>
        /// 移除键的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyPersist(string key)
        {
            return RedisManager.WriteDataBase().KeyPersist(key);
        }

        /// <summary>
        /// 查看给定键距离过期键有多少秒
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public double KeyTTL(string key)
        {
            var flag = RedisManager.ReadDataBase().KeyExists(key);
            if (!flag)
                return -2;
            var time = RedisManager.ReadDataBase().KeyTimeToLive(key);
            if (time == null)
                return -1;
            else
                return time.Value.TotalSeconds;
        }

        #endregion

        #region String命令
        
        /// <summary>
        /// 追加一个值到key上
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>如果 key 已经存在，并且值为字符串，那么这个命令会把 value 追加到原来值（value）的结尾。 如果 key 不存在，那么它将首先创建一个空字符串的key，再执行追加操作，这种情况 APPEND 将类似于 SET 操作。</remarks>
        /// <returns>返回append后字符串值（value）的长度。</returns>
        public long StringAppend(string key, string value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringAppend(key, value);
        }

        /// <summary>
        /// 对key对应的数字做减一操作
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <remarks>如果key不存在，那么在操作之前，这个key对应的值会被置为0。如果key有一个错误类型的value或者是一个不能表示成数字的字符串，就返回错误。这个操作最大支持在64位有符号的整型数字。</remarks>
        /// <returns>返回减少之后的值 </returns>
        public long StringDecr(string key, int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringDecrement(key);
        }

        /// <summary>
        /// 将对应的key减少value
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <param name="value">一次减去的值</param>
        /// <remarks>如果key不存在，那么在操作之前，这个key对应的值会被置为0。如果key有一个错误类型的value或者是一个不能表示成数字的字符串，就返回错误。这个操作最大支持在64位有符号的整型数字。</remarks>
        /// <returns>返回减少之后的值</returns>
        public long StringDecrBy(string key,int value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringDecrement(key, value);
        }

        /// <summary>
        /// 返回key的value
        /// </summary>
        /// <param name="keys"></param>
        /// <remarks>返回key的value。如果key不存在，返回特殊值nil。如果key的value不是string，就返回错误，因为GET只处理string类型的values。</remarks>
        /// <returns>key对应的value，或者nil（key不存在时）</returns>
        public string StringGet(string key,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringGet(key);
        }

        /// <summary>
        /// 返回key的value
        /// </summary>
        /// <param name="keys"></param>
        /// <remarks>返回key的value。如果key不存在，返回特殊值nil。如果key的value不是string，就返回错误，因为GET只处理string类型的values。</remarks>
        /// <returns>key对应的value，或者nil（key不存在时）</returns>
        public string StringGetAsync(string key,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringGetAsync(key).Result;
        }

        /// <summary>
        /// 获取所有指定key的value，对于不存在的string或者不存在的key，返回null
        /// </summary>
        /// <param name="keys"></param>
        public ArrayList StringGet(string[] keys,int db = -1)
        {
            RedisKey[] list = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                list[i] = keys[i];
            }
            var sets = RedisManager.ReadDataBase(db).StringGet(list);
            ArrayList list_result = new ArrayList();
            foreach (var item in sets)
            {
                list_result.Add(item.ToString());
            }
            return list_result;
        }

        /// <summary>
        /// 获取所有指定key的value，对于不存在的string或者不存在的key，返回null
        /// </summary>
        /// <param name="keys"></param>
        public List<string> StringGetAsync(string[] keys,int db = -1)
        {
            RedisKey[] list = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                list[i] = keys[i];
            }
            var sets = RedisManager.ReadDataBase(db).StringGetAsync(list).Result;
            List<string> list_result = new List<string>();
            foreach (var item in sets)
            {
                list_result.Add(item.ToString());
            }
            return list_result;
        }


        /// <summary>
        /// 获取存储在key上的值的一个子字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        /// <remarks>返回key对应的字符串value的子串，这个子串是由start和end位移决定的（两者都在string内）。可以用负的位移来表示从string尾部开始数的下标。所以-1就是最后一个字符，-2就是倒数第二个，以此类推。</remarks>
        /// <returns></returns>
        public string StringGetRange(string key,int start,int end,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringGetRange(key, start, end);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string StringGetRangeAsnyc(string key,int start,int end,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringGetRangeAsync(key, start, end).Result;
        }

        /// <summary>
        /// 设置一个key的value，并获取设置之前的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>如果key存在但是对应的value不是字符串，就返回错误</remarks>
        /// <returns></returns>
        public string StringGetSet(string key,string value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringGetSet(key, value);
        }

        /// <summary>
        /// 设置一个key的value，并获取设置之前的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>如果key存在但是对应的value不是字符串，就返回错误</remarks>
        /// <returns></returns>
        public string StringGetSetAsync(string key, string value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringGetSetAsync(key, value).Result;
        }

        /// <summary>
        /// 对key对应的数字做加一操作
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long StringIncr(string key,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringIncrement(key);
        }

        /// <summary>
        /// 对key对应的数字做加一操作
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<long> StringIncrAsync(string key,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringIncrementAsync(key);
        }

        /// <summary>
        /// 将对应的key增加value
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <param name="value">一次增加的值</param>
        /// <remarks>如果key不存在，那么在操作之前，这个key对应的值会被置为0。如果key有一个错误类型的value或者是一个不能表示成数字的字符串，就返回错误。这个操作最大支持在64位有符号的整型数字。</remarks>
        /// <returns></returns>
        public long StringIncrBy(string key, int value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringIncrement(key, value);
        }

        /// <summary>
        /// 将对应的key增加value
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <param name="value">一次增加的值</param>
        /// <remarks>如果key不存在，那么在操作之前，这个key对应的值会被置为0。如果key有一个错误类型的value或者是一个不能表示成数字的字符串，就返回错误。这个操作最大支持在64位有符号的整型数字。</remarks>
        /// <returns></returns>
        public Task<long> StringIncrByAsync(string key, int value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringIncrementAsync(key, value);
        }

        /// <summary>
        /// 将对应的key增加value
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <param name="value">一次增加的值</param>
        /// <remarks>如果key不存在，那么在操作之前，这个key对应的值会被置为0。如果key有一个错误类型的value或者是一个不能表示成数字的字符串，就返回错误。这个操作最大支持在64位有符号的整型数字。</remarks>
        /// <returns></returns>
        public double StringIncrBy(string key, double value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringIncrement(key, value);
        }

        /// <summary>
        /// 将对应的key增加value
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <param name="value">一次增加的值</param>
        /// <remarks>如果key不存在，那么在操作之前，这个key对应的值会被置为0。如果key有一个错误类型的value或者是一个不能表示成数字的字符串，就返回错误。这个操作最大支持在64位有符号的整型数字。</remarks>
        /// <returns></returns>
        public Task<double> StringIncrByAsync(string key, double value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringIncrementAsync(key, value);
        }
        
        /// <summary>
        /// 设置一个key的value值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db">库ID</param>
        /// <remarks>返回key的value。如果key不存在，返回特殊值nil。如果key的value不是string，就返回错误，因为GET只处理string类型的values。</remarks>
        /// <returns>如果SET命令正常执行那么回返回OK，否则如果加了NX 或者 XX选项，但是没有设置条件。那么会返回nil。</returns>
        public bool StringSet(string key, string value,int db = -1)
        {
            bool flag = RedisManager.WriteDataBase(db).StringSet(key: key, value: value);
            return flag;
        }

        /// <summary>
        /// 设置一个键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>返回key的value。如果key不存在，返回特殊值nil。如果key的value不是string，就返回错误，因为GET只处理string类型的values。</remarks>
        /// <returns>如果SET命令正常执行那么回返回OK，否则如果加了NX 或者 XX选项，但是没有设置条件。那么会返回nil。</returns>
        public Task<bool> StringSetAsync(string key, string value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringSetAsync(key,value);
        }

        /// <summary>
        /// 设置一组键值对
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        public bool StringSet(string[] keys,string[] values,int db = -1)
        {
            List<KeyValuePair<RedisKey, RedisValue>> list = new List<KeyValuePair<RedisKey, RedisValue>>();
            for (int i = 0; i < keys.Length; i++)
            {
                KeyValuePair<RedisKey, RedisValue> dict = new KeyValuePair<RedisKey, RedisValue>(keys[i],values[i]);
                list.Add(dict);
            }
            return RedisManager.WriteDataBase(db).StringSet(list.ToArray());
        }

        /// <summary>
        /// 设置一组键值对
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public Task<bool> StringSetAsync(string[] keys, string[] values,int db = -1)
        {
            List<KeyValuePair<RedisKey, RedisValue>> list = new List<KeyValuePair<RedisKey, RedisValue>>();
            for (int i = 0; i < keys.Length; i++)
            {
                KeyValuePair<RedisKey, RedisValue> dict = new KeyValuePair<RedisKey, RedisValue>(keys[i], values[i]);
                list.Add(dict);
            }
            return RedisManager.WriteDataBase(db).StringSetAsync(list.ToArray());
        }

        /// <summary>
        /// 对应给定的keys到他们相应的values上。只要有一个key已经存在，MSETNX一个操作都不会执行。
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool StringSetNX(string[] keys,string[] values,int db = -1)
        {
            List<KeyValuePair<RedisKey, RedisValue>> list = new List<KeyValuePair<RedisKey, RedisValue>>();
            for (int i = 0; i < keys.Length; i++)
            {
                KeyValuePair<RedisKey, RedisValue> dict = new KeyValuePair<RedisKey, RedisValue>(keys[i], values[i]);
                list.Add(dict);
            }
            return RedisManager.WriteDataBase(db).StringSet(list.ToArray(),When.NotExists);
        }

        /// <summary>
        /// 设置key对应字符串value，并且设置key在给定的seconds时间之后超时过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">过期秒数</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool StringSetEX(string key, string value, int seconds,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringSet(key, value, TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// 用 value 参数覆盖给定 key 所储存的字符串值，从偏移量 offset 开始。
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <param name="offset">覆盖的起始位置</param>
        /// <param name="value">覆盖值</param>
        /// <returns></returns>
        public long StringSetRange(string key,int offset,string value,int db = -1)
        {
            return (long)RedisManager.WriteDataBase(db).StringSetRange(key, offset, value);
        }

        /// <summary>
        /// 设置位值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset">大于等于0</param>
        /// <param name="bit">0/1</param>
        /// <returns>返回位的值</returns>
        public bool StringSetBit(string key,long offset,bool bit,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringSetBit(key, offset, bit);
        }

        /// <summary>
        /// 设置位值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset">大于等于0</param>
        /// <param name="bit">0/1</param>
        /// <returns>返回位的值</returns>
        public Task<bool> StringSetBitAsync(string key, long offset, bool bit,int db = -1)
        {
            return RedisManager.WriteDataBase(db).StringSetBitAsync(key, offset, bit);
        }

        /// <summary>
        /// 获取位的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool StringGetBit(string key,long offset,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringGetBit(key, offset);
        }

        /// <summary>
        /// 获取位的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Task<bool> StringGetBitAsync(string key, long offset,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringGetBitAsync(key, offset);
        }
        
        /// <summary>
        /// 返回 key 所储存的字符串值的长度。
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <returns></returns>
        public long StringLength(string key,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringLength(key);
        }

        /// <summary>
        /// 返回 key 所储存的字符串值的长度。
        /// </summary>
        /// <param name="key">字符串名</param>
        /// <returns></returns>
        public Task<long> StringLengthAsync(string key,int db = -1)
        {
            return RedisManager.ReadDataBase(db).StringLengthAsync(key);
        }

        #endregion

        #region List命令

        /// <summary>
        /// 获取指定下标的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ListIndex(string key, int index,int db = -1)
        {
            return RedisManager.ReadDataBase(db).ListGetByIndex(key, index);
        }

        public enum RedisCommandDirection
        {
            BEFORE = 0,
            AFTER = 1
        }

        public long ListInsert(string key, RedisCommandDirection direct, string pivot, string value,int db = -1)
        {
            if (direct == RedisCommandDirection.BEFORE)
            {
                return RedisManager.WriteDataBase(db).ListInsertBefore(key, pivot, value);
            }
            else if (direct == RedisCommandDirection.AFTER)
            {
                return RedisManager.WriteDataBase(db).ListInsertAfter(key, pivot, value);
            }
            else
            {
                throw new Exception("插入值的方向错误!");
            }
        }

        /// <summary>
        /// 返回List的长度，如果key不存在返回0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key,int db = -1)
        {
            return RedisManager.ReadDataBase().ListLength(key);
        }

        /// <summary>
        /// 往队列左边出队一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListLeftPop(string key,int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListLeftPop(key);
        }
        
        /// <summary>
        /// 从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListLeftPush(string key, string value,int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListLeftPush(key, value);
        }

        /// <summary>
        /// 从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="values">插入的值数组</param>
        /// <returns></returns>
        public long ListLeftPush(string key, string[] values, int db = -1)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            return RedisManager.WriteDataBase(db).ListLeftPush(key, rvalues);
        }

        /// <summary>
        /// 当队列存在时，从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListLeftPushX(string key,string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListLeftPush(key, value, When.Exists);
        }

        /// <summary>
        /// 从列表中获取指定范围的元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="start">起始下标</param>
        /// <param name="stop">结束下标</param>
        /// <returns></returns>
        public ArrayList ListRange(string key,int start,int stop, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).ListRange(key, start, stop);
            var result_list = new ArrayList();
            foreach (var item in list)
            {
                result_list.Add(item);
            }
            return result_list;
        }

        /// <summary>
        /// 从队列中删除元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="count">删除元素的个数和方向</param>
        /// <param name="value">删除的元素</param>
        /// <returns></returns>
        public long ListRemove(string key, string value, int count, int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListRemove(key, value, count);
        }

        /// <summary>
        /// 设置队列里面一个元素的值
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="index">下标位置</param>
        /// <param name="value">修改的元素值</param>
        public void ListSetByIndex(string key,int index,string value, int db = -1)
        {
            RedisManager.WriteDataBase(db).ListSetByIndex(key, index, value);
        }

        /// <summary>
        /// 修建一个已存在的list，这样list就只包含指定范围的元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="start">起始下标</param>
        /// <param name="stop">结束下标</param>
        public void ListTrim(string key,int start,int stop, int db = -1)
        {
            RedisManager.WriteDataBase(db).ListTrim(key, start, stop);
        }

        /// <summary>
        /// 移除source列表中的最后一个元素，添加到destination的第一个元素
        /// </summary>
        /// <param name="source">源队列</param>
        /// <param name="destination">目标队列</param>
        /// <remarks>可用来做安全的队列，循环列表</remarks>
        /// <returns></returns>
        public string ListRightPopLeftPush(string source,string destination, int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListRightPopLeftPush(source, destination);
        }
        
        /// <summary>
        /// 往队列左边出队一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListRightPop(string key, int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListRightPop(key);
        }

        /// <summary>
        /// 从队列右边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListRightPush(string key, string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListRightPush(key, value);
        }

        /// <summary>
        /// 从队列右边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="values">插入的值数组</param>
        /// <returns></returns>
        public long ListRightPush(string key, string[] values, int db = -1)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            return RedisManager.WriteDataBase(db).ListRightPush(key, rvalues);
        }

        /// <summary>
        /// 当队列存在时，从队列右边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListRightPushX(string key, string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).ListRightPush(key, value, When.Exists);
        }

        #endregion

        #region Set命令

        /// <summary>
        /// 添加一个或多个元素到集合里
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public bool SetAdd(string key,string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SetAdd(key, value);
        }

        /// <summary>
        /// 添加一个或多个元素到集合里
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long SetAdd(string key,string[] values, int db = -1)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            return RedisManager.WriteDataBase(db).SetAdd(key, rvalues);
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合之间的差集。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ArrayList SetDiff(string first,string second, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SetCombine(SetOperation.Difference, first, second);
            ArrayList list_result = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
            {
                list_result.Add(list[i]);
            }
            return list_result;
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合之间的差集。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ArrayList SetDiff(string[] keys, int db = -1)
        {
            List<RedisKey> list_key = new List<RedisKey>();
            for (int i = 0; i < keys.Length; i++)
            {
                list_key.Add(keys[i]);
            }
            var list = RedisManager.ReadDataBase(db).SetCombine(SetOperation.Difference, keys: list_key.ToArray());
            ArrayList list_result = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
            {
                list_result.Add(list[i]);
            }
            return list_result;
        }

        /// <summary>
        /// 将集合之间给定的差集保存在dest集合中，如果集合已存在则将其覆盖
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="dest">生成的集合</param>
        /// <returns></returns>
        public long SetDiffStore(string first, string second, string dest, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SetCombineAndStore(SetOperation.Difference, dest, first, second);
        }

        /// <summary>
        /// 将集合之间给定的差集保存在dest集合中，如果集合已存在则将其覆盖
        /// </summary>
        /// <param name="keys">集合数组</param>
        /// <param name="dest">生成的集合</param>
        /// <returns></returns>
        public long SetDiffStore(string[] keys, string dest,int db = -1)
        {
            List<RedisKey> list_key = new List<RedisKey>();
            for (int i = 0; i < keys.Length; i++)
            {
                list_key.Add(keys[i]);
            }
            return RedisManager.WriteDataBase(db).SetCombineAndStore(SetOperation.Difference, dest, keys: list_key.ToArray());
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合之间的交集。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ArrayList SetInter(string first,string second, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SetCombine(SetOperation.Intersect, first, second);
            ArrayList list_result = new ArrayList();
            for (int i = 0; i < list.Length; i++)
            {
                list_result.Add(list[i]);
            }
            return list_result;
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合之间的交集。
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ArrayList SetInter(string[] keys, int db = -1)
        {
            List<RedisKey> list_key = new List<RedisKey>();
            for (int i = 0; i < keys.Length; i++)
            {
                list_key.Add(keys[i]);
            }
            var list = RedisManager.ReadDataBase(db).SetCombine(SetOperation.Intersect, list_key.ToArray());
            ArrayList list_result = new ArrayList();
            for (int i = 0; i < list.Length; i++)
            {
                list_result.Add(list[i]);
            }
            return list_result;
        }

        /// <summary>
        /// 将集合之间给定的差集保存在dest集合中，如果集合已存在则将其覆盖
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="dest">生成的集合</param>
        /// <returns></returns>
        public long SetInterStore(string first, string second, string dest, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SetCombineAndStore(SetOperation.Intersect, dest, first, second);
        }

        /// <summary>
        /// 将集合之间给定的差集保存在dest集合中，如果集合已存在则将其覆盖
        /// </summary>
        /// <param name="keys">集合数组</param>
        /// <param name="dest">生成的集合</param>
        /// <returns></returns>
        public long SetInterStore(string[] keys, string dest, int db = -1)
        {
            List<RedisKey> list_key = new List<RedisKey>();
            for (int i = 0; i < keys.Length; i++)
            {
                list_key.Add(keys[i]);
            }
            return RedisManager.WriteDataBase(db).SetCombineAndStore(SetOperation.Intersect, dest, keys: list_key.ToArray());
        }

        /// <summary>
        /// 判断 member 元素是否集合 key 的成员。
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="member">成员的值</param>
        /// <returns></returns>
        public bool SetIsMember(string key,string member, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SetContains(key, member);
        }

        /// <summary>
        /// 获取集合里面所有的key
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <returns></returns>
        public ArrayList SetMembers(string key, int db = -1)
        {
            var sets = RedisManager.ReadDataBase(db).SetMembers(key);
            ArrayList list = new ArrayList();
            foreach (var item in sets)
            {
                list.Add(item.ToString());
            }
            return list;
        }

        /// <summary>
        /// 将 member 元素从 source 集合移动到 destination 集合。
        /// </summary>
        /// <param name="source">源集合</param>
        /// <param name="destination">目标集合</param>
        /// <param name="value">移动的值</param>
        /// <returns></returns>
        public bool SetMove(string source,string destination,string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SetMove(source, destination, value);
        }

        /// <summary>
        /// 返回集合的基数
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <returns></returns>
        public long SetLength(string key, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SetLength(key);
        }

        /// <summary>
        /// 判断成员是否是集合中的成员
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="member">成员的值</param>
        /// <returns></returns>
        public bool SetContains(string key,string member, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SetContains(key, member);
        }

        /// <summary>
        /// 移除并返回集合中的一个随机元素。
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <returns></returns>
        public string SetPop(string key, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SetPop(key);
        }

        /// <summary>
        /// 随机获取集合中的一个元素
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <returns></returns>
        public string SetRandMember(string key, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SetRandomMember(key);
        }

        /// <summary>
        /// 随机获取集合中的一组元素
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="count">取出元素的个数</param>
        /// <returns></returns>
        public ArrayList SetRandMembers(string key,int count, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SetRandomMembers(key,count);
            ArrayList list_result = new ArrayList();
            for (int i = 0; i < list.Count(); i++)
            {
                list_result.Add(list[i]);
            }
            return list_result;
        }

        /// <summary>
        /// 移除集合 key 中的一个元素，不存在的 member 元素会被忽略。
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="value">删除的值</param>
        /// <returns></returns>
        public bool SetRemove(string key,string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SetRemove(key, value);
        }

        /// <summary>
        /// 移除集合 key 中的多个 member 元素，不存在的 member 元素会被忽略。
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="value">删除的值</param>
        /// <returns></returns>
        public long SetRemove(string key,string[] value, int db = -1)
        {
            var list = Array.ConvertAll<string, RedisValue>(value, s => (RedisValue)s);
            return RedisManager.WriteDataBase(db).SetRemove(key, values: list.ToArray());
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合的并集。
        /// </summary>
        /// <param name="first">第一个集合</param>
        /// <param name="second">第二个集合</param>
        /// <returns></returns>
        public string[] SetUnion(string first,string second, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SetCombine(SetOperation.Union, first, second);
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合的并集。
        /// </summary>
        /// <param name="keys">所有合并的集合名称</param>
        /// <returns></returns>
        public string[] SetUnion(string[] keys, int db = -1)
        {
            var redisKeys = Array.ConvertAll<string, RedisKey>(keys, m => (RedisKey)m);
            var list = RedisManager.ReadDataBase(db).SetCombine(SetOperation.Union, redisKeys);
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 但它将结果保存到 destination 集合，该集合是所有给定集合的并集。
        /// </summary>
        /// <param name="destination">合并生成的集合</param>
        /// <param name="first">第一个集合</param>
        /// <param name="second">第二个集合</param>
        /// <returns></returns>
        public long SetUnionStore(string destination, string first, string second, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SetCombineAndStore(SetOperation.Union, destination, first, second);
        }

        /// <summary>
        /// 但它将结果保存到 destination 集合，该集合是所有给定集合的并集。
        /// </summary>
        /// <param name="destination">合并生成的集合</param>
        /// <param name="keys">所有合并的集合名称</param>
        /// <returns></returns>
        public long SetUnionStore(string destination, string[] keys, int db = -1)
        {
            var redisKeys = Array.ConvertAll<string, RedisKey>(keys, m => (RedisKey)m);
            return RedisManager.ReadDataBase(db).SetCombineAndStore(SetOperation.Union, destination, redisKeys);
        }

        /// <summary>
        /// 迭代集合键中的元素
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="pattern">获取的值</param>
        /// <param name="pagesize">最大结果数</param>
        /// <returns></returns>
        public string[] SetScan(string key,string pattern,int pagesize, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SetScan(key, pattern, pagesize).ToArray();
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 迭代集合键中的元素
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="pattern">获取的值</param>
        /// <param name="pagesize">最大结果数</param>
        /// <returns></returns>
        public string[] SetScan(string key, string pattern, int pagesize,int cursor, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SetScan(key, pattern, pagesize,cursor).ToArray();
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        #endregion

        #region Hash命令

        /// <summary>
        /// 删除哈希表 key 中的一个指定域，不存在的域将被忽略。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field">域的名称</param>
        /// <returns></returns>
        public bool HashDelete(string key,string field, int db = -1)
        {
            return RedisManager.WriteDataBase(db).HashDelete(key, field);
        }

        /// <summary>
        /// 删除哈希表 key 中的一个指定域，不存在的域将被忽略。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field">域的名称</param>
        /// <returns></returns>
        public long HashDelete(string key, string[] fields, int db = -1)
        {
            return RedisManager.WriteDataBase(db).HashDelete(key,Array.ConvertAll<string, RedisValue>(fields,m => (RedisValue)m));
        }

        /// <summary>
        /// 查看哈希表 key 中，给定域 field 是否存在。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field">域的名称</param>
        /// <returns></returns>
        public bool HashExists(string key,string field, int db = -1)
        {
            return RedisManager.ReadDataBase(db).HashExists(key, field);
        }

        /// <summary>
        /// 返回哈希表 key 中给定域 field 的值。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field">域的名称</param>
        /// <returns></returns>
        public string HashGet(string key,string field, int db = -1)
        {
            return RedisManager.ReadDataBase(db).HashGet(key, field);
        }

        /// <summary>
        /// 返回哈希表 key 中，所有的域和值。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <returns></returns>
        public Dictionary<string, string> HashGetAll(string key, int db = -1)
        {
            var entry = RedisManager.ReadDataBase(db).HashGetAll(key);
            return entry.ToDictionary(m => m.Name.ToString(), n => n.Value.ToString());
        }

        /// <summary>
        /// 为哈希表 key 中的域 field 的值加上增量 value 。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field">域的名称</param>
        /// <param name="value">增量</param>
        /// <returns></returns>
        public long HashIncrement(string key,string field,int value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).HashIncrement(key, field, value);
        }

        /// <summary>
        /// 为哈希表 key 中的域 field 的值加上增量 value 。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field">域的名称</param>
        /// <param name="value">增量</param>
        /// <returns></returns>
        public double HashIncrement(string key, string field, double value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).HashIncrement(key, field, value);
        }

        /// <summary>
        /// 返回哈希表 key 中的所有域。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <returns></returns>
        public string[] HashKeys(string key, int db = -1)
        {
            var list_key = RedisManager.ReadDataBase(db).HashKeys(key);
            return Array.ConvertAll<RedisValue, string>(list_key, m => m.ToString());
        }

        /// <summary>
        /// 返回哈希表 key 中域的数量。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long HashLength(string key, int db = -1)
        {
            return RedisManager.ReadDataBase(db).HashLength(key);
        }

        /// <summary>
        /// 将哈希表 key 中的域 field 的值设为 value 。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field">域的名称</param>
        /// <param name="value">域的值</param>
        /// <returns></returns>
        public bool HashSet(string key,string field,string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).HashSet(key, field, value);
        }

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="fields">域的名称</param>
        /// <param name="values">域的值</param>
        public void HashSet(string key,string[] fields,string[] values, int db = -1)
        {
            //判断两个数组的长度是否相等
            if (fields.Length != values.Length)
                throw new Exception("域的名称和域的值的数组长度不等!");
            if (fields.Length <= 0)
                throw new Exception("域的名称数组长度不能小于0");
            HashEntry[] hashlist = new HashEntry[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                HashEntry entry = new HashEntry(fields[i], values[i]);
                hashlist[i] = entry;
            }
            RedisManager.WriteDataBase(db).HashSet(key, hashlist);
        }

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中。
        /// </summary>
        /// <param name="key">哈希表的名称</param>
        /// <param name="field_value">域的名称和值</param>
        public void HashSet(string key,Dictionary<string,string> field_value, int db = -1)
        {
            var fields = field_value.Keys.ToArray();
            var values = field_value.Values.ToArray();
            this.HashSet(key, fields, values,db);
        }
        

        /// <summary>
        /// 当且仅当域 field 不存在时。将哈希表 key 中的域 field 的值设置为 value 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSetNX(string key,string field,string value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).HashSet(key, false, value, When.NotExists);
        }

        /// <summary>
        /// 返回哈希表 key 中所有域的值。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] HashValues(string key, int db = -1)
        {
            var values = RedisManager.ReadDataBase(db).HashValues(key);
            return Array.ConvertAll<RedisValue, string>(values, m => m.ToString());

        }

        /// <summary>
        /// 增量地迭代哈希表的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Dictionary<string, string> HashScan(string key,string pattern ,int pageSize, int db = -1)
        {
            var entrys = RedisManager.ReadDataBase(db).HashScan(key, pattern, pageSize).ToList();
            return entrys.ToDictionary(m => m.Name.ToString(), n => n.Value.ToString());
        }
        #endregion

        #region ZSet命令

        /// <summary>
        /// 将一个或多个 member 元素及其 score 值加入到有序集 key 当中。
        /// </summary>
        /// <param name="key">有序集合名称</param>
        /// <param name="value">值</param>
        /// <param name="score">分值</param>
        /// <returns></returns>
        public bool SortedSetAdd(string key,string value,long score, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetAdd(key, value, score);
        }

        /// <summary>
        /// 将一个或多个 member 元素及其 score 值加入到有序集 key 当中。
        /// </summary>
        /// <param name="key">有序集合名称</param>
        /// <param name="value">值</param>
        /// <param name="score">分值</param>
        /// <returns></returns>
        public long SortedSetAdd(string key,string[] values,int[] score, int db = -1)
        {
            List<SortedSetEntry> list = new List<SortedSetEntry>();
            for(int i = 0;i < values.Length;i++)
            {
                SortedSetEntry entry = new SortedSetEntry(values[i],score[i]);
                list.Add(entry);
            }
            return RedisManager.WriteDataBase(db).SortedSetAdd(key, list.ToArray());
        }

        /// <summary>
        /// 返回有序集 key 的基数。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SortedSetLength(key);
        }

        /// <summary>
        /// 返回有序集 key 中， score 值在 min 和 max 之间(默认包括 score 值等于 min 或 max )的成员的数量。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public long SortedSetCount(string key,int min,int max, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SortedSetLength(key, min, max);
        }

        /// <summary>
        /// 为有序集 key 的成员 member 的 score 值加上增量 increment 。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double SortedSetIncrBy(string key, string member, int value, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetIncrement(key, member, value);
        }

        /// <summary>
        /// 返回有序集 key 中，指定区间内的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string[] SortedSetRange(string key,int start,int end, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SortedSetRangeByRank(key, start, end);
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 返回有序集 key 中，指定区间内的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> SortedSetRangeWithScore(string key, int start, int end, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SortedSetRangeByRankWithScores(key, start, end);
            List<Dictionary<string, string>> list_result = new List<Dictionary<string, string>>();
            for (int i = 0; i < list.Count(); i++)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("member",list[i].Element);
                dict.Add("score", list[i].Score.ToString());
                list_result.Add(dict);
            }
            return list_result;
        }

        /// <summary>
        /// 返回有序集 key 中，指定区间内的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string[] SortedSetRangeByScore(string key, long start, long end, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SortedSetRangeByScore(key, start, end);
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 返回有序集 key 中成员 member 的排名。其中有序集成员按 score 值递增(从小到大)顺序排列。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long? SortedSetRank(string key,string member, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SortedSetRank(key, member);
        }

        /// <summary>
        /// 移除有序集 key 中的一个或多个成员，不存在的成员将被忽略。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool SortedSetRemove(string key,string member, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetRemove(key, member);
        }

        /// <summary>
        /// 移除有序集 key 中的一个或多个成员，不存在的成员将被忽略。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long SortedSetRemove(string key, string[] member, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetRemove(key,Array.ConvertAll<string,RedisValue>(member,m => (RedisValue)m));
        }

        /// <summary>
        /// 移除有序集 key 中，指定排名(rank)区间内的所有成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public long SortedSetRemoveRangeByRank(string key,int start,int end, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetRemoveRangeByRank(key, start, end);
        }

        /// <summary>
        /// 移除有序集 key 中，所有 score 值介于 min 和 max 之间(包括等于 min 或 max )的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public long SortedSetRemoveRangeByScore(string key,int start,int end, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetRemoveRangeByScore(key, start, end);
        }

        /// <summary>
        /// 返回有序集 key 中，指定区间内的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string[] SortedSetRevRange(string key, int start, int end, int db = -1)
        {
            var list = RedisManager.ReadDataBase(db).SortedSetRangeByRank(key, start, end,Order.Descending);
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 返回有序集 key 中， score 值介于 max 和 min 之间(默认包括等于 max 或 min )的所有的成员。有序集成员按 score 值递减(从大到小)的次序排列。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string[] SortedSetRevRangeByScore(string key, int start, int end, int db = -1)
        {
            var list = RedisManager.WriteDataBase(db).SortedSetRangeByScore(key, start, end,order:Order.Descending);
            return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 返回有序集 key 中成员 member 的排名。其中有序集成员按 score 值递减(从大到小)排序。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long? SortedSetRevRank(string key, string member, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SortedSetRank(key, member,Order.Descending);
        }
        
        /// <summary>
        /// 返回有序集 key 中，成员 member 的 score 值。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public double? SortedSetScore(string key,string member, int db = -1)
        {
            return RedisManager.ReadDataBase(db).SortedSetScore(key, member);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的并集，其中给定 key 的数量必须以 numkeys 参数指定，并将该并集(结果集)储存到 destination 。默认情况下，结果集中某个成员的 score 值是所有给定集下该成员 score 值之 和 。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="dest"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public double? SortedSetUnionStore(string first,string second,string dest, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetCombineAndStore(SetOperation.Union, dest, first, second);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的并集，其中给定 key 的数量必须以 numkeys 参数指定，并将该并集(结果集)储存到 destination 。默认情况下，结果集中某个成员的 score 值是所有给定集下该成员 score 值之 和 。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="dest"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public double? SortedSetUnionStore(string[] keys, string dest, int db = -1)
        {
            var list = Array.ConvertAll<string, RedisKey>(keys, m => (RedisKey)m);
            return RedisManager.WriteDataBase(db).SortedSetCombineAndStore(SetOperation.Union, dest, list);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的并集，其中给定 key 的数量必须以 numkeys 参数指定，并将该并集(结果集)储存到 destination 。默认情况下，结果集中某个成员的 score 值是所有给定集下该成员 score 值之 和 。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="dest"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public double? SortedSetInterStore(string first, string second, string dest, int db = -1)
        {
            return RedisManager.WriteDataBase(db).SortedSetCombineAndStore(SetOperation.Intersect, dest, first, second);
        }

        /// <summary>
        /// 计算给定的一个或多个有序集的并集，其中给定 key 的数量必须以 numkeys 参数指定，并将该并集(结果集)储存到 destination 。默认情况下，结果集中某个成员的 score 值是所有给定集下该成员 score 值之 和 。
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="dest"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public double? SortedSetInterStore(string[] keys, string dest, int db = -1)
        {
            var list = Array.ConvertAll<string, RedisKey>(keys, m => (RedisKey)m);
            return RedisManager.WriteDataBase(db).SortedSetCombineAndStore(SetOperation.Intersect, dest, list);
        }

        /// <summary>
        /// 迭代集合键中的元素
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="pattern">获取的值</param>
        /// <param name="pagesize">最大结果数</param>
        /// <returns></returns>
        public List<Dictionary<string,string>> SortedSetScan(string key, string pattern, int pagesize, int db = -1)
        {
            List<Dictionary<string, string>> list_result = new List<Dictionary<string, string>>();
            var list = RedisManager.ReadDataBase(db).SortedSetScan(key, pattern, pagesize).ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("member", list[i].Element);
                dict.Add("score", list[i].Score.ToString());
                list_result.Add(dict);
            }
            return list_result;
            //return Array.ConvertAll<RedisValue, string>(list, m => m.ToString());
        }

        /// <summary>
        /// 迭代集合键中的元素
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="pattern">获取的值</param>
        /// <param name="pagesize">最大结果数</param>
        /// <returns></returns>
        public List<Dictionary<string, string>> SortedSetScan(string key, string pattern, int pagesize, int cursor, int db = -1)
        {
            List<Dictionary<string, string>> list_result = new List<Dictionary<string, string>>();
            var list = RedisManager.ReadDataBase(db).SortedSetScan(key, pattern, pagesize, cursor).ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("member", list[i].Element);
                dict.Add("score", list[i].Score.ToString());
                list_result.Add(dict);
            }
            return list_result;
        }
        #endregion

        #region 地理空间（geospatial）

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、名称）添加到指定的key中
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="member">地点名称</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool GeoAdd(string key,double longitude, double latitude,string member,int db = -1)
        {
            return RedisManager.WriteDataBase(db).GeoAdd(key, longitude, latitude, member);
        }

        /// <summary>
        /// 返回两个地点之间的距离(Km)，如果两个地点中有一个不存在就返回空
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member1">地点1</param>
        /// <param name="member2">地点2</param>
        /// <param name="unit">长度单位</param>
        /// <returns></returns>
        public double? GeoDistance(string key,string member1,string member2, GeoUnit unit = GeoUnit.Kilometers, int db = -1)
        {
            return RedisManager.ReadDataBase(db).GeoDistance(key, member1, member2, unit);
        }

        /// <summary>
        /// 返回一个或多个位置元素的 Geohash 表示
        /// Geohash是GustavoNiemeyer发明的一种公共域地理编码系统，它将地理位置编码成一串简短的字母和数字。它是一种分层的空间数据结构，它将空间细分为网格形状的桶，这是Z阶曲线和一般空间填充曲线的众多应用之一。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public string GeoHash(string key, string member, int db = -1)
        {
            return RedisManager.ReadDataBase(db).GeoHash(key, member);
        }

        /// <summary>
        /// 返回指定地点的经纬度，如果地点不存在就返回空
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public GeoPosition? GeoPos(string key,string member, int db = -1)
        {
            return RedisManager.ReadDataBase(db).GeoPosition(key, member);
        }

        /// <summary>
        /// 返回指定地点的经纬度，如果地点不存在就返回空
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public GeoPosition?[] GetPos(string key,string[] member, int db = -1)
        {
            RedisValue[] value = new RedisValue[member.Length];
            for (int i = 0; i < member.Length; i++)
            {
                value[i] = member[i];
            }
            return RedisManager.ReadDataBase(db).GeoPosition(key, value);
        }

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <returns></returns>
        public GeoRadiusResult[] GeoRadius(string key, double longitude, double latitude)
        {
            return GeoRadius(key, longitude, latitude, GeoUnit.Kilometers, -1, Order.Ascending, GeoRadiusOptions.WithDistance);
        }

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="unit">长度单位</param>
        /// <param name="count">返回值数量</param>
        /// <param name="order">排序方式</param>
        /// <param name="options">返回额外的值，比如之间的距离，地点的经纬度</param>
        /// <returns></returns>
        public GeoRadiusResult[] GeoRadius(string key, double longitude, double latitude,GeoUnit unit = GeoUnit.Kilometers,int count = -1,Order? order = Order.Ascending, GeoRadiusOptions options = GeoRadiusOptions.Default, int db = -1)
        {
            return RedisManager.ReadDataBase(db).GeoRadius(key, longitude, latitude, unit,count, order, options);
        }

        /// <summary>
        /// 以给定的地点为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">地点</param>
        /// <param name="radius">距离</param>
        /// <returns></returns>
        public GeoRadiusResult[] GeoRadiusByMember(string key, string member, double radius)
        {
            return GeoRadiusByMember(key, member, radius, GeoUnit.Kilometers, -1, Order.Ascending, GeoRadiusOptions.WithDistance);
        }

        /// <summary>
        /// 以给定的地点为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">地点</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">长度单位</param>
        /// <param name="count">返回值数量</param>
        /// <param name="order">排序方式</param>
        /// <param name="options">返回额外的值，比如之间的距离，地点的经纬度</param>
        /// <returns></returns>
        public GeoRadiusResult[] GeoRadiusByMember(string key,string member,double radius, GeoUnit unit = GeoUnit.Kilometers, int count = -1, Order? order = Order.Ascending, GeoRadiusOptions options = GeoRadiusOptions.Default, int db = -1)
        {
            return RedisManager.ReadDataBase(db).GeoRadius(key, member, radius, unit, count, order, options);
        }

        /// <summary>
        /// 移除指定键的地点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool GeoRemove(string key,string member, int db = -1)
        {
            return RedisManager.WriteDataBase(db).GeoRemove(key, member);
        }

        #endregion

    }
}
