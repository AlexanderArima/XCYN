using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    public abstract class AbstractPerson
    {
        public abstract string CreateHead();

        public abstract string CreateBody();

        public abstract string CreateHand();

        public abstract string CreateFoot();
        
    }
}
