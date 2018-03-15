using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.yield
{
    public class YieldTest
    {
        public static IEnumerable<int> WithNoYield()
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

        public static IEnumerable<int> WithYield()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i.ToString());
                if (i > 2)
                    yield return i;
            }
        }

        
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BookAttribute : Attribute
    {
        private string BookName;

        public BookAttribute(string BookName)
        {
            this.BookName = BookName;
            Console.WriteLine(BookName);
        }
    }
}
