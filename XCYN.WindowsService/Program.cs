﻿using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.WindowsService
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //注册Log4Net
            XmlConfigurator.Configure();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new UseQuartzService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
