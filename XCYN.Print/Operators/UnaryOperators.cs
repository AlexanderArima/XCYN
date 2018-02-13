using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Operators
{
    /// <summary>
    /// 一元运算符
    /// </summary>
    public class UnaryOperators
    {
        /// <summary>
        /// +运算符
        /// </summary>
        public void Fun1()
        {
            /*
             为所有数值类型预定义了一元 + 运算符。 对数值类型进行一元 + 运算的结果就是操作数的值。
             运算符既可作为一元运算符也可作为二元运算符。为数值类型和字符串类型预定义了二元 + 运算符。
             对于数值类型，+ 计算两个操作数之和。 
             当其中的一个操作数是字符串类型或两个操作数都是字符串类型时，+ 将操作数的字符串表示形式串联在一起。
             */

            Console.WriteLine(+5);        // unary plus
            Console.WriteLine(5 + 5);     // addition
            Console.WriteLine(5 + .5);    // addition
            Console.WriteLine("5" + "5"); // string concatenation
            Console.WriteLine(5.0 + "5"); // string concatenation
            Console.Read();
            /*
            Output:
            5
            10
            5.5
            55
            55
            */
        }

        /// <summary>
        /// -运算符
        /// </summary>
        public void Fun2()
        {
            /*
             为所有数值类型预定义了一元 - 运算符。 对数值类型进行一元 - 运算的结果是操作数的数值求反运算。
             针对所有数值和枚举类型预定义二元 - 运算符，从第一个操作数中减去第一个操作数。
             委托类型也提供二元 - 运算符，该运算符执行委托移除。
             */
            int a = 5;
            Console.WriteLine(-a);
            Console.WriteLine(a - 1);
            Console.WriteLine(a - .5);
            Console.Read();
            /*
            Output:
            -5
            4
            4.5
            */
        }

        /// <summary>
        /// ~运算符
        /// </summary>
        public void Fun3()
        {
            /*
             运算符对其操作数执行按位求补运算，这对反转每一个位都有影响。 按位求补运算符针对
             */
            int[] values = { 0, 0x111, 0xfffff, 0x8888, 0x22000022 };
            foreach (int v in values)
            {
                Console.WriteLine("~0x{0:x8} = 0x{1:x8}", v, ~v);
            }
            Console.Read();
            /*
            Output:
            ~0x00000000 = 0xffffffff
            ~0x00000111 = 0xfffffeee
            ~0x000fffff = 0xfff00000
            ~0x00008888 = 0xffff7777
            ~0x22000022 = 0xddffffdd
            */
        }

        /// <summary>
        /// ()运算符
        /// </summary>
        public void Fun4()
        {
            //除了用于指定表达式中的运算顺序外，圆括号还用于执行以下任务：
            //指定强制转换或类型转换。
            double x = 1234.7;
            int a;
            a = (int)x; // Cast double to int
            Console.WriteLine("a:"+a);
            //调用方法或委托。
            Console.WriteLine("Hello World");
            Console.Read();
        }

        /// <summary>
        /// await关键字
        /// </summary>
        public void Fun5()
        {
            /*
             await运算符应用于异步方法中的任务，在方法的执行中插入挂起点，
             直到所等待的任务完成。 任务表示正在进行的工作
             await仅可用于由 async 关键字修饰的异步方法中。 
             使用 async 修饰符定义并且通常包含一个或多个 await 表达式的这类方法称为异步方法。
             */
            Console.WriteLine("请输入访问地址");
            string args = Console.ReadLine();
            if (!string.IsNullOrEmpty(args))
                GetPageSizeAsync(args).Wait();
            else
                Console.WriteLine("Enter at least one URL on the command line.");
            Console.Read();
        }

        private static async Task GetPageSizeAsync(string url)
        {
            var client = new HttpClient();
            var uri = new Uri(Uri.EscapeUriString(url));
            byte[] urlContents = await client.GetByteArrayAsync(uri);
            Console.WriteLine($"{url}: {urlContents.Length / 2:N0} characters");
        }

        /// <summary>
        /// &运算符
        /// </summary>
        public void Fun6()
        {
            /*
             一元 & 运算符返回其操作数的地址
             为整型类型和 bool 预定义了二元 & 运算符。对于整型类型，& 计算其操作数的逻辑按位 AND。 
             对于 bool 操作数，& 计算其操作数的逻辑 AND;即,当且仅当其两个操作数皆为 true 时，结果才为 true。
             */
            Console.WriteLine(true & true);//true
            Console.WriteLine(false & true);//false
            Console.WriteLine(false & false);//false
            //    1111 1000
            //    0011 1111
            //    ---------
            //    0011 1000 or 38
            Console.WriteLine("0x{0:x}", 0xf8 & 0x3f);
            Console.Read();
        }
    }
}
