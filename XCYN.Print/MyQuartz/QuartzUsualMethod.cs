using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using Quartz.Impl.Matchers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCYN.Print.rabbitmq;

namespace XCYN.Print.MyQuartz
{
    /// <summary>
    /// Quartz常用方法
    /// </summary>
    public class QuartzUsualMethod
    {
        /// <summary>
        /// 每隔3s触发一次，永远重复
        /// </summary>
        public static void Fun1()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            
            //将键值对传给定时器
            var job = JobBuilder.Create<ConsumerJob>()
                                                .UsingJobData("UserName", "Cheng")
                                                 .UsingJobData("Password", 123456)
                                                 .Build();
            
            var trigger = TriggerBuilder.Create()
                                                .WithSimpleSchedule(m => m.WithIntervalInSeconds(3).RepeatForever())
                                                 .StartNow()
                                                 .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        ///  每隔1s出发一次，执行1次
        /// </summary>
        public static void Fun2()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            //Job执行的任务
            var job = JobBuilder.Create<PublishJob>().Build();

            //Schedule计划表，可以设置调用次数，调用间隔
            //Trigger触发器，可以设置调用开始，结束时间，优先级
            var trigger = TriggerBuilder.Create().WithSimpleSchedule(
                                                                             m => m.WithIntervalInSeconds(1).WithRepeatCount(1))
                                                                            .StartNow()//.EndAt(DateTimeOffset.Now.AddSeconds(10))
                                                                            .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 通过 OfType + 反射 的方式执行定时任务
        /// </summary>
        public static void Fun3()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();            

            //job
            string path = string.Format("{0}XCYN.Common.dll", System.AppDomain.CurrentDomain.BaseDirectory);
            var type = Assembly.LoadFile(path).CreateInstance("XCYN.Common.MyJob").GetType();
            var job = JobBuilder.Create().OfType(type).Build();

            //trigger
            var trigger = TriggerBuilder.Create().
                                  WithSimpleSchedule(m => m.WithIntervalInSeconds(1).RepeatForever())
                                 .StartNow().Build();

            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        ///  JobBuilder对象 & IJobDetail 常用方法
        /// </summary>
        public static void Fun4()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            //job
            //string path = string.Format("{0}XCYN.Common.dll", System.AppDomain.CurrentDomain.BaseDirectory);
            //var type = Assembly.LoadFile(path).CreateInstance("XCYN.Common.MyJob").GetType();
            var dict = new Dictionary<string, string>();
            dict["username"] = "aisino";

            var job = JobBuilder.Create<MyJob>()
                            .WithDescription("Job名字是Fun4")  //给Job添加一个描述
                            .WithIdentity("Aisino") //给Job命名，默认随机生成一个Guid作为名字
                            .RequestRecovery(true)  //当执行失败时，是否恢复执行
                            .StoreDurably(true) //是否持久化（默认当没有Trigger指向Job时，Job会被删掉）
                            .SetJobData(new JobDataMap(dict))   //发送字典对象
                            .Build();

            //trigger
            var trigger = TriggerBuilder.Create().
                                  WithSimpleSchedule(m => m.WithIntervalInSeconds(1).RepeatForever())
                                 .StartNow().Build();

            //var trigger2 = TriggerBuilder.Create().
            //                   WithSimpleSchedule(m => m.WithIntervalInSeconds(5).RepeatForever())
            //                  .StartNow().Build();
            //System.Collections.Generic.ISet<ITrigger> set = new System.Collections.Generic.HashSet<ITrigger>();
            //set.Add(trigger);
            //set.Add(trigger2);
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        ///  在一分钟后启动触发器
        /// </summary>
        public static void Fun5()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();
            
            var job = JobBuilder.Create<MyJob>()
                            .Build();
            var timer = DateBuilder.EvenMinuteDateAfterNow();
            //trigger
            var trigger = TriggerBuilder.Create().StartAt(DateBuilder.EvenMinuteDateAfterNow())  //在一分钟后启动触发器
                                                                        .WithSimpleSchedule(m => m.WithIntervalInSeconds(1).RepeatForever())
                                                                        .Build();
            
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 在一分钟后关闭触发器
        /// </summary>
        public static void Fun6()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            var job = JobBuilder.Create<MyJob>()
                            .Build();

            //trigger
            var trigger = TriggerBuilder.Create() .StartNow()
                                                                        .EndAt(DateTimeOffset.Now.AddMinutes(1))  //在一分钟后关闭触发器
                                                                        .WithSimpleSchedule(m => m.WithIntervalInSeconds(1).RepeatForever())
                                                                        .Build();

            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 优先级调用（数字越大优先级越高）
        /// </summary>
        public static void Fun7()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            var job = JobBuilder.Create<MyJob>()
                            .Build();

            //trigger
            var trigger = TriggerBuilder.Create().StartNow()
                                                                        .WithPriority(1)
                                                                        .UsingJobData("Name","Trigger1")
                                                                        .EndAt(DateTimeOffset.Now.AddMinutes(1))  //在一分钟后关闭触发器
                                                                        .WithSimpleSchedule(m => m.WithIntervalInSeconds(1).RepeatForever())
                                                                        .Build();

            //trigger2
            var trigger2 = TriggerBuilder.Create().StartNow()
                                                                        .WithPriority(999999)
                                                                        .UsingJobData("Name", "Trigger2")
                                                                        .EndAt(DateTimeOffset.Now.AddMinutes(1))  //在一分钟后关闭触发器
                                                                        .WithSimpleSchedule(m => m.WithIntervalInSeconds(1).RepeatForever())
                                                                        .Build();
            //Quartz.Collection.HashSet<ITrigger> list = new HashSet<ITrigger>();
            Quartz.Collection.HashSet<ITrigger> list = new Quartz.Collection.HashSet<ITrigger>();
            list.Add(trigger);
            list.Add(trigger2);

            sche.ScheduleJob(job, list, true);
        }

        /// <summary>
        /// WithDailyTimeIntervalSchedule 可以设置某个时间段内执行定时任务
        /// </summary>
        public static void Fun8()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            //8：00 - 20：00执行任务，每隔1min执行一次
            var trigger = TriggerBuilder.Create().StartNow()
                                                                        .UsingJobData("Name", "Trigger1")
                                                                        .WithDailyTimeIntervalSchedule(m => 
                                                                            m.OnEveryDay().
                                                                            StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8,0)).
                                                                            EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(20,0)).
                                                                            WithIntervalInMinutes(1)
                                                                        ).Build();
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// WithDailyTimeIntervalSchedule 可以设置某个时间段内执行定时任务
        /// </summary>
        public static void Fun9()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            //周一至周五的 8：00 - 20：00执行任务，每隔1min执行一次
            var trigger = TriggerBuilder.Create().StartNow()
                                                                        .UsingJobData("Name", "Trigger1")
                                                                        .WithDailyTimeIntervalSchedule(m =>
                                                                            m.OnMondayThroughFriday().
                                                                            StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 0)).
                                                                            EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(20, 0)).
                                                                            WithIntervalInMinutes(1)
                                                                        ).Build();
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// CronSchedule 根据字符串来替代之前的简单计划表，日历计划表，每天计划表
        /// </summary>
        public static void Fun10()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            var job = JobBuilder.Create<MyJob>()
                          .Build();
            //秒 分 时 天 月 年 周
            //1. 天或者周必须有一个是？（？表示模糊）
            //2. * 表示所有值即，秒位为*表示每秒执行一次
            //3. - 表示范围，分钟位为10-12表示每个小时的10，11，12分执行一次
            //4. , 表示每个值即，分钟位为10，20，30表示每个小时的10，20，30分执行一次
            //5. / 表示递增，秒位为0/5表示0，5，10，15，20，25，30，35，40，45，50，55秒执行一次
            //例子：
            // * * * * * ?       => 每秒执行一次
            // 0/5 * * * * ?   => 5s执行一次
            // 0 * * * * ?       => 1min执行一次
            // 0 0 1 * * ?       => 每个月1号执行一次
            // 地址：https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontrigger.html
            var trigger = TriggerBuilder.Create().StartNow().WithCronSchedule("0/1 * * * * ?").Build();
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// DailyCalendar 排除每天某个时间段任务的执行
        /// </summary>
        public static void Fun11()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            //使任务在10点到11点间不再执行
            DailyCalendar cale = new DailyCalendar(
               DateBuilder.DateOf(10, 0, 0).DateTime,
               DateBuilder.DateOf(11, 0, 0).DateTime
            );
            sche.AddCalendar("myCalendar", cale, true, true);   

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            var trigger = TriggerBuilder.Create().StartNow().WithDailyTimeIntervalSchedule(
                                                                                            m => m.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(7, 0))
                                                                                                          .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                                                                                                          .WithIntervalInSeconds(1) 
                                                                                                          .Build()  //是任务在7点-19点间执行，执行频率：每秒执行一次
                                                                                        )
                                                                                        .ModifiedByCalendar("myCalendar")
                                                                                        .Build();
            //ModifiedByCalendar 将ICalendar的设置应用到触发器中
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// WeeklyCalendar 排除每周某个星期的任务的执行
        /// </summary>
        public static void Fun12()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();
            
            WeeklyCalendar cale = new WeeklyCalendar();
            cale.SetDayExcluded(DayOfWeek.Thursday, true);  //让星期四不触发Schedule
            //cale.SetDayExcluded(DayOfWeek.Thursday, false); //让星期四触发Schedule
            sche.AddCalendar("myCalendar", cale, true, true);

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            var trigger = TriggerBuilder.Create().StartNow().WithDailyTimeIntervalSchedule(
                                                                                            m => m.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(7, 0))
                                                                                                          .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                                                                                                          .WithIntervalInSeconds(1)
                                                                                                          .Build()  //是任务在7点-19点间执行，执行频率：每秒执行一次
                                                                                        )
                                                                                        .ModifiedByCalendar("myCalendar")
                                                                                        .Build();
            //ModifiedByCalendar 将ICalendar的设置应用到触发器中
            sche.ScheduleJob(job, trigger);
            
        }

        /// <summary>
        /// HolidayCalendar 排除某一天的任务的执行(如果涉及到同一天跨年的情况，需要多次添加不同年份)
        /// </summary>
        public static void Fun13()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            HolidayCalendar cale = new HolidayCalendar();
            cale.AddExcludedDate(DateTime.Now.AddYears(-1)); //排除去年的今天不处理
            cale.AddExcludedDate(DateTime.Now); //排除今天不处理
            sche.AddCalendar("myCalendar", cale, true, true);

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            var trigger = TriggerBuilder.Create().StartNow().WithDailyTimeIntervalSchedule(
                                                                                            m => m.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(7, 0))
                                                                                                          .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                                                                                                          .WithIntervalInSeconds(1)
                                                                                                          .Build()  //是任务在7点-19点间执行，执行频率：每秒执行一次
                                                                                        )
                                                                                        .ModifiedByCalendar("myCalendar")
                                                                                        .Build();
            //ModifiedByCalendar 将ICalendar的设置应用到触发器中
            sche.ScheduleJob(job, trigger);

        }

        /// <summary>
        /// MonthlyCalendar 排除每月某一天的任务的执行
        /// </summary>
        public static void Fun14()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            MonthlyCalendar cale = new MonthlyCalendar();
            cale.SetDayExcluded(25, true);  //排除每个月25号执行
            sche.AddCalendar("myCalendar", cale, true, true);

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            var trigger = TriggerBuilder.Create().StartNow().WithDailyTimeIntervalSchedule(
                                                                                            m => m.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(7, 0))
                                                                                                          .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                                                                                                          .WithIntervalInSeconds(1)
                                                                                                          .Build()  //是任务在7点-19点间执行，执行频率：每秒执行一次
                                                                                        )
                                                                                        .ModifiedByCalendar("myCalendar")
                                                                                        .Build();
            //ModifiedByCalendar 将ICalendar的设置应用到触发器中
            sche.ScheduleJob(job, trigger);

        }

        /// <summary>
        /// AnnualCalendar 排除每年某一天的任务的执行
        /// </summary>
        public static void Fun15()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();
            
            AnnualCalendar cale = new AnnualCalendar();
            //12月25号不执行
            cale.SetDayExcluded(new DateTimeOffset(2018, 12, 25, 12, 0, 0, TimeSpan.FromHours(8)), true);  
            sche.AddCalendar("myCalendar", cale, true, true);

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            var trigger = TriggerBuilder.Create().StartNow().WithDailyTimeIntervalSchedule(
                                                                                            m => m.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(7, 0))
                                                                                                          .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                                                                                                          .WithIntervalInSeconds(1)
                                                                                                          .Build()  //是任务在7点-19点间执行，执行频率：每秒执行一次
                                                                                        )
                                                                                        .ModifiedByCalendar("myCalendar")
                                                                                        .Build();
            //ModifiedByCalendar 将ICalendar的设置应用到触发器中
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// CronCalendar 通过Cron表达式排除任务的执行
        /// </summary>
        public static void Fun16()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            //只在营业时间执行8AM-5PM
            CronCalendar cale = new CronCalendar("* * 0-7,18-23 ? * *");

            sche.AddCalendar("myCalendar", cale, true, true);

            var job = JobBuilder.Create<MyJob>()
                          .Build();

            var trigger = TriggerBuilder.Create().StartNow().WithDailyTimeIntervalSchedule(
                                                                                            m => m.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(7, 0))
                                                                                                          .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                                                                                                          .WithIntervalInSeconds(1)
                                                                                                          .Build()  //是任务在7点-19点间执行，执行频率：每秒执行一次
                                                                                        )
                                                                                        .ModifiedByCalendar("myCalendar")
                                                                                        .Build();
            //ModifiedByCalendar 将ICalendar的设置应用到触发器中
            sche.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 多个Trigger同时调用Job时，会产生冲突，这时使用DisallowConcurrentExecution就可以让线程同步
        /// </summary>
        public static void Fun17()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            var job = JobBuilder.Create<MyConcurrentJob>()
                          .Build();
            // 地址：https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontrigger.html
            var trigger = TriggerBuilder.Create().WithIdentity("trigger1").StartNow().WithCronSchedule("0/1 * * * * ?").Build();
            var trigger2 = TriggerBuilder.Create().WithIdentity("trigger2").StartNow().WithCronSchedule("0/1 * * * * ?").Build();
            var dictionary = new Dictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>>();
            dictionary.Add(job, new Quartz.Collection.HashSet<ITrigger>() { trigger, trigger2 });
            sche.ScheduleJobs(dictionary, true);
        }

        /// <summary>
        /// 有时候我们希望让Job保持状态，这时就可以使用PersistJobDataAfterExecution 持久化Job中的数据
        /// </summary>
        public static void Fun18()
        {
            var sche = StdSchedulerFactory.GetDefaultScheduler();
            sche.Start();

            var job = JobBuilder.Create<MyConcurrentJob>().UsingJobData("count", 0)
                          .Build();
            var trigger = TriggerBuilder.Create().StartNow().WithCronSchedule("0/1 * * * * ?").Build();
            sche.ScheduleJob(job, trigger);
        }

    }
    
    /// <summary>
    /// 出队的业务逻辑
    /// </summary>
    public class ConsumerJob : IJob
    {
        public static string RabbitMQ_UserName = ConfigurationManager.AppSettings["RabbitMQ_UserName"];
        public static string RabbitMQ_Password = ConfigurationManager.AppSettings["RabbitMQ_Password"];
        public static string RabbitMQ_HostName = ConfigurationManager.AppSettings["RabbitMQ_HostName"];

        public void Execute(IJobExecutionContext context)
        {
            var key = context.JobDetail.Key;
            var map = context.JobDetail.JobDataMap;
            var UserName = map.GetString("UserName");
            var password = map.GetInt("Password");
            ConsumerJob.ConsumerBasic("test", 50);
        }

        /// <summary>
        /// 基本消费者
        /// </summary>
        public static void ConsumerBasic(string queueName,int count)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = RabbitMQ_UserName;
            factory.Password = RabbitMQ_Password;
            factory.HostName = RabbitMQ_HostName;
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //获取消息
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            channel.BasicGet("test", true);
                        }
                        Console.WriteLine(string.Format("消费{0}条数据还剩{1}条", count, channel.MessageCount(queueName)));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("队列：" + queueName + "，不存在");
                    }
                    
                }
            }
        }
        
    }

    /// <summary>
    /// 业务逻辑
    /// </summary>
    public class PublishJob : IJob
    {

        public static string RabbitMQ_UserName = ConfigurationManager.AppSettings["RabbitMQ_UserName"];
        public static string RabbitMQ_Password = ConfigurationManager.AppSettings["RabbitMQ_Password"];
        public static string RabbitMQ_HostName = ConfigurationManager.AppSettings["RabbitMQ_HostName"];

        public void Execute(IJobExecutionContext context)
        {
            PublishJob.PublishBasic("test");
        }

        /// <summary>
        /// 基础发布消息
        /// </summary>
        public static void PublishBasic(string QueueName)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = RabbitMQ_UserName;
            factory.Password = RabbitMQ_Password;
            factory.HostName = RabbitMQ_HostName;
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //创建交换机

                    //创建Queue
                    var queue = channel.QueueDeclare(QueueName, true, false, false, null);
                    if(channel.MessageCount(QueueName) > 1000)
                    {
                        Console.WriteLine("队列中的数据超过1000条，停止插入数据");
                        return;
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                       
                        //发送消息
                        channel.BasicPublish("", QueueName, basicProperties: null, body: msg);
                    }
                    Console.WriteLine(string.Format("生产出100条数据，目前还剩{0}条还未处理",
                        channel.MessageCount(QueueName)));
                }
            }
        }
        
    }

    /// <summary>
    /// 自定义任务
    /// </summary>
    public class MyJob : IJob
    {
        
        public void Execute(IJobExecutionContext context)
        {
            //context.JobDetail.JobDataMap["count"] = Convert.ToInt32(context.JobDetail.JobDataMap["count"]) + 1;
            Console.WriteLine("本地执行时间：{0}，下次执行时间：{1}",
                context.ScheduledFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"),
                context.NextFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss")
                //context.JobDetail.JobDataMap["count"]
                );
            
            //Console.WriteLine("上次执行时间：{0}", context.PreviousFireTimeUtc == null ? "不存在" : context.PreviousFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"));
            //Console.WriteLine("本次执行时间：{0}", context.FireTimeUtc == null ? "不存在" : context.FireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"));
            //Console.WriteLine("下次执行时间：{0}", context.NextFireTimeUtc == null ? "不存在" : context.NextFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"));
            //var st = context.Trigger as ISimpleTrigger;
            //if(st != null)
            //{
            //    Console.WriteLine("当前次数：{0} " , st.TimesTriggered);
            //}
        }
    }

    /// <summary>
    /// 持久化作业中的缓存数据
    /// </summary>
    [PersistJobDataAfterExecution]
    public class MyPersistJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("本地执行时间：{0}，下次执行时间：{1}，执行次数：{2}",
                context.ScheduledFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"),
                context.NextFireTimeUtc.Value.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss"),
                context.JobDetail.JobDataMap["count"]
                );
            context.JobDetail.JobDataMap["count"] = Convert.ToInt32(context.JobDetail.JobDataMap["count"]) + 1;
        }
    }
    
    /// <summary>
    /// 多个Trigger同步等待执行Job
    /// </summary>
    [DisallowConcurrentExecution]
    public class MyConcurrentJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("当前时间：{0}，当前Trigger名称：{1}", DateTime.Now.ToString() , context.Trigger.Key.Name);
        }
    }
}
