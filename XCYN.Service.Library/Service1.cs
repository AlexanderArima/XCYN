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
    }
}
