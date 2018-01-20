using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.State
{
    public class AfternoonState : AbstractState
    {
        public override void Handle(Context context)
        {
            if (context.hour >= 12 & context.hour < 18)
            {
                Console.WriteLine("中午好");
            }
            else
            {
                context.state = new EveningState();
                context.request();
            }
        }
    }
}
