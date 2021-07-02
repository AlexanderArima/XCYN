using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    /// <summary>
    /// 匿名类型.
    /// </summary>
    public class NiMingType
    {
        /// <summary>
        /// 创建一个匿名对象，并使用.
        /// </summary>
        public static void Fun01()
        {
            var book = new
            {
                name = "深入解析C#",
                pages = 100,
            };

            var str = JsonConvert.SerializeObject(book);
            var name = book.name;
            var pages = book.pages;
            Console.WriteLine(str);
        }
    }
}
