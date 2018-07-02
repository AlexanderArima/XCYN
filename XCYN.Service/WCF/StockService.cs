using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace XCYN.Service.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“StockService”。
    public class StockService : IStockService
    {
        public void DoBigAnalysisFast()
        {
            Thread.Sleep(10000);
        }

        public void DoBigAnslysisSlow()
        {
            Thread.Sleep(10000);
        }
        
        public StockPrice GetPrice(string ticker)
        {
            var price = new StockPrice()
            {
                theTickerSymbol = ticker,
                theCurrentPriceNow = 100,
                theCurrentTimeNow = DateTime.Now,
                theDailyVolumeSoFar = 450000,
                theDailyChangeSoFar = .123456
            };
            return price;
        }
        
        public double SetPrice(StockPrice price)
        {
            return price.theCurrentPriceNow;
        }


       

    }
}
