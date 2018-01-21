using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Command
{
    public class Received
    {
        public void Add()
        {
            Console.WriteLine("调用Add方法");
        }

        public void Remove()
        {
            Console.WriteLine("调用Remove方法");
        }
    }
}
