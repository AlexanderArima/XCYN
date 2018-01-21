using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Command
{
    public class AddCommand : ICommand
    {
        Received received = null;

        public AddCommand(Received received)
        {
            this.received = received;
        }

        public void execute()
        {
            received.Add();
        }
    }
}
