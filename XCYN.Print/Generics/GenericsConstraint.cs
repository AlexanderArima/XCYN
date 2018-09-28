using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Generics
{
    /// <summary>
    /// 范型约束
    /// </summary>
    public class GenericsConstraint
    {
        public void Fun1()
        {
            //在定义了where T : struct 之后无法通过编译
            Person<string> p = new Person<string>();
            p.Add("hello");

            //在定了了where T : class 之后无法通过编译
            //Person<int> p2 = new Person<int>();
            //p2.Add(123);

        }

        /// <summary>
        /// 范型可以对，它的范围进行约束，例如：可以规定只能是值类型或者引用类型或实现了某个接口的引用类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class Person<T> where T : ICloneable
        {
            public List<T> list = new List<T>();

            public void Add(T t)
            {
                list.Add(t);
            }
        }
    }

    
}
