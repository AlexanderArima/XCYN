using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace XCYN.Core.Print
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username","Cheng")
            };
            //加载程序中的配置项除了必须要添加的Microsoft.Extensions.Configuration，不需要其他的依赖项
            //加载Json文件需要添加Microsoft.Extensions.Configuration.Json这个依赖项
            //加载Xml文件需要添加Microsoft.Extensions.Configuration.Xml这个依赖项
            //加载环境变量需要添加Microsoft.Extensions.Configuration.EnvironmentVariables这个依赖项
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                                             //.AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                                             //.AddXmlFile("appsetting.xml",optional:true,reloadOnChange:true)
                                             //.AddEnvironmentVariables("Path")
                                             .AddInMemoryCollection(list)
                                             .Build();

            //相同的键，只取最后一个
            while (true)
            {
                var info = configuration["username"];

                Console.WriteLine(info);

                System.Threading.Thread.Sleep(1000);
            }

        }
    }
}
