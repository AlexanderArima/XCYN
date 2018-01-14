using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Mediator
{
    public class QQMediator:AbstractMediator
    {

        private List<AbstractColleague> list = new List<AbstractColleague>();

        /// <summary>
        /// 向name发送msg
        /// </summary>
        /// <param name="name">接收人</param>
        /// <param name="msg">消息</param>
        public override void Send(string name,string msg)
        {
            var receiver = list.FirstOrDefault(m => m.UserName.Equals(name));
            if(receiver != null)
                receiver.Receive(string.Format("{0}:{1}",name,msg));
        }

        /// <summary>
        /// 添加接收人
        /// </summary>
        /// <param name="colleague"></param>
        public override void Add(AbstractColleague colleague)
        {
            if(!list.Contains(colleague))
                list.Add(colleague);
        }
    }
}
