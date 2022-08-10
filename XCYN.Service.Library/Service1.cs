using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using XCYN.Service.Library.Model;

namespace XCYN.Service.Library
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    [JavascriptCallbackBehavior(UrlParameterName = "jsonpCallback")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service1 : IService1
    {
        /// <summary>
        /// 阈值，每秒钟最多访问16次
        /// </summary>
        private static int QPS = 16;

        /// <summary>
        /// 重置访问次数的时间间隔
        /// </summary>
        /// <remarks>1秒 = 1000号码，1毫秒=10000ticks.</remarks>
        private static int TIME_WINDOWS = 1 * 10000 * 1000;

        /// <summary>
        /// 起始时间.
        /// </summary>
        private static long START_TIME = DateTime.Now.Ticks;

        /// <summary>
        /// 计数器.
        /// </summary>
        private static AtomicInteger REC_COUNT = new AtomicInteger();

        /// <summary>
        /// 获取数据.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetData(int value)
        {
            return "";
        }

        /// <summary>
        /// 验证请求次数是否超过限制.
        /// </summary>
        /// <returns></returns>
        private bool TryAcquire()
        {
            if (DateTime.Now.Ticks - Service1.START_TIME > TIME_WINDOWS)
            {
                // 每隔1s重置一次计数器
                Service1.REC_COUNT.Set(0);
                Service1.START_TIME = DateTime.Now.Ticks;
            }

            return Service1.REC_COUNT.IncrementAndGet() < Service1.QPS;
        }

        /// <summary>
        /// 一种简单的限制流量的算法.
        /// </summary>
        /// <returns></returns>

        public string LimitRequest()
        {
            string msg = string.Empty;
            try
            {
                if (TryAcquire())
                {
                    msg = DateTime.Now.ToString() + "：请求成功";
                    Debug.WriteLine(msg);
                    return msg;
                }
                else
                {
                    // 请求超时
                    throw new WebFaultException(System.Net.HttpStatusCode.GatewayTimeout);
                }
            }
            catch (Exception ex)
            {
                // 服务器错误
                Debug.WriteLine(DateTime.Now.ToString() + "：" + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 服务器列表.
        /// </summary>
        private List<IPAddressRight> _service_map = new List<IPAddressRight>()
        {
            new IPAddressRight("192.168.1.1", 2 ),
            new IPAddressRight("192.168.1.2", 2 ),
            new IPAddressRight("192.168.1.3", 2 ),
            new IPAddressRight("192.168.1.4", 4 ),
        };

        /// <summary>
        /// 访问下标.
        /// </summary>
        private int index = 0;

        /// <summary>
        /// 负载均衡加权算法.
        /// </summary>
        /// <returns></returns>
        public string LoadBalance()
        {
            List<string> list_temp = new List<string>();
            for (int i = 0; i < _service_map.Count; i++)
            {
                for (int j = 0; j < _service_map.ElementAt(i).Right; j++)
                {
                    // 优先级高的在列表中的数量就多
                    list_temp.Add(_service_map.ElementAt(i).Address);
                }
            }
            
            if(index == list_temp.Count)
            {
                index = 0;
            }
            
            // 被调用的服务器地址
            string invokeServiceName = list_temp.ElementAt(index);
            int indexOfServiceMap = this._service_map.FindIndex(m => m.Address.Equals(invokeServiceName));
            if(indexOfServiceMap >= 0)
            {
                this._service_map.ElementAt(indexOfServiceMap).Count++;
            }

            // 日志输出
            Debug.WriteLine(DateTime.Now.ToString() + "：调用" + invokeServiceName);
            StringBuilder builder = new StringBuilder();

            Debug.WriteLine("统计：");
            for (int i = 0; i < this._service_map.Count; i++)
            {
                builder.AppendFormat("调用{0}，{1}次\r\n", this._service_map[i].Address, this._service_map[i].Count);
            }

            Debug.WriteLine(builder.ToString());
            index++;
            return invokeServiceName + "\r\n" + builder.ToString();
        }
    }
}
