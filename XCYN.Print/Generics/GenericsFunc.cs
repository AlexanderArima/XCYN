using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Generics
{
    public class GenericsFunc
    {
        public void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }
    }

    public class Account
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Account(string name,decimal balance)
        {
            Name = name;
            Balance = balance;
        }
    }

}
