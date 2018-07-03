using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.StockServiceReference;
//using XCYN.Service.WCF;

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
            //创建一个信道工厂
            ChannelFactory<XCYN.Service.WCF.IStockService> channel = new ChannelFactory<XCYN.Service.WCF.IStockService>(
                new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8000/StockService"));
            //创建一个信道
            var client = channel.CreateChannel();
            var price = client.GetPrice("Hello World");
            Console.WriteLine("price:" + price);
            Console.Read();
        }

        /// <summary>
        /// 采用异步的方式访问WCF服务
        /// </summary>
        public void Fun3()
        {
            StockServiceClient client = new StockServiceClient();
            var result = client.BeginGetPrice("cheng", m => {
                var price = ((StockServiceClient)m.AsyncState).EndGetPrice(m);
                Console.WriteLine("price:" + price.ToString());
            }, client);
            Console.Read();
        }

        /// <summary>
        /// 调用单向操作，不用等待10s
        /// </summary>
        public void Fun4()
        {
            StockServiceClient client = new StockServiceClient();
            client.DoBigAnalysisFast();
            Console.WriteLine("调用单向操作完成");
            Console.Read();
        }

        /// <summary>
        /// 不调用单向操作，需要等待10s
        /// </summary>
        public void Fun5()
        {
            StockServiceClient client = new StockServiceClient();
            client.DoBigAnslysisSlow();
            Console.WriteLine("不调用单向操作完成");
            Console.Read();
        }
        
    }
}
