using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.delegates;
using XCYN.Print.linq;
using XCYN.Print.rabbitmq;
using XCYN.Print.yield;

namespace XCYN.Print
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Publish.PublishPriority();
            //Consumer.ConsumerWorkQueue();
        }
    }
}
