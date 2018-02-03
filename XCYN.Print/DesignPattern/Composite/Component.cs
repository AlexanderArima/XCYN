using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Composite
{
    public abstract class Component
    {
        protected List<Component> _list = new List<Component>();

        public string name { get; set; }

        public abstract void Add(Component component);

        public abstract void Remove(Component component);

        public abstract void Display(int deep);
    }
}
