using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    public class Card
    {
        public string suit { get; set; }

        public string num { get; set; }

        public override string ToString()
        {
            return suit + " " + num;
        }
    }
}
