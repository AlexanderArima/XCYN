using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.delegates;
using XCYN.Print.linq;
using XCYN.Print.rabbitmq;
using XCYN.Print.redis;
using XCYN.Print.yield;

namespace XCYN.Print
{
    class Program
    {
        
        static void Main(string[] args)
        {
            BasicCommand command = new BasicCommand();
            var name = command.ListLeftPop("list_name");
            Console.WriteLine(name);
            Console.ReadKey();
            //Consumer.ConsumerWorkQueue();
        }
    }
}
