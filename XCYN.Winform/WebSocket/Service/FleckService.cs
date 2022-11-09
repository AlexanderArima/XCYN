using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace XCYN.Winform.WebSocket.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“FleckService”。
    [JavascriptCallbackBehavior(UrlParameterName = "jsonpCallback")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FleckService : IFleckService
    {
        public CreateUserName_Receive CreateUserName()
        {
            var obj = OnlineUser.Create();
            if(obj == null)
            {
                return new CreateUserName_Receive()
                {
                    code = 999999,
                    data = null,
                    msg = "数据异常",
                };
            }
            else
            {
                return new CreateUserName_Receive()
                {
                    code = 0,
                    data = new CreateUserName_Receive.Data()
                    {
                        name = obj.Name,
                        id = obj.ID
                    },
                    msg = "success",
                };
            }
        }
    }

    public class CreateUserName_Receive
    {
        public int code { get; set; }

        public string msg { get; set; }

        public Data data { get; set; }

        public class Data
        {
            public string name { get; set; }

            public string id { get; set; }
        }
    }
}
