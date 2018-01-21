using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Command
{
    public class RemoveCommand : ICommand
    {

        Received received = null;

        public RemoveCommand(Received received)
        {
            this.received = received;
        }

        public void execute()
        {
            received.Remove();
        }
    }
}
