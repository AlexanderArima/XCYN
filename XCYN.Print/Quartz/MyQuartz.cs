using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.rabbitmq;

namespace XCYN.Print.Quartz1
{
    public class MyQuartz
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

        static int Count = 0;

        public void Execute(IJobExecutionContext context)
        {
            //Count++;
            //if(Count >= 5)
            //{
            //    var trigger = context.Scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
            //    context.Scheduler.UnscheduleJob(trigger.ElementAt(0));
            //}
            //var keys = context.Scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
            //Console.WriteLine(keys.Count);
            //var dict = context.MergedJobDataMap;
            //Console.WriteLine(dict.Keys.ElementAt(0) + ":" + dict.Values.ElementAt(0));
            Console.WriteLine("定时执行");
        }
    }
}
