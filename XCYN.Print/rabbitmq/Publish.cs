using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.rabbitmq
{
    public class Publish
    {
        /// <summary>
        /// 基础发布消息
        /// </summary>
        public static void PublishBasic()
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
                    //创建交换机
                    
                    //创建Queue
                    var queue = channel.QueueDeclare("test", true, false, false, null);

                    for (int i = 0; i < 100; i++)
                    {
                        var msg = Encoding.UTF8.GetBytes(string.Format("{0},你好",i));
                        //发送消息
                        channel.BasicPublish("", "test", basicProperties: null, body: msg);
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
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
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
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
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
            factory.UserName = "root";
            factory.Password = "900424";
            factory.HostName = "192.168.1.111";
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
        /// 通过Header生产消息
        /// </summary>
        public static void PublishHeader()
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
                    //创建交换机
                    //channel.ExchangeDeclare("MyExchange", ExchangeType.Fanout, true, false, null);
                    //创建Queue
                    //var queue = channel.QueueDeclare("test", true, false, false, null);
                    var prop = channel.CreateBasicProperties();
                    prop.Headers = new Dictionary<string, object>();
                    prop.Headers.Add("userName", "root");
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
    }
}
