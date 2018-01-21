using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.ChainOfResponsibility
{
    public abstract class AbstractHandler
    {

        protected AbstractHandler handler = null;

        public void SetHandler(AbstractHandler handler)
        {
            this.handler = handler;
        }

        public abstract void Request(int state);
        
    }
}
