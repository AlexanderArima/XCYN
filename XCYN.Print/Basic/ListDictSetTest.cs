using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    public class ListDictSetTest
    {

        /// <summary>
        /// List按值查找的时间复杂度为0(n)，SortedSet,Dictionary,SortedDictionary按值查找的时间复杂度为0(1)
        /// </summary>
        public void Fun1()
        {
            List<string> list = new List<string>();
            SortedSet<string> ss = new SortedSet<string>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            SortedDictionary<string, string> sdict = new SortedDictionary<string, string>();
            for (int i = 0; i <= 1000000; i++)
            {
                list.Add(i.ToString());
                ss.Add(i.ToString());
                dict.Add(i.ToString(), i.ToString());
                sdict.Add(i.ToString(), i.ToString());
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            list.FirstOrDefault(m => m =="1000000");
            sw.Stop();

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            ss.FirstOrDefault(m => m == "1000000");
            sw2.Stop();

            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            var s = dict["1000000"];
            sw3.Stop();

            Stopwatch sw4 = new Stopwatch();
            sw4.Start();
            var s2 = sdict["1000000"];
            sw4.Stop();

            Console.WriteLine($"list:{sw.Elapsed},hastset:{sw2.Elapsed},dictionary:{sw3.Elapsed},sorteddictionary:{sw4.Elapsed}");
        }
    }
}
