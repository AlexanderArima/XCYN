using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Common
{
    public static class IPAddressHelper
    {
        /// <summary>
        /// 验证IP地址字符串的正确性
        /// </summary>
        /// <param name="strIP">要验证的IP地址字符串</param>
        /// <returns>格式是否正确，正确为 true 否则为 false</returns>
        public static bool CheckIPAddr(string strIP)
        {
            if (string.IsNullOrEmpty(strIP))
            {
                return false;
            }
            // 保持要返回的信息
            bool bRes = true;
            int iTmp = 0;    // 保持每个由“.”分隔的整型
            string[] ipSplit = strIP.Split('.');
            if (ipSplit.Length < 4 || string.IsNullOrEmpty(ipSplit[0]) ||
                string.IsNullOrEmpty(ipSplit[1]) ||
                string.IsNullOrEmpty(ipSplit[2]) ||
                string.IsNullOrEmpty(ipSplit[3]))
            {
                bRes = false;
            }
            else
            {
                for (int i = 0; i < ipSplit.Length; i++)
                {
                    if (!int.TryParse(ipSplit[i], out iTmp) || iTmp < 0 || iTmp > 255)
                    {
                        bRes = false;
                        break;
                    }
                }
            }

            return bRes;
        }

        /// <summary>
        /// 验证某个IP是否可ping通
        /// </summary>
        /// <param name="strIP">要验证的IP</param>
        /// <param name="timeout">最大等待时间，超过了这个时间，就认为ping不通</param>
        /// <returns>是否可连通  是：true 否：false</returns>
        public static bool TestNetConnectity(string strIP,int timeout = 1000)
        {
            if (!CheckIPAddr(strIP))
            {
                return false;
            }
            // Windows L2TP VPN和非Windows VPN使用ping VPN服务端的方式获取是否可以连通
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "testtesttesttesttesttesttesttest";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            var reply = pingSender.SendPingAsync(strIP, timeout, buffer, options);
            return (reply.Result.Status == IPStatus.Success);
        }
        
        /// <summary>
        /// 连续几次查看是否某个IP可以PING通，如果能连通则立即返回true，否则将多次尝试连接
        /// </summary>
        /// <param name="strIP">ping的IP地址</param>
        /// <param name="WaitSecond">每次间隔时间，单位：秒</param>
        /// <param name="iTestTimes">测试次数</param>
        /// <returns>是否可以连通</returns>
        public static bool TestNetConnected(string strIP, int timeout = 1000, int WaitSecond = 5, int iTestTimes = 10)
        {
            for (int i = 0; i < iTestTimes - 1; i++)
            {
                if (TestNetConnectity(strIP, timeout))
                {
                    return true;
                }
                Thread.Sleep(WaitSecond * 1000);
            }

            return TestNetConnectity(strIP);
        }

    }
}
