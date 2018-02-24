using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Operators
{
    public class NullOperators
    {

        /// <summary>
        /// ?? 空合并运算符，提供了一种快捷的方式，可以在处理可空类型和引用类型时表示null的可能性。
        /// 这个运算符放在两个操作数中间，如果第一个操作数不为Null，则返回第一个表达式，
        /// 如果为Null，则返回第二个表达式
        /// </summary>
        public void Fun1()
        {
            //值类型
            int? a = 1;
            int? b = null;
            Console.WriteLine($"a ?? 0:{a ?? 0}");
            Console.WriteLine($"b ?? 0:{b ?? 0}");
        }

        public void Fun2()
        {
            //引用类型
            string str = "";
            string str2 = null;
            Console.WriteLine($"str ?? \"the string is null\" : {str ?? "the string is null"}");
            Console.WriteLine($"str2 ?? \"the string is null\" : {str2 ?? "the string is null"}");
        }
        
        /// <summary>
        /// ? 空值传播运算符，如果person为空，则不会调用它的Name值，从而抛出异常，而是返回null
        /// </summary>
        public void Fun3(Person person)
        {
            string name = person?.Name;
            Console.WriteLine($"name:{name}");
        }
        
        public void Fun4(Person person)
        {
            //处理值类型时，可以配合??运算符
            int num = person?.Age ?? 0;
            //相当于
            //int num2 = 0;
            //if(person == null)
            //{
            //    num2 = 0;
            //}
            //else
            //{
            //    num2 = person.Age;
            //}
            Console.WriteLine($"num:{num}");

        }

    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

}
