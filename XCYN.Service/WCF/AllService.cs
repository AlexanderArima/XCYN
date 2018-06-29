using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XCYN.Service.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“IAllService”。
    public class AllService : IAllService
    {
        public void DoBigAnalysisFast()
        {
            
        }

        public void DoBigAnslysisSlow()
        {
            
        }

        public void DoWork()
        {
            
        }

        public double GetOrder(string ticker)
        {
            return 1990;
        }

        public double GetPrice(string ticker)
        {
            return 1992;
        }
    }
}
