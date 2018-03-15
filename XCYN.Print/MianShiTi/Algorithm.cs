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

        /// <summary>
        /// 一个富二代给他儿子的四年大学生活存一笔钱，富三代每月只能取3k作为下个月的生活费，采用的是整存零取的方式，
        /// 年利率在1.71%，请问富二代需要一次性存入多少钱。
        /// 思路: 这个题目是我们知道了结果，需要逆推条件， 第48月富三代要连本带息的把3k一把取走，那么
        /// 第47月存款应为： (第48个月的存款+3000)/(1+0.0171/12(月))；
        /// 第46月存款应为： (第47个月的存款+3000)/(1+0.0171/12(月));
        ///  .....                    .....
        /// 第1个月存款应为: (第2个月的存款+3000)/(1+0.0171/12(月));
        /// </summary>
        public static void Fun3()
        {
            double[] money = new double[48];
            money[47] = 3000;
            double rate = 0.0171;
            for (int i = 46; i >= 0; i--)
            {
                money[i] = (money[i + 1] + 3000) / (1 + rate / 12);
            }
            for (int i = 47; i >= 0; i--)
            {
                Console.WriteLine($"第{i+1}个月的合计为:{money[i]}");
            }
            
        }

        /// <summary>
        /// 求阶乘
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Fact(int n)
        {
            if(n == 1)
            {
                return 1;
            }
            return n * Fact(n - 1);
        }

        /// <summary>
        /// 贪心算法（又称贪婪算法）是指，在对问题求解时，总是做出在当前看来是最好的选择。
        /// 也就是说，不从整体最优上加以考虑，他所做出的是在某种意义上的局部最优解。
        /// </summary>
        public static Dictionary<decimal,int> ExChange(decimal num)
        {
            /*
             其实说到贪心，基本上都会提到“背包问题”，这里我就举一个“找零钱的问题“，对的，找零钱问题是我们生活中一个活生生的贪心算法
             的例子，比如我买了“康师傅来一桶方便面”，给了10两银子，方便面3.8两，那么收银mm该找我6.2两，现实中mm不自觉的就会用到贪心的行
             为给我找最少张币，总不能我给mm一张，mm给我十几张，那样mm会心疼的。
             */
            var money = GetInit();
            int i = 0;
            while(true)
            {
                if(num < 0.05M)
                {
                    return money;
                }
                var max = money.Keys.ElementAt(i);//表示100元纸币
                if(num >= max)
                {
                    money[max] = money[max] + 1;
                    num = num - max;
                }
                else
                {
                    if (num >= 0.05M && num < 0.1M)
                    {
                        money[0.1M] = money[0.1M] + 1;
                        num = 0.0M;
                        if (money[0.1M] * 0.1M + money[0.2M] * 0.2M + money[0.5M] * 0.5M == 1.0M)
                        {
                            money[0.1M] = 0;
                            money[0.2M] = 0;
                            money[0.5M] = 0;
                            money[1.0M] += 1;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        static Dictionary<decimal, int> GetInit()
        {
            Dictionary<decimal, int> money = new Dictionary<decimal, int>();

            //key表示钱，value表示钱的张数
            money.Add(100.00M, 0);
            money.Add(50.00M, 0);
            money.Add(20.00M, 0);
            money.Add(10.00M, 0);
            money.Add(5.00M, 0);
            money.Add(2.00M, 0);
            money.Add(1.00M, 0);
            money.Add(0.50M, 0);
            money.Add(0.20M, 0);
            money.Add(0.10M, 0);

            return money;
        }

    }
}
