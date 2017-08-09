using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using XCYN.WS.Models;

namespace XCYN.WS
{
    public class Connection102 : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, "Welcome!");
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            //将json解析成对象
            var user = JsonConvert.DeserializeObject<User>(data);
            if(user.action.ToLower().Equals("add"))
            {
                //进入房间
                Groups.Add(connectionId, user.group_name);
                //发送消息，可排除特定用户
                return this.Connection.Broadcast(string.Format("{0} 房间进来了新人,大家欢迎!", user.group_name),connectionId);
            }
            else
            {
                return Groups.Send(user.group_name, string.Format("{0}:{1}",connectionId,user.data), connectionId);
            }
        }
    }
    
}