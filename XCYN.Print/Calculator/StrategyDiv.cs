using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Calculator
{
    public class StrategyDiv : IStrategy
    {
        public decimal Exec(decimal first, decimal second)
        {
            return first / second;
        }
    }
}
