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
        StockPrice GetPrice(string ticker);

        [OperationContract]
        double SetPrice(StockPrice price);

        /// <summary>
        /// 单向操作，客户端调用服务后即刻返回结果
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void DoBigAnalysisFast();

        [OperationContract(IsOneWay = false)]
        void DoBigAnslysisSlow();
    }
}
