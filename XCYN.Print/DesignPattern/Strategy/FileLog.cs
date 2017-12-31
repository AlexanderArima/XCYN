using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Strategy
{
    /// <summary>
    /// 策略2：打印日志到文件中
    /// </summary>
    class FileLog : AbstactLog
    {
        public override void Write(string msg)
        {
            Console.WriteLine("打印日志到文件中:{0}", msg);
        }
    }
}
