using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.State
{
    public abstract class AbstractState
    {
        public abstract void Handle(Context context);
    }
}
