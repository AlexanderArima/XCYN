using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common
{
    public class MyJob : IJob
    {
        public static int count = 0;
        public void Execute(IJobExecutionContext context)
        {
            //Console.WriteLine("执行Common的定时任务");
            
            Debug.WriteLine("本地执行时间：{0}，下次执行时间：{1}，执行次数：{2}",
                context.ScheduledFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"),
                context.NextFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"),
                count++);
        }
        
    }
}
