using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Calculator
{
    public interface IStrategy
    {
        decimal Exec(decimal first, decimal second);
    }
}
