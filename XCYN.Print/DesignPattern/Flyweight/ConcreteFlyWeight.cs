using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Flyweight
{
    public class ConcreteFlyWeight : FlyWeight
    {
        public override void run(string msg)
        {
            Console.WriteLine("共享的:{0}",msg);
        }
    }
}
