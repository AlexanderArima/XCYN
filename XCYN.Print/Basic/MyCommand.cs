using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    public class MyCommand
    {
        public void Fun1()
        {
            using (MyConnection conn = new MyConnection())
            {
                conn.Open();
                throw new Exception("");
            }
        }

        /// <summary>
        /// 使用内插字符串的自变量.
        /// </summary>
        public static void Fun02()
        {
            var id = "202107020001";
            var name = "张三";
            Console.WriteLine($"The id is {id}, The name is {name}");
        }
    }
}
