using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XCYN.Service.WCF
{
    //完全用代码编写WCF
    [ServiceContract]
    public interface IStockService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        double GetPrice(string ticker);
    }
}
