using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Adapter.Class
{
    public class PowerAdapter : ITwoHole, IThreeHole
    {
        public void GetLargePower()
        {
            Console.WriteLine("获取三项插头的电");
        }

        public void GetPower()
        {
            GetLargePower();
        }
    }
}
