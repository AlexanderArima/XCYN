using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.delegates
{
    /// <summary>
    /// lambda表达式，从C#3.0开始，就有一种新的方法实现委托的赋予：lambda表达式。
    /// 只要有委托参数类型的地方，就可以使用lambda表达式
    /// </summary>
    public class LambdaTest
    {
        public void Fun1()
        {
            Action<int> action = (num) => 
            {
                Console.WriteLine($"action:{num * num}");
            };
            action.Invoke(100);

            Func<int, int> func = (num) =>
             {
                 return num * num;
             };
            Console.WriteLine($"func:{func(100)}");
        }
    }
}
