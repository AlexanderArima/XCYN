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

        public void DoWork()
        {
        }

        public double GetPrice(string ticker)
        {
            return 0.00;
        }
    }
}
