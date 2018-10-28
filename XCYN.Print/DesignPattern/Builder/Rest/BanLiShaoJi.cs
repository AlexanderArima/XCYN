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
    public class BanLiShaoJi : Cooking
    {
        public override string name()
        {
            return "板栗烧鸡";
        }

        public override float price()
        {
            return 38;
        }
    }
}
