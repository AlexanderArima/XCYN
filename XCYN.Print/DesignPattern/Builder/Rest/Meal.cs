using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    /// <summary>
    /// 监工：定义使用建造者角色中的方法来生成实例的方法；
    /// </summary>
    public class Meal
    {
        private List<Item> list = new List<Item>();

        public void Add(Item item)
        {
            list.Add(item);
        }

        public void Show()
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.name() + "   单价：" +item.price());
            }
        }

        
    }
}
