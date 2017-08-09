using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.WS.APP
{
    class Program
    {
        static void Main(string[] args)
        {
            //AsClient();
            AsServer();
        }

        /// <summary>
        /// 作为服务器开发接口调用
        /// </summary>
        private static void AsServer()
        {
            Console.WriteLine("启动服务");
            WebApp.Start("http://localhost:9004");
            Console.Read();
        }

        /// <summary>
        /// 作为客户端调用IIS服务器的接口
        /// </summary>
        private static void AsClient()
        {
            //连接地址
            var conn = new HubConnection("http://localhost:51300/myConn");
            //代理类
            var proxy = conn.CreateHubProxy("ChatHub");
            //启动链接
            conn.Start().Wait();
            //监听方法
            proxy.On("allChat", (msg) =>
            {
                Console.WriteLine(msg);
            });
            //调用服务器方法
            proxy.Invoke("allChat");
            Console.Read();
        }
    }
}
