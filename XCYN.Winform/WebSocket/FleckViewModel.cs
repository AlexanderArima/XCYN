using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.WebSocket
{
    public class FleckViewModel
    {
        public static List<string> list_firstName = new List<string>()
        { "张", "李" , "王", "赵", "方"};

        public static List<string> list_lastName = new List<string>()
        { "一","二","三","四","五" };

        public static string GetRandomName()
        {
            Random r1 = new Random();
            var index1 = r1.Next(5);
            Random r2 = new Random();
            var index2 = r2.Next(5);
            return list_firstName.ElementAt(index1) + list_lastName.ElementAt(index2);
        }
    }
}
