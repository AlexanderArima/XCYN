using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.delegates
{
    /// <summary>
    /// 多播委托
    /// 委托也可以包含多个方法，如果使用多播委托就可以按循序调用多个方法，为此多播委托的返回值必须为void
    /// </summary>
    public class MulticastDelegateTest
    {

        public void Fun1()
        {

            Action myDelegate = MathOperations.MultiplyTwo;
            Action action = MathOperations.Square;
            Action action2 = myDelegate + action;
            //为了避免某个方法中抛出异常导致后续方法调用中断，使用Delegate类的GetInvocationlist方法
            //按顺序一次调用数组，就能保证调用的完整性了。
            var list = action2.GetInvocationList();
            
            foreach (var item in list)
            {
                try
                {
                    var a = (Action)item;
                    a.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            Console.Read();
        }

    }

    internal delegate void MyDelegate(double num);

    internal class MathOperations
    {

        public static double num = 2;

        public static void MultiplyTwo()
        {
            num *= 2;
            Console.WriteLine(num);
            throw new OverflowException("参数溢出");
        }

        public static void Square()
        {
            num = num * num;
            Console.WriteLine(num);
        }
    }
}
