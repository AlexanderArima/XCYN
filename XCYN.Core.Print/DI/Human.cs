using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCYN.Core.Print.DI
{
    /// <summary>
    /// 实现
    /// </summary>
    public class Human : IEnable
    {
        //ILogger logger = null;

        public Human()
        {
            Console.WriteLine("构造方法");
        }
            
        public Human(ILoggerFactory factory)
        {
            //logger = factory?.CreateLogger<Human>();
            //logger.LogInformation("构造方法");
            Console.WriteLine("构造方法");
        }
        public void Say()
        {
            //logger.LogInformation("发布Debug日志");
            Console.WriteLine("Say...");
        }
    }
}
