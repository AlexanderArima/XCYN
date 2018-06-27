using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XCYN.Service.WCF;

namespace XCYN.Print.WCF
{
    /// <summary>
    /// 访问WCF的客户端
    /// </summary>
    public class HandleStockClient
    {
        /// <summary>
        /// 使用代码和配置文件编写WCF客户端
        /// </summary>
        public void Fun1()
        {
            StockServiceReference.StockServiceClient proxy = new Print.StockServiceReference.StockServiceClient();
            var price = proxy.GetPrice("x1");
            Console.WriteLine("price:"+price);
            Console.Read();
        }
        
        /// <summary>
        /// 完全用代码访问WCF服务
        /// </summary>
        public void Fun2()
        {
            ChannelFactory<XCYN.Service.WCF.IStockService> channel = new ChannelFactory<IStockService>(
                new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8000/StockService"));

            var client = channel.CreateChannel();
            var price = client.GetPrice("Hello World");
            Console.WriteLine("price:" + price);
            Console.Read();
        }
    }
}
