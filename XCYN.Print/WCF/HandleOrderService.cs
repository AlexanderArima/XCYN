using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.WCF
{
    public class HandleOrderService
    {
        public void Fun1()
        {
            OrderServiceReference.OrderServiceClient proxy = new OrderServiceReference.OrderServiceClient();
            var price = proxy.GetPrice("x1");
            Console.WriteLine("price:" + price);
            Console.Read();
        }
    }
}
