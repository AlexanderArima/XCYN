using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    public class Algorithm
    {
        /// <summary>
        /// 求素数
        /// </summary>
        /// <param name="text"></param>
        public static void Fun1(string text)
        {
            int j;
            //开方
            j = (int)Math.Ceiling(Math.Sqrt(Convert.ToDouble(text)));
            for (int i = 1; i < j; i++)
            {
                //取整
                if(Math.IEEERemainder(Convert.ToDouble(text),i) == 0)
                {
                    Console.WriteLine("不是素数");
                }
                else
                {
                    Console.WriteLine("是素数");
                }
            }
        }

        /// <summary>
        /// 百钱百鸡算法(用100元钱买100只鸡，其中公鸡5元一只，母鸡3元一只，小鸡3只1元)
        /// Math.DivRem(int a, int b, out int result);返回a/b的余数，result接收余数
        /// </summary>
        public static void Fun2()
        {
            int a = 0, b = 0, c = 0, p = 0;
            for (a = 1; a <= 19; a++)
            {
                for (b = 1; b <= 33; b++)
                {
                    c = 100 - a - b;
                    Math.DivRem(c, 3, out p);
                    if(((5 * a + 3 * b + c / 3) == 100) && p == 0)
                    {
                        Console.WriteLine($"a:{a}");
                        Console.WriteLine($"b:{b}");
                        Console.WriteLine($"c:{c}");
                        return;
                    }
                }
            }
        }
        
        /// <summary>
        /// 斐波那契数列
        /// </summary>
        public static void Fibonacci()
        {
            int month = 12;
            int[] fab = new int[month];
            fab[0] = 1;
            fab[1] = 1;
            for (int i = 2; i < month; i++)
            {
                fab[i] = fab[i - 1] + fab[i - 2];
            }
            for (int i = 0; i < fab.Length; i++)
            {
                Console.WriteLine($"第{i + 1}个月的小兔子为:{fab[i]}");
            }
        }


    }
}
