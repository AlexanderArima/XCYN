using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using XCYN.ServiceReference1;
using System.ServiceModel.Channels;

namespace XCYN
{
    class Program
    {
        static void Main(string[] args)
        {
            //调用WCF服务
            ServiceReference1.ServiceFlyClient client = new ServiceReference1.ServiceFlyClient();
            client.fly(new Student() {
                Name = "Xiao",
                Age = 17
            });
            Console.ReadKey();
        }
    }
}
