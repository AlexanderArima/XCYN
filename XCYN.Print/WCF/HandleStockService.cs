using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.WCF
{
    public class HandleStockService
    {
        public void Fun1()
        {
            StockServiceReference.StockServiceClient proxy = new Print.StockServiceReference.StockServiceClient();
            var price = proxy.GetPrice("x1");
            Console.WriteLine("price:"+price);
            Console.Read();
        }
    }
}
