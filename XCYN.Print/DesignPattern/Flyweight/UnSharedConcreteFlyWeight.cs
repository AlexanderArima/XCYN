using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Flyweight
{
    public class UnSharedConcreteFlyWeight : FlyWeight
    {
        public override void run(string msg)
        {
            Console.WriteLine("不共享的:{0}",msg);
        }
    }
}
