using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Composite
{
    /// <summary>
    /// 树枝节点
    /// </summary>
    public class Composite : Component
    {

        public Composite(string name)
        {
            this.name = name;
        }

        public override void Add(Component component)
        {
            _list.Add(component);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="deep"></param>
        public override void Display(int deep)
        {
            Console.WriteLine(new string('-',deep) + name);
            foreach (var item in _list)
            {
                //让子节点突出显示
                item.Display(deep + 2);
            }
        }

        public override void Remove(Component component)
        {
            _list.Remove(component);
        }
    }
}
