using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.linq
{
    public class linqToObject
    {
        public void get()
        {
            goto myCase;
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            dic.Add(1, new List<int> { 10, 20, 30, 40 });
            dic.Add(2, new List<int> { 10, 20, 30, 40 });
            dic.Add(3, new List<int> { 10, 20, 30, 40 });
            //将序列中每个元素合并到一个数组中
            var query = dic.SelectMany(i => i.Value);
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
            
            int[] num1 = new int[] { 10, 20, 30, 40,50 };
            int[] num2 = new int[] { 10, 20, 30, 40 };
            //比较数组中每一个元素是否相等
            var query2 = num1.SequenceEqual(num2);
            Console.WriteLine(query2);

            myCase:
            int[] num3 = new int[] { 10, 20, 30, 40, 50 };
            int[] num4 = new int[] { 10, 20, 30, 40 };
            int[] num5 = new int[] { 10, 20, 30 };
            var query3 = num3.Zip(num4, (i, j) => i + j).Zip(num5, (i, j) => i + j);
            //对两个函数进行操作
            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }
        }
    }
}
