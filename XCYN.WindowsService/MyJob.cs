using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.WindowsService
{
    /// <summary>
    /// 任务类.
    /// </summary>
    internal class MyJob : IJob
    {
        /// <summary>
        /// 执行任务的方法.
        /// </summary>
        public void Execute(IJobExecutionContext context)
        {
            // 查看网络链接是否正常
            var flag = PingIPAddress("www.baidu.com");
            if(flag == false)
            {
                File.AppendAllText("C:\\InternetStatus.txt",
                    string.Format("{0}，网络异常{1}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.NewLine));
            }
            else
            {
                File.AppendAllText("C:\\InternetStatus.txt",
                    string.Format("{0}，网络正常{1}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.NewLine));
            }
        }

        /// <summary>
        /// ping一个ip地址
        /// </summary>
        /// <returns></returns>
        public static bool PingIPAddress(string ip)
        {
            Ping ping = new Ping();
            try
            {
                string data = "x";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                PingReply pingReply = ping.Send(ip, 500, buffer);
                if (pingReply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
