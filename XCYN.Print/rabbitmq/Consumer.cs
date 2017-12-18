using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.rabbitmq
{
    public class Consumer
    {
        /// <summary>
        /// 基本消费者
        /// </summary>
        public static void ConsumerBasic()
        {
            Console.WriteLine("开启消息消费者");
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //获取消息
                   var result = channel.BasicGet("test", true);
                   var msg = Encoding.UTF8.GetString(result.Body);
                   Console.WriteLine(msg);
                }
            }
        }

        /// <summary>
        /// 通过订阅消费消息
        /// </summary>
        public static void ConsumerWorkQueue()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("test", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        #region 直连(Direct)的方式，消费不同的RoutingKey
        /// <summary>
        /// 消费普通日志
        /// </summary>
        public static void ConsumerLogElse()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //添加交换机
                    channel.ExchangeDeclare("MyExchange", ExchangeType.Direct, true, false, null);
                    //创建队列
                    channel.QueueDeclare("log_else",true,false,false,null);
                    var arr = new string[3]{"info","warning","debug"};
                    //绑定队列和交换机
                    for (int i = 0; i < arr.Length; i++)
                    {
                        channel.QueueBind("log_else","MyExchange",arr[i],null);
                    }
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("log_else", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        /// <summary>
        /// 消费错误日志
        /// </summary>
        public static void ConsumerLogError()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //添加交换机
                    channel.ExchangeDeclare("MyExchange", ExchangeType.Direct, true, false, null);
                    //创建队列
                    channel.QueueDeclare("log_error", true, false, false, null);
                    var arr = new string[1] { "error" };
                    //绑定队列和交换机
                    for (int i = 0; i < arr.Length; i++)
                    {
                        channel.QueueBind("log_error", "MyExchange", arr[i], null);
                    }
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("log_error", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        #endregion

        #region 多播(Fanout)的方式，往多个Queue传递同一个消息
        /// <summary>
        /// 通过多播(Fanout)消费消息
        /// </summary>
        public static void ConsumerFanout()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //声明交换机
                    channel.ExchangeDeclare("MyExchange", ExchangeType.Fanout, true, false, null);
                    channel.QueueDeclare(queue: "test", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    //声明一个队列
                    channel.QueueBind("test", "MyExchange", "", null);
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("test", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        /// <summary>
        /// 通过多播(Fanout)消费消息
        /// </summary>
        public static void ConsumerFanout2()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //声明交换机
                    channel.ExchangeDeclare("MyExchange", ExchangeType.Fanout, true, false, null);
                    channel.QueueDeclare(queue: "test2", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    //声明一个队列
                    channel.QueueBind("test2", "MyExchange", "", null);
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("test2", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        #endregion

        #region 消息头(Headers)的方式，根据发送的消息内容中的headers属性进行匹配
        /// <summary>
        /// Header类型
        /// </summary>
        public static void ConsumerHeader()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            using(var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    //当x-match为all时，必须满足所有的条件
                    //dict.Add("x-match", "any");
                    //当x-match为any时，最少满足一个条件即可
                    dict.Add("x-match", "all");
                    dict.Add("userName", "root");
                    dict.Add("password", "123456");
                    channel.ExchangeDeclare("MyExchange", ExchangeType.Headers, true, false, null);
                    channel.QueueDeclare("test", true, false, false, null);
                    channel.QueueBind("test", "MyExchange", "", dict);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("test", true, consumer);
                    Console.ReadKey();
                }
            }
        }
        #endregion

        #region 主题(topic)的方式，分组归类发送消息
        /// <summary>
        /// 通过主题(topic)分组归类发送消息
        /// </summary>
        public static void ConsumerTopic1()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("MyExchange", ExchangeType.Topic, true, false, null);
                    channel.QueueDeclare("queue1", true, false, false, null);
                    //#可以匹配0到n个标识符
                    channel.QueueBind("queue1", "MyExchange", "#.com", null);
                    //.可以匹配一个标识符
                    //channel.QueueBind("queue1", "MyExchange", "*.com", null);
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("queue1", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        /// <summary>
        /// 通过主题(topic)分组归类发送消息
        /// </summary>
        public static void ConsumerTopic2()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("MyExchange", ExchangeType.Topic, true, false, null);
                    channel.QueueDeclare("queue2", true, false, false, null);
                    channel.QueueBind("queue2", "MyExchange", "*.cn", null);
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("queue2", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        #endregion

        /// <summary>
        /// QueueDeclarePassive用于判断队列是否存在，及队列的相关信息
        /// </summary>
        public static void ConsumerPassive()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        var queue = channel.QueueDeclarePassive("test");
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("test", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

        /// <summary>
        /// 消费队列中死信交换机和路由键
        /// </summary>
        public static void ConsumerDeadLetterExchangeAndRoutingKey()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
            //创建connection
            using (var connection = factory.CreateConnection())
            {
                //创建channel
                using (var channel = connection.CreateModel())
                {
                    //声明死信的交换机和队列，被删除的消息会发到这里面
                    channel.ExchangeDeclare("dead_exchange", ExchangeType.Direct, false, false, null);
                    channel.QueueDeclare("dead_queue", false, false, false, null);
                    channel.QueueBind("dead_queue","dead_exchange","dead_queue",null);
                    //获取消息
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var msg = Encoding.UTF8.GetString(e.Body);
                        Console.WriteLine(msg);
                    };
                    channel.BasicConsume("dead_queue", true, consumer);
                    Console.ReadKey();
                    //var result = channel.BasicGet("test", true);
                    //var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

    }
}
