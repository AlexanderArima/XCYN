using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Strategy
{
    /// <summary>
    /// 策略1：打印日志到数据库中
    /// </summary>
    class DBLog:AbstactLog
    {
        public override void Write(string msg)
        {
            Console.WriteLine("打印日志到数据库中:{0}", msg);
        }
    }
}
