using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using XCYN.Service.WCF;

namespace XCYN.Service
{
    class Program
    {

        static void Main(string[] args)
        {
            HandleOrderService();
        }

        /// <summary>
        /// 使用代码和配置文件编写WCF
        /// </summary>
        public static void HandleOrderService()
        {
            ServiceHost host = new ServiceHost(typeof(OrderService));
            host.Open();
            Console.WriteLine("服务地址：http://localhost:8733/OrderService/");
            Console.WriteLine("点击回车键关闭服务");
            Console.ReadLine();
            host.Close();
        }

        /// <summary>
        /// 完全用代码编写WCF
        /// </summary>
        public static void HandleStockService()
        {
            ServiceHost host = new ServiceHost(typeof(StockService), new Uri("http://localhost:8000/StockService"));
            host.AddServiceEndpoint(typeof(IStockService), new BasicHttpBinding(), "");
            host.Open();
            Console.WriteLine("服务地址：http://localhost:8000/StockService");
            Console.WriteLine("点击回车键关闭服务");
            Console.ReadLine();
            host.Close();
        }

        public static void HandleServiceFly()
        {
            ServiceHost host = new ServiceHost(typeof(ServiceFly));
            //host.Description.Endpoints[0].EndpointBehaviors.Add(new MyEndpointBehavior());
            host.Open();
            Console.WriteLine("WCF服务已打开");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 操作行为
    /// </summary>
    public class MyOperationBehavior : Attribute, IOperationBehavior
    {

        public int length { get; set; }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new MyParameterInspector(length));
        }

        public void Validate(OperationDescription operationDescription)
        {
            
        }
    }

    /// <summary>
    /// 参数检查器
    /// </summary>
    public class MyParameterInspector : IParameterInspector
    {

        int _length;

        public MyParameterInspector(int length)
        {
            _length = length;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            for(int i = 0;i < inputs.Length;i++)
            {
                if(inputs[i].ToString().Length > _length)
                {
                    throw new Exception("长度不能超过" + _length);
                }
            }
            return null;
        }
    }

    /// <summary>
    /// 消息检查器
    /// </summary>
    public class MyDispatchMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var ip = request.Headers.GetHeader<string>("ip", string.Empty);
            var dt = request.Headers.GetHeader<string>("dt", string.Empty);
            var token = request.Headers.GetHeader<string>("token", string.Empty);
            return request;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

        }
    }

    /// <summary>
    /// 结点行为
    /// </summary>
    public class MyEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MyDispatchMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            
        }
    }
}
