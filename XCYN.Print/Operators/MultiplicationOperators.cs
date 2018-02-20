using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Operators
{
    public class MultiplicationOperators
    {
        /// <summary>
        /// * 乘法运算
        /// </summary>
        public void Fun1()
        {
            Console.WriteLine(2 * 2);
            Console.Read();
        }

        /// <summary>
        ///  * 运算符还用于声明指针类型和取消引用指针。
        ///  此运算符仅可用于不安全的上下文，通过使用 unsafe 关键字表示，且需要 /unsafe 编译器选项。
        ///  取消引用运算符也称为间接寻址运算符。
        /// </summary>
        public unsafe void Fun2()
        {
            int i = 5;
            int* j = &i;
            *j = *j * 5;
            i = i * 2;
            Console.WriteLine("j:"+*j);
            Console.WriteLine("i:"+i);
            Console.Read();
        }

        /// <summary>
        /// 除法操作符 / 和取模操作符 %
        /// </summary>
        public void Fun3()
        {
            Console.WriteLine("5除以2等于{0}，余数为{1}",5 / 2,5 % 2);
            Console.WriteLine(5 / 2f);//除以浮点数可以得到小数
            Console.Read();
        }
    }
}
