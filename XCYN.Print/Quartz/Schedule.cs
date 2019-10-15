using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.Quartz;

namespace XCYN.Print.Quartz1
{
    public class Schedule
    {
        /// <summary>
        /// SimpleSchedule 简单计划表（最常用的）
        /// </summary>
        public static void Fun1()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();
            
            var job = JobBuilder.Create<MyTask>().Build();

            // WithSimpleSchedule方法可以按照指定的时间间隔执行任务
            // SimpleScheduleBuilder可以具体的设置间隔的时分秒以及重复的次数
            var trigger = TriggerBuilder.Create().WithSimpleSchedule(
                                                                             m => m.WithInterval(TimeSpan.FromSeconds(2)).RepeatForever())
                                                                            .StartNow()
                                                                            .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// CalendarIntervalSchedule 日历计划表
        /// </summary>
        public static void Fun2()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            var job = JobBuilder.Create<MyTask>().Build();

            // WithCalendarIntervalSchedule
            // CalendarIntervalScheduleBuilder可以具体的设置间隔的年月日时分秒，解决了每个月天数不同的问题
            var trigger = TriggerBuilder.Create().WithCalendarIntervalSchedule(
                                                                             m => m.WithIntervalInWeeks(1))
                                                                            .StartNow()
                                                                            .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }

    public class MyTask : IJob
    {
        int index = 0;

        public void Execute(IJobExecutionContext context)
        {
            index++;
            Console.WriteLine("index:{0}，执行时间:{1}，下次执行时间:{2}，上次执行时间:{3}", 
                index, 
                context.ScheduledFireTimeUtc.Value,
                context.NextFireTimeUtc.Value,
                context.PreviousFireTimeUtc == null ? DateTimeOffset.MinValue : context.PreviousFireTimeUtc.Value);
        }
    }
}
