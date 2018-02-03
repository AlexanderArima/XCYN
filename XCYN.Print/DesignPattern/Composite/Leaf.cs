using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Composite
{
    /// <summary>
    /// 叶子节点
    /// </summary>
    public class Leaf : Component
    {
        public override void Add(Component component)
        {
            
        }

        public override void Display(int deep)
        {
            Console.WriteLine(new string('-', deep) + name);
        }

        public override void Remove(Component component)
        {

        }
    }
}
