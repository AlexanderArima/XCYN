using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Prototype.Deep
{
    [Serializable]
    public class Person : IPrototype
    {

        public string name { get; set; }

        public int age { get; set; }

        public Address address { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
