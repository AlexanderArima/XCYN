using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Core.Print.DI
{
    /// <summary>
    /// 实现日志切入类
    /// </summary>
    public class MyLogAttribute : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine($"写入日志，时间{DateTime.Now}");
            var task = next(context);
            Console.WriteLine($"结束写入日志，时间{DateTime.Now}");
            return task;
          
        }
    }
}
