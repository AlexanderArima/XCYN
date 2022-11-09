using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;

namespace XCYN.Winform.WebSocket
{
    /// <summary>
    /// WebSocket后台服务.
    /// </summary>
    public class FleckServer
    {

        public void Init()
        {
            FleckLog.Level = LogLevel.Debug;
            List<IWebSocketConnection> list = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:7181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("启动...");
                    list.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("关闭...");
                    list.Remove(socket);
                };
                socket.OnMessage = msg =>
                {
                    Console.WriteLine("消息：" + msg);
                    list.ForEach(m =>
                    {
                        m.Send("消息：" + msg);
                    });
                };
            });

            var input = Console.ReadLine();
            if (input.Equals("exit"))
            {
                foreach (var item in list)
                {
                    item.Send(input);
                }

                input = Console.ReadLine();
            }
        }
    }
}
