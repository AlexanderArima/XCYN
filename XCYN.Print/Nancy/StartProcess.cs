using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Nancy
{
    internal class StartProcess
    {
        public static void Fun01()
        {
            using (NancyHost host = new NancyHost(new Uri("http://localhost:8888/")))
            {
                host.Start();
                Console.WriteLine("NancyHost已启动");
                try
                {
                    Console.WriteLine("正在启动http://localhost:8888/");
                    System.Diagnostics.Process.Start("http://localhost:8888/");
                    Console.WriteLine("成功启动http://localhost:8888/");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出现异常：" + ex.Message);
                }

                Console.ReadKey();
            }

            Console.WriteLine("NancyHost已停止");
        }
    }
}
