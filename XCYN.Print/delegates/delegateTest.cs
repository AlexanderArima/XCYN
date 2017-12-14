using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.delegates
{
    public class delegateTest
    {
        delegate void MyAction(string str);

        delegate int MyAction2(string str);

        public static void SayHello(string str)
        {
            Console.WriteLine(str);
        }

        public static void DelegateFun()
        {
            goto myCase;
            //调用委托
            MyAction ac1 = SayHello;
            ac1("ac1");

            //匿名委托
            MyAction2 ac2 = delegate (string str)
            {
                Console.WriteLine(str);
                return 1;
            };
            ac2("ac2");

            //使用lambda表达式 
            MyAction2 ac3 = str => 1;
            Console.WriteLine(ac3("ac3"));

            // 使用泛型委托
            // Action是无返回值的泛型委托。它也是用：public delegate void Action<in T>(T obj);声明得到的
            // Action至少0个参数，至多16个参数，无返回值。
            Action<int, int> ac4 = (str, str2) => Console.WriteLine(str + str2);
            ac4(1, 2);

            // Func是有返回值的泛型委托。它是用：public delegate TResult Func<in T, out TResult>(T arg);声明得到的
            // Func至少0个参数，至多16个参数，根据返回值泛型返回。必须有返回值，不可void
            Func<int, int, bool> ac5 = (str, str2) =>
            {
                if (str > str2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            Console.WriteLine(ac5(5, 3));

            myCase:
            Predicate<int> ac6 = (str) =>
            {
                return str % 2 == 0 ? true : false;
            };
            Console.WriteLine(ac6(0));

        }

    }
}
