using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MyQuartz
{
    /// <summary>
    /// Quartz的哑火策略
    /// </summary>
    public class QuartzMisfire
    {
        /// <summary>
        /// 哑火策略：WithMisfireHandlingInstructionFireNow
        /// 不追赶哑火，如果有触发哑火，立即执行，更新下次执行时间
        /// </summary>
        public static void Fun1()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //将键值对传给定时器
            var job = JobBuilder.Create<MyJob>()
                                                 .Build();

            var trigger = TriggerBuilder.Create()
                                                .StartAt(DateBuilder.DateOf(7,0,0))
                                                .WithSimpleSchedule(m => 
                                                    m.WithIntervalInHours(1)
                                                        .WithRepeatCount(100)
                                                        //如果有触发哑火，立即执行，如果之前定义的是12：00触发，调度时间变成了现在的时间
                                                        .WithMisfireHandlingInstructionFireNow()    
                                                ).Build();

            scheduler.ScheduleJob(job, trigger);
        }
        
        /// <summary>
        /// 哑火策略：WithMisfireHandlingInstructionIgnoreMisfires
        /// 错过的立即追赶，下次执行时间不变
        /// </summary>
        public static void Fun3()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //将键值对传给定时器
            var job = JobBuilder.Create<MyJob>()
                                                .UsingJobData("count", 0)
                                                .Build();

            var trigger = TriggerBuilder.Create()
                                                .StartAt(DateBuilder.DateOf(7, 0, 0))
                                                .WithSimpleSchedule(m =>
                                                    m.WithIntervalInHours(1)
                                                        .WithRepeatCount(100)
                                                        //错过的立即追赶，然后正常调度
                                                        .WithMisfireHandlingInstructionIgnoreMisfires()
                                                ).Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 哑火策略：WithMisfireHandlingInstructionNext
        /// 不追赶哑火，正常执行，由于是正常执行下次执行时间不变了
        /// </summary>
        public static void Fun4()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //将键值对传给定时器
            var job = JobBuilder.Create<MyJob>()
                                                 .Build();

            var trigger = TriggerBuilder.Create()
                                                .StartAt(DateBuilder.DateOf(7, 0, 0))
                                                .WithSimpleSchedule(m =>
                                                         m.WithIntervalInHours(1)
                                                        .WithRepeatCount(100)
                                                        //正常调度，执行次数 = 预计执行次数 - 错过的次数
                                                        //.WithMisfireHandlingInstructionNextWithRemainingCount()
                                                        //正常调度，执行次数不变
                                                        .WithMisfireHandlingInstructionNextWithExistingCount()
                                                ).Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 哑火策略：WithMisfireHandlingInstructionNow
        /// 立即执行，但不追赶哑火的，更新下次执行时间
        /// </summary>
        public static void Fun5()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //将键值对传给定时器
            var job = JobBuilder.Create<MyJob>()
                                                 .Build();

            var trigger = TriggerBuilder.Create()
                                                 .StartAt(DateBuilder.DateOf(7, 0, 0))
                                                .WithSimpleSchedule(m =>
                                                    m.WithIntervalInHours(1)
                                                        .WithRepeatCount(100)
                                                        //立即执行，执行次数 = 预计执行次数 - 错过的次数
                                                        //.WithMisfireHandlingInstructionNowWithRemainingCount()
                                                        //立即执行，执行次数不变
                                                        .WithMisfireHandlingInstructionNowWithExistingCount()
                                                ).Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// WithCronSchedule的哑火策略
        /// 哑火策略：WithMisfireHandlingInstructionFireAndProceed
        /// 哑火的任务合并到一次执行，下次正常执行
        /// 
        /// 哑火策略：WithMisfireHandlingInstructionIgnoreMisfires
        /// 追赶执行哑火的任务，下次正常执行
        /// 
        /// 哑火策略：WithMisfireHandlingInstructionDoNothing
        /// 什么都不做，下次正常执行
        /// </summary>
        public static void Fun6()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //将键值对传给定时器
            var job = JobBuilder.Create<MyJob>()
                                                 .Build();

            var trigger = TriggerBuilder.Create()
                                                .StartAt(DateBuilder.DateOf(7, 0, 0))
                                                .WithCronSchedule("0 0 7-20 ? * MON-FRI",   //工作日8AM - 8PM每小时执行一次
                                                    //哑火的任务合并到一次执行，下次正常执行
                                                    //m => m.WithMisfireHandlingInstructionFireAndProceed()
                                                    //追赶执行哑火的任务，下次正常执行
                                                    //m => m.WithMisfireHandlingInstructionIgnoreMisfires()
                                                    //什么都不做，下次正常执行
                                                    m => m.WithMisfireHandlingInstructionDoNothing()
                                                )
                                                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
