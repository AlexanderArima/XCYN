using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Flyweight
{
    public class Factory
    {
        static Dictionary<int, FlyWeight> cache = new Dictionary<int, FlyWeight>();

        public static FlyWeight GetInstance(int num)
        {
            if(cache.Keys.Contains(num))
            {
                return cache[num];
            }
            else
            {
                cache[num] = new ConcreteFlyWeight();
                return cache[num];
            }
        }
    }
}
