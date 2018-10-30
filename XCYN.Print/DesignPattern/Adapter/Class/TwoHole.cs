using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Adapter.Class
{
    public class TwoHole : ITwoHole
    {
        public void GetPower()
        {
            Console.WriteLine("获取双向插头的电");
        }
    }
}
