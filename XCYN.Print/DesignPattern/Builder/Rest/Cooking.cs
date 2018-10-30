using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    /// <summary>
    ///  建造者部分实现
    /// </summary>
    public abstract class Cooking : Item
    {
        public virtual float discount {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
            }
        }

        public string type {
            get
            {
                return "炒菜";
            }
            set
            {
                type = value;
            }
        }

        public abstract string name();
        public abstract float price();
    }
}
