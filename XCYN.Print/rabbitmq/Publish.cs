using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.rabbitmq
{
    public class Publish
    {

        public static string RabbitMQ_UserName = ConfigurationManager.AppSettings["RabbitMQ_UserName"];
        public static string RabbitMQ_Password = ConfigurationManager.AppSettings["RabbitMQ_Password"];
        public static string RabbitMQ_HostName = ConfigurationManager.AppSettings["RabbitMQ_HostName"];

        /// <summary>
        /// 基础发布消息
        /// </summary>
        public static void PublishBasic()
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
                    var queue = channel.QueueDeclare("test", true, false, false, null);

                    for (int i = 0; i < 10; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好",i));
                        Console.WriteLine("生产出：" + string.Format("{0},你好", i));
                        //发送消息
                        channel.BasicPublish("", "test", basicProperties: null, body: msg);
                    }
                }
            }
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

                    for (int i = 0; i < 10; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                        //Console.WriteLine("生产出：" + string.Format("{0},你好", i));
                        //发送消息
                        channel.BasicPublish("", QueueName, basicProperties: null, body: msg);
                    }
                }
            }
        }


        /// <summary>
        /// 带上交换机发布消息
        /// </summary>
        public static void PublishWithExchange()
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
                    channel.ExchangeDeclare("myexchange", ExchangeType.Direct, true, false, null);
                    //创建Queue
                    var queue = channel.QueueDeclare("test", true, false, false, null);
                    channel.QueueBind("test", "myexchange", "test", null);//绑定交换机
                    for (int i = 0; i < 100; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                        //发送消息
                        channel.BasicPublish("myexchange", "test", basicProperties: null, body: msg);
                    }
                }
            }
        }

        /// <summary>
        /// 直连(Direct)的方式，发布不同的routingkey
        /// </summary>
        public static void PublishLog()
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
                    //var queue = channel.QueueDeclare("test", true, false, false, null);
                    
                    for (int i = 0; i < 100; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                        var queue = i % 13 == 0 ? "error" : "info";
                        //发送消息,只需要交换机和routingKey
                        channel.BasicPublish("MyExchange", queue, basicProperties: null, body: msg);
                    }
                }
            }
        }

        /// <summary>
        /// 通过多播的方式生产消息
        /// </summary>
        public static void PublishFanout()
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
                    //channel.ExchangeDeclare("MyExchange", ExchangeType.Fanout, true, false, null);
                    //创建Queue
                    //var queue = channel.QueueDeclare("test", true, false, false, null);
                    //此处不创建队列
                    for (int i = 0; i < 100; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                        //var queue = i % 13 == 0 ? "error" : "info";
                        //发送消息,只需要交换机和routingKey
                        channel.BasicPublish("MyExchange", "", basicProperties: null, body: msg);
                    }
                }
            }
        }

        /// <summary>
        /// 通过消息头(Header)生产消息
        /// </summary>
        public static void PublishHeader()
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
                    //channel.ExchangeDeclare("MyExchange", ExchangeType.Fanout, true, false, null);
                    //创建Queue
                    //var queue = channel.QueueDeclare("test", true, false, false, null);
                    var prop = channel.CreateBasicProperties();
                    prop.Headers = new Dictionary<string, object>();
                    prop.Headers.Add("userName", RabbitMQ_UserName);
                    prop.Headers.Add("password", "123456");
                    //此处不创建队列
                    for (int i = 0; i < 100; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                        //var queue = i % 13 == 0 ? "error" : "info";
                        //发送消息,只需要交换机和routingKey
                        channel.BasicPublish("MyExchange", "", basicProperties: prop, body: msg);
                    }
                }
            }
        }

        /// <summary>
        /// 通过Topic生产消息
        /// </summary>
        public static void PublishTopic()
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
                    //channel.ExchangeDeclare("MyExchange", ExchangeType.Fanout, true, false, null);
                    //创建Queue
                    //var queue = channel.QueueDeclare("test", true, false, false, null);
                    var prop = channel.CreateBasicProperties();
                    prop.Headers = new Dictionary<string, object>();
                    prop.Headers.Add("userName", RabbitMQ_UserName);
                    prop.Headers.Add("password", "123456");
                    //此处不创建队列
                    for (int i = 0; i < 100; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                        //var queue = i % 13 == 0 ? "error" : "info";
                        //发送消息,只需要交换机和routingKey
                        channel.BasicPublish("MyExchange", i % 13 == 0 ? "com" : "sina.cn", basicProperties: prop, body: msg);
                    }
                }
            }
        }

        /// <summary>
        /// 生产一个非持久化的队列(重启服务后该队列会被删除)
        /// </summary>
        public static void PublishNonDurable()
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
                    var durable = false;
                    //创建队列
                    channel.QueueDeclare("queueNonDurable", durable, false, false, null);
                }
            }
        }

        /// <summary>
        /// 生产一个排外的队列(只能由当前连接访问，当连接关闭时删除队列)
        /// </summary>
        public static void PublishExclusive()
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
                    var Exclusive = true;
                    //创建队列
                    channel.QueueDeclare("queueExclusive", true, Exclusive, false, null);
                }
            }
        }

        /// <summary>
        /// 生产一个自动删除的队列(当没有消费者时，删除该队列)
        /// </summary>
        public static void PublishAutoDelete()
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
                    var AutoDelete = true;
                    //创建队列
                    channel.QueueDeclare("queue", true, false, AutoDelete, null);
                }
            }
        }

        /// <summary>
        /// 生产一个自动删除的队列(当没有消费者时，删除该队列)
        /// </summary>
        public static void PublishPassive()
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
                    //创建队列
                    channel.QueueDeclare("test", true, false, false, null);
                    for (int i = 0; i < 1000; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("你好,{0}", i));
                        channel.BasicPublish(string.Empty, "test", null, msg);
                    }
                   
                }
            }
        }

        /// <summary>
        /// 生产一个有生命周期(Time-To-Live)的消息
        /// </summary>
        public static void PublishMessageTTL()
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
                    //定义一个队列中所有的消息的生命周期
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("x-message-ttl", 60000);
                    //创建队列
                    channel.QueueDeclare("test", false, false, false,dict);
                    var msg = Encoding.UTF8.GetBytes(string.Format("你好"));
                    //定义一个消息的生命周期
                    //当同时定义了两个生命周期时，以小的为准。
                    var properties = channel.CreateBasicProperties();
                    properties.Expiration = "8000";
                    channel.BasicPublish(string.Empty, "test", properties, msg);
                }
            }
        }

        /// <summary>
        /// 当一个队列不被使用时，过多久会被删除
        /// </summary>
        public static void PublishAutoExpire()
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
                    //定义一个队列中所有的消息的生命周期
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("x-expires", 11000);
                    //创建队列
                    channel.QueueDeclare("test", false, false, false, dict);
                    var msg = Encoding.UTF8.GetBytes(string.Format("你好"));
                    channel.BasicPublish(string.Empty, "test", null, msg);
                }
            }
        }

        /// <summary>
        /// 设置队列中消息的最大数量(LRU算法，保留最近调用过的消息)
        /// </summary>
        public static void PublishMaxLength()
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
                    //定义一个队列中所有的消息的生命周期
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("x-max-length", 10);
                    //创建队列
                    channel.QueueDeclare("test", false, false, false, dict);
                    for (int i = 0; i < 20; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("你好,{0}",i));
                        channel.BasicPublish(string.Empty, "test", null, msg);
                    }
                    
                }
            }
        }

        /// <summary>
        /// 设置队列中消息的最大数量(LRU算法，保留最近调用过的消息)
        /// </summary>
        public static void PublishMaxLengthByte()
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
                    //定义一个队列中所有的消息的生命周期
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("x-max-length-bytes", 100);
                    //创建队列
                    channel.QueueDeclare("test", false, false, false, dict);
                    for (int i = 0; i < 20; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("你好,{0}", i));//占8字节
                        channel.BasicPublish(string.Empty, "test", null, msg);
                    }

                }
            }
        }

        /// <summary>
        /// 设置队列中死信交换机和路由键
        /// </summary>
        public static void PublishDeadLetterExchangeAndRoutingKey()
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
                    //定义消息的最大长度和死信交换机和队列
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("x-max-length", 10);
                    dict.Add("x-dead-letter-exchange", "dead_exchange");
                    dict.Add("x-dead-letter-routing-key", "dead_queue");
                    //创建队列
                    channel.QueueDeclare("test", false, false, false, dict);
                    for (int i = 0; i < 20; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("你好,{0}", i));//占8字节
                        channel.BasicPublish(string.Empty, "test", null, msg);
                    }

                }
            }
        }

        /// <summary>
        /// 设置队列中的优先级
        /// </summary>
        public static void PublishPriority()
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
                    //定义队列的最大优先级
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("x-max-priority", 10);
                    //创建队列
                    channel.QueueDeclare("test", false, false, false, dict);
                    for (int i = 0; i < 10; i++)
                    {
                        //设置消息的优先级
                        var properties = channel.CreateBasicProperties();
                        properties.Priority = (byte)i;
                        var msg = Encoding.UTF8.GetBytes(string.Format("你好,{0}", i));//占8字节
                        channel.BasicPublish(string.Empty, "test", properties, msg);
                    }
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// 发布消息确认
        /// </summary>
        public static void PublishTX()
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
                    var queue = channel.QueueDeclare("test", true, false, false, null);
                    //开启确认机制
                    channel.TxSelect();
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                            //发送消息
                            channel.BasicPublish("", "test", true, basicProperties: null, body: msg);
                        }
                        //消息确认
                        channel.TxCommit();
                    }
                    catch
                    {
                        //回滚
                        channel.TxRollback();
                    }

                }
            }
        }

        /// <summary>
        /// 消息持久化
        /// </summary>
        public static void PublishPersistent()
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
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    //dict.Add("x-queue-mode", "lazy");
                    //创建Queue
                    var queue = channel.QueueDeclare("test", true, false, false, null);
                    for (int i = 0; i < 10; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好", i));
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;//持久化消息
                        //发送消息
                        channel.BasicPublish("", "test", basicProperties: properties, body: msg);
                    }
                }
            }
        }

        /// <summary>
        /// 队列懒加载
        /// </summary>
        /// <remarks>消息发布后存放在disk中，不占用内存空间。默认的消息发布后会存放在内存中，当内存不足时，才会将消息存放在disk中</remarks>
        public static void PublishLazy()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = RabbitMQ_UserName;
            factory.Password = RabbitMQ_Password;
            factory.HostName = "192.168.1.107";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //创建交换机
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("x-queue-mode", "lazy");
                    //创建Queue
                    var queue = channel.QueueDeclare("test", true, false, false, dict);
                    for (int i = 0; i < int.MaxValue; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0}", string.Join(",", Enumerable.Range(0, 10000))));
                        //发送消息
                        channel.BasicPublish("", "test", basicProperties: null, body: msg);
                        Console.WriteLine("发布消息,{0}",i);
                    }
                }
            }
        }

    }
}
