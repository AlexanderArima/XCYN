﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XCYN.Print.DesignPattern.Proxy
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“DataService”。
    public class DataService : IDataService
    {
        public string GetName(int id)
        {
            //连接数据库，并查询
            return "hello world";
        }
    }
}
