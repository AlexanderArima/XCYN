using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Calculator
{
    public class StrategyAdd : IStrategy
    {
        public decimal Exec(decimal first, decimal second)
        {
            return first + second;
        }
    }
}
