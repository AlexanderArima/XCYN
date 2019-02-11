using Quartz;
using Quartz.Impl;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.rabbitmq;

namespace XCYN.Print.Quartz
{
    public class MyQuartz
    {
        public static void Fun1()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

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

        public static void Fun2()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            var job = JobBuilder.Create<PublishJob>().Build();

            var trigger = TriggerBuilder.Create().WithSimpleSchedule(m => m.WithIntervalInSeconds(1)
                                                                            .RepeatForever())
                                                 .StartNow()
                                                 .Build();

            scheduler.ScheduleJob(job, trigger);
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
             ConsumerJob.ConsumerBasic("test",50);
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
}
