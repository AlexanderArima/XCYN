using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.DesignPattern.Mediator;

namespace XCYN.Print.DesignPattern.Mediator
{
    public class Colleague : AbstractColleague
    {
        private AbstractMediator _mediator = null;

        public Colleague(AbstractMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 向中介者传递接收人和消息
        /// </summary>
        /// <param name="name">接收人</param>
        /// <param name="msg">消息</param>
        public override void Send(string name, string msg)
        {
            _mediator.Send(name, msg);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msg"></param>
        public override void Receive(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
