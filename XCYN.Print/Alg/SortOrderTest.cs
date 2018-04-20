using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.Alg
{
    /// <summary>
    /// 用于比较各个排序方法的效率
    /// </summary>
    public class SortOrderTest
    {
        /// <summary>
        /// 冒泡排序 vs 快排
        /// </summary>
        public static void BubbleVsQuickSort()
        {
            for (int i = 0; i < 5; i++)
            {
                List<int> list = new List<int>();
                //随机生成2000个数字类型的数组
                for (int j = 0; j < 2000; j++)
                {
                    Thread.Sleep(1);//休眠一毫秒生成随机地数字
                    var seek = DateTime.Now.Ticks.ToString();
                    int number = new Random(Convert.ToInt32(seek.Substring(seek.Length - 7,7))).Next(0, 100000);
                    list.Add(number);
                }
                Console.WriteLine($"第{i + 1}次比较");
                Stopwatch watch = new Stopwatch();
                watch.Start();
                list = list.OrderBy(m => m).ToList();
                watch.Stop();
                Console.WriteLine($"快速排序耗费{watch.ElapsedMilliseconds}ms...");
                Console.WriteLine($"最小的前十个数是:{ string.Join(",",list.Take(10)) }...");

                watch.Start();
                list = SortOrder.BubbleSort(list);
                watch.Stop();
                Console.WriteLine($"冒泡排序耗费{watch.ElapsedMilliseconds}ms...");
                Console.WriteLine($"最小的前十个数是:{ string.Join(",", list.Take(10)) }...");

            }
        }

        /// <summary>
        /// 快排 vs 快排
        /// </summary>
        public static void QuickSortVsQuickSort()
        {
            //5次比较
            for (int i = 1; i <= 5; i++)
            {
                List<int> list = new List<int>();

                //插入200个随机数到数组中
                for (int j = 0; j < 2000; j++)
                {
                    Thread.Sleep(1);//休眠一毫秒生成随机地数字
                    var seek = DateTime.Now.Ticks.ToString();
                    int number = new Random(Convert.ToInt32(seek.Substring(seek.Length - 7, 7))).Next(0, 100000);
                    list.Add(number);
                }

                Console.WriteLine("\n第" + i + "次比较：");

                Stopwatch watch = new Stopwatch();

                watch.Start();
                var result = list.OrderBy(single => single).ToList();
                watch.Stop();

                Console.WriteLine("\n系统定义的快速排序耗费时间：" + watch.ElapsedMilliseconds);
                Console.WriteLine("输出前是十个数:" + string.Join(",", result.Take(10).ToList()));

                watch.Start();
                SortOrder.QuickSort(list, 0, list.Count - 1);
                watch.Stop();

                Console.WriteLine("\n俺自己写的快速排序耗费时间：" + watch.ElapsedMilliseconds);
                Console.WriteLine("输出前是十个数:" + string.Join(",", list.Take(10).ToList()));

            }
        }

        /// <summary>
        /// 选择排序 vs 快速排序
        /// </summary>
        public static void SelectionVsQuick()
        {
            //5次比较
            for (int i = 1; i <= 5; i++)
            {
                List<int> list = new List<int>();

                //插入2w个随机数到数组中
                for (int j = 0; j < 2000; j++)
                {
                    Thread.Sleep(1);//休眠一毫秒生成随机地数字
                    var seek = DateTime.Now.Ticks.ToString();
                    int number = new Random(Convert.ToInt32(seek.Substring(seek.Length - 7, 7))).Next(0, 100000);
                    list.Add(number);
                }

                Console.WriteLine("\n第" + i + "次比较：");

                Stopwatch watch = new Stopwatch();

                watch.Start();
                var result = list.OrderBy(single => single).ToList();
                watch.Stop();

                Console.WriteLine("\n快速排序耗费时间：" + watch.ElapsedMilliseconds);
                Console.WriteLine("输出前十个数:" + string.Join(",", result.Take(10).ToList()));

                watch.Start();
                result = SortOrder.SelectionSort(list);
                watch.Stop();

                Console.WriteLine("\n直接选择排序耗费时间：" + watch.ElapsedMilliseconds);
                Console.WriteLine("输出前十个数:" + string.Join(",", list.Take(10).ToList()));

            }
        }

        /// <summary>
        /// 堆排序 vs 快速排序
        /// </summary>
        public static void HeapSortVsQuick()
        {
            //5次比较
            for (int j = 1; j <= 5; j++)
            {
                List<int> list = new List<int>();

                //插入2w个数字
                for (int i = 0; i < 20000; i++)
                {
                    Thread.Sleep(1);//休眠一毫秒生成随机地数字
                    var seek = DateTime.Now.Ticks.ToString();
                    int number = new Random(Convert.ToInt32(seek.Substring(seek.Length - 7, 7))).Next(0, 100000);
                    list.Add(number);
                }

                Console.WriteLine("\n第" + j + "次比较：");

                Stopwatch watch = new Stopwatch();
                watch.Start();
                var result = list.OrderBy(single => single).ToList();
                watch.Stop();
                Console.WriteLine("\n快速排序耗费时间:" + watch.ElapsedMilliseconds);
                Console.WriteLine("输出前十个数" + string.Join(",", result.Take(10).ToList()));

                watch = new Stopwatch();
                watch.Start();
                SortOrder.HeapSort(list);
                watch.Stop();
                Console.WriteLine("\n堆排序耗费时间:" + watch.ElapsedMilliseconds);
                Console.WriteLine("输出前十个数" + string.Join(",", list.Take(10).ToList()));
            }
        }
    }
}
