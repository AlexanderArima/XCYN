using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.ChainOfResponsibility
{
    public class ConcreteHander2 : AbstractHandler
    {
        public override void Request(int state)
        {
            if(state > 3 && state <= 7)
            {
                Console.WriteLine("请假天数为{0},部门经理批准",state);
            }
            else
            {
                this.handler.Request(state);
            }
        }
    }
}
