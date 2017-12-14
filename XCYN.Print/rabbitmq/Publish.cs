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
                    var msg = Encoding.UTF8.GetBytes("你好");
                    //发送消息
                    channel.BasicPublish(string.Empty, "test", basicProperties: null, body: msg);
                }
            }
        }
    }
}
