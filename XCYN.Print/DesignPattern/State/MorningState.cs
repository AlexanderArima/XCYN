using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.State
{
    public class MorningState : AbstractState
    {
        public override void Handle(Context context)
        {
            
            if(context.hour > 0 && context.hour < 12)
            {
                Console.WriteLine("早上好");
            }
            else
            {
                //如果不符合条件则调用下一个类来判断
                context.state = new AfternoonState();
                context.request();
            }
        }
    }
}
