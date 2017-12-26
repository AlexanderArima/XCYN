using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Prototype.Shallow
{
    public class Address
    {

        public string province { get; set; }

        public string city { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
