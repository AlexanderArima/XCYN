using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XCYN.Service.WCF
{
    //使用代码和配置文件编写WCF服务
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        double GetPrice(string ticker);
    }
}
