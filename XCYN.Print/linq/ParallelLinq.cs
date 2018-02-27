using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.linq
{
    /// <summary>
    /// 并行Linq查询
    /// </summary>
    public class ParallelLinq
    {
        static IEnumerable<int> SampleData()
        {
            int arraySize = 50000000;
            var r = new Random();
            return Enumerable.Range(0, arraySize -1).Select(x => r.Next(140)).ToList();
        }

        public void Fun1()
        {
            var q = from a in SampleData()
                    where Math.Log(a) < 4
                    select a;
            Console.WriteLine(q.Average());
        }
    }
}
