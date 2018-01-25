using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCYN.Common.Sql.redis;

namespace XCYN.Print.Redis
{
    public class BetchTest
    {
        /// <summary>
        /// 3479
        /// </summary>
        public void StringAdd()
        {
            RedisCommand command = new RedisCommand();
            long total = 0;
            for (int j = 0; j < 10; j++)
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                for (int i = 0; i < 10000; i++)
                {
                    //批量插入数据
                    command.SetAdd("id:" + i, "11111111111111111111111111111111111");
                }
                s.Stop();
                var t = s.ElapsedMilliseconds;
                total += t;
                Console.WriteLine("ThreadID:{1},插入,读取了10000条数据花费{0}毫秒", t, Thread.CurrentThread.ManagedThreadId);
            }
            Console.WriteLine("ThreadID:{1},平均花费:{0}", total / 10, Thread.CurrentThread.ManagedThreadId);
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void StringSetGet()
        {
            RedisCommand command = new RedisCommand();
            long total = 0;
            for (int j = 0; j < 10; j++)
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                for (int i = 0; i < 10000; i++)
                {
                    //批量插入数据
                    command.StringSet("id:" + i, "11111111111111111111111111111111111");
                }
                for (int i = 0; i < 10000; i++)
                {
                    command.StringGet("id:" + i);
                }
                
                s.Stop();
                var t = s.ElapsedMilliseconds;
                total += t;
                
                Console.WriteLine("ThreadID:{1},插入,读取了10000条数据花费{0}毫秒", t, Thread.CurrentThread.ManagedThreadId);
            }
            Console.WriteLine("ThreadID:{1},平均花费:{0}", total / 10, Thread.CurrentThread.ManagedThreadId);

        }

        /// <summary>
        /// 13380
        /// </summary>
        public void BetchGetSet()
        {
           
            RedisCommand command = new RedisCommand();
            long total = 0;
            for (int j = 0; j < 10; j++)
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                var batch = RedisManager.WriteDataBase().CreateBatch();
                for (int i = 0; i < 10000; i++)
                {
                    //批量插入数据
                    command.StringSet("id:" + i, "11111111111111111111111111111111111");
                }
                batch.Execute();
                batch = RedisManager.ReadDataBase().CreateBatch();
                for (int i = 0; i < 10000; i++)
                {
                    command.StringGet("id:" + i);
                }
                batch.Execute();
                s.Stop();
                var t = s.ElapsedMilliseconds;
                total += t;

                Console.WriteLine("ThreadID:{1},插入,读取了10000条数据花费{0}毫秒", t, Thread.CurrentThread.ManagedThreadId);
            }
            Console.WriteLine("ThreadID:{1},平均花费:{0}", total / 10, Thread.CurrentThread.ManagedThreadId);

           
        }
    }
}
