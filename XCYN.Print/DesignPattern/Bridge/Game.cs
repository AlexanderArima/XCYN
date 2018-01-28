using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Bridge
{
    public class Game : Soft
    {
        public override void run()
        {
            Console.WriteLine("运行游戏");
        }
    }
}
