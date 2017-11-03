using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Test
{
    public class YieldTest
    {
        public IEnumerable<int> WithNoYield()
        {
            IList<int> list = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i.ToString());
                if (i > 2)
                    list.Add(i);
            }
            return list;
        }

        public IEnumerable<int> WithYield()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i.ToString());
                if (i > 2)
                    yield return i;
            }
        }
    }
}
