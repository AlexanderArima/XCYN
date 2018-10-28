using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    /// <summary>
    /// 建造者角色
    /// </summary>
    public interface Item
    {
        string name();
        string type { get; set; }
        float discount { get; set; }
        float price();
    }
}
