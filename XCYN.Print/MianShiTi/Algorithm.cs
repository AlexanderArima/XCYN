using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    public class Algorithm
    {

        #region 经典算法

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
        /// 洗牌问题
        /// </summary>
        public static List<Card> NewCard()
        {
            List<Card> list = new List<Card>();
            string[] num = { "A","2","3","4","5","6","7","8","9","J","Q","K" };
            string[] suit = { "黑桃", "红桃", "梅花" ,"方片"};
            for (int j = 0; j < num.Length; j++)
            {
                for (int i = 0; i < suit.Count(); i++)
                {
                    var card = new Card();
                    card.num = num[j];
                    card.suit = suit[i];
                    list.Add(card);

                }
            }
            return list;
            for (int i = 0; i < list.Count(); i++)
            {
                Console.WriteLine(list[i].ToString());
            }
            Console.Read();
        }

        public static void Shuffle(List<Card> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                //生成随机位置，然后交换
                var random = new Random().Next(0,list.Count - 1);
                Thread.Sleep(1);
                var temp = list[i];
                list[i] = list[random];
                list[random] = temp;

            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].ToString());
            }

            Console.Read();
        }

        #endregion

        #region 递推思想

        /// <summary>
        /// 以迭代的方式实现斐波那契数列，效率比下面的以递归的方式效率快了近100倍
        /// </summary>
        public int Fibonacci(int num)
        {
            //int month = 42;
            int[] fab = new int[num];
            fab[0] = 1;
            fab[1] = 1;
            for (int i = 2; i < num; i++)
            {
                fab[i] = fab[i - 1] + fab[i - 2];
            }
            //for (int i = 0; i < fab.Length; i++)
            //{
                //Console.WriteLine($"第{i + 1}个月的小兔子为:{fab[i]}");
            //}
            return fab[fab.Count() - 1];
        }

        /// <summary>
        /// 以递归的方式计算斐波那契数列
        /// </summary>
        public int FibonacciRecursive(int num)
        {
            if (num <= 2)
            {
                return 1;
            }
            else
            {
                return FibonacciRecursive(num - 1) + FibonacciRecursive(num - 2);
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

        #endregion

        #region 贪心思想

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

        #endregion

        #region 递归思想


        /// <summary>
        /// 求阶乘
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Fact(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            return n * Fact(n - 1);
        }

        /// <summary>
        /// 十进制转二进制
        /// </summary>
        /// <param name="rtn"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Convert10To2(ref string rtn, int num)
        {
            if (num == 0)
                return string.Empty;
            Convert10To2(ref rtn, num / 2);
            return rtn += num % 2;
        }

        /// <summary>
        /// 树的先序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        public void TreeDRT<T>(ChainTree<T> tree)
        {
            if (tree == null)
                return;

            if(tree.data != null)
                Console.WriteLine(tree.data.ToString());
            else
                Console.WriteLine("");

            TreeDRT(tree.left);

            TreeDRT(tree.right);
        }
        
        /// <summary>
        /// 树的中序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        public void TreeLRT<T>(ChainTree<T> tree)
        {
            if (tree == null)
                return;

            TreeLRT(tree.left);

            if (tree.data != null)
                Console.WriteLine(tree.data.ToString());
            else
                Console.WriteLine("");

            TreeLRT(tree.right);
        }

        /// <summary>
        /// 树的后序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        public void TreeLRD<T>(ChainTree<T> tree)
        {
            if (tree == null)
                return;

            TreeLRD(tree.left);

            if (tree.data != null)
                Console.WriteLine(tree.data.ToString());
            else
                Console.WriteLine("");

            TreeLRD(tree.right);
        }

        #endregion

        #region 枚举思想

        /**
         下面是一个填写数字的模板，其中每个字都代表数字中的”0~9“，那么要求我们输入的数字能够满足此模板。
             算 法 洗 脑 题
          X              算
          -----------------
          题 题 题 题 题 题
         */

        /// <summary>
        /// 初始版
        /// </summary>
        public void EnumIdea()
        {
            int count = 0;

            //“算”字的取值范围
            for (int i1 = 1; i1 < 10; i1++)
            {
                //“法”字的取值范围
                for (int i2 = 0; i2 < 10; i2++)
                {
                    //“洗”字的取值范围
                    for (int i3 = 0; i3 < 10; i3++)
                    {
                        //"脑"字的取值范围
                        for (int i4 = 0; i4 < 10; i4++)
                        {
                            //"题"字的取值范围
                            for (int i5 = 1; i5 < 10; i5++)
                            {
                                count++;

                                //一个猜想值
                                var guess = (i1 * 10000 + i2 * 1000 + i3 * 100 + i4 * 10 + i5) * i1;

                                //最终结果值
                                var result = i5 * 100000 + i5 * 10000 + i5 * 1000 + i5 * 100 + i5 * 10 + i5;

                                if (guess == result)
                                {
                                    Console.WriteLine("\n\n不简单啊，费了我  {0}次,才tmd的找出来\n\n", count);

                                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}", i1, i2, i3, i4, i5);
                                    Console.WriteLine("\n\n\tX\t\t\t\t{0}", i1);
                                    Console.WriteLine("—————————————————————————————");
                                    Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}", i5, i5, i5, i5, i5, i5);

                                    Console.Read();
                                }

                                Console.WriteLine("第{0}搜索", count);

                            }
                        }
                    }
                }
            }

            Console.Read();
        }

        /// <summary>
        /// 进阶版
        /// </summary>
        public void EnumIdea2()
        {
            //商
            int[] resultArr = { 111111, 222222, 333333, 444444, 555555, 666666, 777777, 888888, 999999 };

            //除数
            int[] numArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int count = 0;

            for (int i = 0; i < resultArr.Count(); i++)
            {
                for (int j = 0; j < numArr.Count(); j++)
                {
                    count++;

                    var result = resultArr[i].ToString();

                    var num = numArr[j].ToString();

                    var origin = (resultArr[i] / numArr[j]).ToString();

                    if (origin.LastOrDefault() == result.FirstOrDefault()
                        && origin.FirstOrDefault() == num.FirstOrDefault()
                        && result.Length - 1 == origin.Length)
                    {
                        Console.WriteLine("\n\n费了{0} 次，tmd找出来了", count);
                        Console.WriteLine("\n\n感谢一楼同学的回答。现在的时间复杂度已经降低到O(n2)，相比之前方案已经是秒杀级别\n");

                        Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}", origin.ElementAt(0), origin.ElementAt(1), origin.ElementAt(2), origin.ElementAt(3), origin.ElementAt(4));
                        Console.WriteLine("\n\n\tX\t\t\t\t{0}", num);
                        Console.WriteLine("—————————————————————————————");
                        Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}", result.ElementAt(0), result.ElementAt(0), result.ElementAt(0), result.ElementAt(0), result.ElementAt(0), result.ElementAt(0));

                        Console.Read();
                    }
                    Console.WriteLine("第{0}搜索", count);
                }
            }
            Console.WriteLine("无解");
            Console.Read();
        }

        #endregion

        public void ReverseArray()
        {
            List<int> list = new List<int>();
            list.AddRange(new int[6] { 1, 2, 3, 4, 5,6 } );
            Stack<int> list_new = new Stack<int>();
            for (int i = 0; i < list.Count; i = i + 2)
            {
                int one = list[i];
                if(i + 1 < list.Count)
                {
                    int two = list[i + 1];
                    list_new.Push(two);
                }
                list_new.Push(one);
            }

            Console.WriteLine("反转前的结果：");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list.ElementAt(i));
            }

            Console.WriteLine("反转后的结果：");
            for (int i = 0; i < list_new.Count; i++)
            {
                Console.WriteLine(list_new.ElementAt(i));
            }
        }
    }
}
