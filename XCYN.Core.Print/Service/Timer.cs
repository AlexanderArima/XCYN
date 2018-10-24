using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Core.Print.Service
{
    public class Timer
    {
        public void Fun1()
        {
            CancellationTokenSource cancellation = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (!cancellation.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("正在处理后台程序");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        break;
                    }
                }
            }).ContinueWith((obj) => {
                Console.WriteLine("退出程序中...");
                Thread.Sleep(1000);
            });

            while (true)
            {
                Thread.Sleep(5000);
                //读取配置文件isQuit是否退出
                var isQuit = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build()["isQuit"];
                if (isQuit == "1")
                {
                    cancellation.Cancel();
                    break;
                }
            }
        }
    }
}
