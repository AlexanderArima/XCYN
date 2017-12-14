using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.rabbitmq
{
    public class Consumer
    {
        public static void ConsumerBasic()
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
                   var result = channel.BasicGet("test", true);
                   var msg = Encoding.UTF8.GetString(result.Body);
                }
            }
        }

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
    }
}
