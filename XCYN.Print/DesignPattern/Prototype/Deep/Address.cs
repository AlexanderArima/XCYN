using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Prototype.Deep
{
    [Serializable]
    public class Address:IPrototype
    {

        public string province { get; set; }

        public string city { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
