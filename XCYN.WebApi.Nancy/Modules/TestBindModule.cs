using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.WebApi.Nancy.Modules
{
    public class TestBindModule : NancyModule
    {
        public class ReceiveObject
        {
            public string pageSize { get; set; }

            public string token { get; set; }

            public string index { get; set; }

            public string[] list { get; set; }

            public Data data { get; set; }

            public class Data
            {
                public string id { get; set; }
            }
        }

        public TestBindModule() : base("/order")
        {
            Get["/"] = p => 
            {
                // 绑定模型
                // ReceiveObject req = this.Bind();
                // var req = this.Bind<ReceiveObject>();

                // 屏蔽指定的参数
                // var req = this.Bind<ReceiveObject>(m => m.pageSize, m => m.index);

                var req = this.Bind<ReceiveObject>("pageSize", "index");
                return "";
            };
        }
    }
}