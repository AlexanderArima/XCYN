using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.State
{
    public class EveningState : AbstractState
    {
        public override void Handle(Context context)
        {
            //最后一个条件，就无需判断了
            Console.WriteLine("晚上好");
        }
    }
}
