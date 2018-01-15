using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Calculator
{
    public class Calculator
    {

        private IStrategy _strategy;

        //切换策略
        public Calculator(IStrategy strategy)
        {
            _strategy = strategy;
        }

        //执行策略方法
        public decimal Exec(decimal first,decimal second)
        {
            return _strategy.Exec(first, second);
        }
    }
}
