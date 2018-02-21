using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Operators
{
    public class RelationOperators
    {
        /// <summary>
        /// < 运算符
        /// </summary>
        public void Fun1()
        {
            Console.WriteLine(1 < 2);
            Console.WriteLine(1 < 1);
            Console.Read();
        }

        /// <summary>
        /// > 运算符
        /// </summary>
        public void Fun2()
        {
            Console.WriteLine(1 > 2);
            Console.WriteLine(1 > 1);
        }
        
        /// <summary>
        /// <= 运算符
        /// </summary>
        public void Fun3()
        {
            Console.WriteLine(1 <= 2);
            Console.WriteLine(1 <= 1);
            Console.Read();
        }
    }
}
