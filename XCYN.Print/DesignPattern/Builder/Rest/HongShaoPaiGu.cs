using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    /// <summary>
    /// 建造者具体实现
    /// </summary>
    public class HongShaoPaiGu : Cooking
    {
        
        public override string name()
        {
            return "红烧排骨";
        }

        public override float price()
        {
            return 48;
        }
    }
}
