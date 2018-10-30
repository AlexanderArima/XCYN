using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Adapter.Object
{
    public class PowerAdapter:ThreeHole
    {

        TwoHole twohole = new TwoHole();
        
        /// <summary>
        /// 实现三项插头的逻辑
        /// </summary>
        public override void GetLargePower()
        {
            twohole.GetPower();
            Console.WriteLine("接入三项插头");
        }
    }
}
