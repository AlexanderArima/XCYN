using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XCYN.Core.Print.DI;

namespace XCYN.Core.Print
{
    class Program
    {
        static void Main(string[] args)
        {
            HandlerFun fun = new HandlerFun();
            fun.Fun3();
            Console.Read();
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        static void LoadConfig()
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
            var info = configuration["username"];
            Console.WriteLine(info);
        }

        static void LoadArray()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build();
            //加载数组，通过:下标来取出指定下标的元素
            var ip = configuration["ipList:0"];
            Console.WriteLine(ip);
        }

        /// <summary>
        /// 将JSON字符串转成对象(需要加载Microsoft.Extensions.Configuration.Binder类库)
        /// </summary>
        static void LoadObject()
        {
            IConfiguration conf = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build();
            var obj = conf.Get<Rootobject>();
            Console.WriteLine(obj.username);
        }

        /// <summary>
        /// 加载ini配置文件
        /// </summary>
        static void LoadIni()
        {
            IConfiguration conf = new ConfigurationBuilder().AddIniFile("appsetting.ini").Build();
            var obj = conf["owner:name"];
            Console.WriteLine(obj);
        }
    }



    public class Rootobject
    {
        public string username { get; set; }
        public Mysql mysql { get; set; }
        public string[] ipList { get; set; }
    }

    public class Mysql
    {
        public string host { get; set; }
        public int port { get; set; }
    }


}
