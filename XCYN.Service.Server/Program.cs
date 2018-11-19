using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XCYN.Service.Library;

namespace XCYN.Service.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service1));

            host.Open();
            Console.WriteLine("启动服务");

            Console.Read();
        }
    }
}
