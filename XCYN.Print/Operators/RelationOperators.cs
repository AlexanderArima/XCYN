using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Operators
{
    /// <summary>
    /// 关系运算符
    /// </summary>
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

        /// <summary>
        /// >= 运算符
        /// </summary>
        public void Fun4()
        {
            Console.WriteLine(1 >= 2);
            Console.WriteLine(1 >= 1);
            Console.Read();
        }

        /// <summary>
        /// is 运算符可以检查对象是否与特定的类型兼容。“兼容”指的是对象或者类型，或者父类型 
        /// </summary>
        public void Fun5()
        {
            Console.WriteLine(1 is 1);
            Console.WriteLine(1 is Int32);
            Console.WriteLine(1 is object);
            /*
             true
             true
             true
             */
            Console.Read();
        }

        /// <summary>
        /// as 运算符用于执行引用类型的显示类型转换，如果转换的类型与指定的类型兼容，转换就会成功；
        /// 如果不兼容，as运算符就会返回null
        /// </summary>
        public void Fun6()
        {
            object s = "1";
            object s2 = 1;
            Console.WriteLine(s as String);
            Console.WriteLine(s2 as String);
            Console.Read();
        }
    }
}
