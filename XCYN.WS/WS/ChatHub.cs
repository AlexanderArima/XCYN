using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections;
using System.Threading.Tasks;
using XCYN.WS.Models;

namespace XCYN.WS
{
    public class ChatHub : Hub
    {

        public static HashSet<User> _set = new HashSet<User>();
        
        public override Task OnDisconnected(bool stopCalled)
        {
            _set.RemoveWhere((i) => i.ConnectionID.Equals(this.Context.ConnectionId));
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnConnected()
        {
            User user = new User() {
                ConnectionID = this.Context.ConnectionId,
                RandomName = XCYN.Common.GetRandomName.getRandomName(),
            };
            _set.Add(user);
            return base.OnConnected();
        }

        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="room_id"></param>
        public void EnterRoom(string room_id)
        {
            //加入房间
            Groups.Add(this.Context.ConnectionId, room_id);
            //向进入房间的人发送消息
            //Clients.Client(this.Context.ConnectionId).hello("恭喜你，成功进入房间id:"+room_id);
            //向房间中所有人发送消息
            var name = _set.Where((i) => i.ConnectionID.Equals(this.Context.ConnectionId)).Select((i)=>i.RandomName);
            Clients.Group(room_id).groupNotice(name.FirstOrDefault() + " 进入房间"+room_id);
            Clients.Client(this.Context.ConnectionId).notice("您已经进入了房间" + room_id);
        }

        public void Chat(string room_id,string msg)
        {
            var name = _set.Where((i) => i.ConnectionID.Equals(this.Context.ConnectionId)).Select((i) => i.RandomName);
            Clients.Group(room_id).groupChat(string.Format(@"<div class='div_user'><span>{0}：</span><span>{1}</span></div>", name.FirstOrDefault(), msg));
        }

        public void allChat()
        {
            Clients.Client(this.Context.ConnectionId).allChat("你好啊");
        }


        //public void Hello()
        //{
        //All表示给所有人发送消息
        //Clients.All.hello("欢迎你:" + this.Context.ConnectionId);
        //AllExcept表示给除了一部分人发送消息
        //Clients.AllExcept(this.Context.ConnectionId).hello("欢迎你:"+ this.Context.ConnectionId);
        //Client表示给特定的人发送消息
        //Clients.Client(this.Context.ConnectionId).hello("欢迎你:" + this.Context.ConnectionId);

        //var list = new List<string>();
        //list.Add(this.Context.ConnectionId);
        ////Clients给特定的一组人发送消息
        //Clients.Clients(list).hello("欢迎你:" + this.Context.ConnectionId);

        //Clients.Group("1").hello("北京时间:"+DateTime.Now);

        //针对验证通过的用户
        //Clients.User(this.Context.ConnectionId);
        //Clients.Users(list);

        //}

    }
}