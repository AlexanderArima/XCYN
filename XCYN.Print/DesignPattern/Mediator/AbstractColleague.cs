using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Mediator
{
    public abstract class AbstractColleague
    {
        public string UserName { get; set; }

        public abstract void Send(string name, string msg);

        public abstract void Receive(string msg);
    }
}
