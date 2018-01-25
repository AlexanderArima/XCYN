using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common.Sql.redis;

namespace XCYN.Test
{
    [TestClass]
    public class RedisBenchTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            RedisCommand command = new RedisCommand();
            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 10000; i++)
            {
                //批量插入数据
                command.SetAdd("id:" + i, "11111111111111111111111111111111111");
            }
            s.Stop();
            var t = s.ElapsedMilliseconds;

            //插入10000条数据，测试了五次，分别花费了4848,3660,3618,3833,3690毫秒

        }
    }
}
