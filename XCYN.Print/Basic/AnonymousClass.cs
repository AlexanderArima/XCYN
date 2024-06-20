using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    /// <summary>
    /// 匿名类.
    /// </summary>
    internal static class AnonymousClass
    {
        /// <summary>
        /// 声明一个匿名类.
        /// </summary>
        public static void Fun01()
        {
            // 使用了隐式推断类型var，匿名类new{object initalizer}
            var p = new
            {
                id = 1,
                name = "张三",
                age = 18,
                sex = "男",
            };

            var json = JsonConvert.SerializeObject(p);
            Console.WriteLine(json);

            var list = new[]
            {
                new
                {
                    id = 1,
                    name = "张三",
                    age = 18,
                    sex = "男",
                },
                new
                {
                    id = 2,
                    name = "李四",
                    age = 18,
                    sex = "男",
                },
                new
                {
                    id = 3,
                    name = "王五",
                    age = 18,
                    sex = "男",
                },
            };

            json = JsonConvert.SerializeObject(list);
            Console.WriteLine(json);
        }

        /// <summary>
        /// 获取该字符串包括多少个数字.
        /// </summary>
        /// <param name="str">字符串.</param>
        public static int GetNumberLength(this string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int temp;
                var flag = int.TryParse(str[i].ToString(), out temp);
                if (flag == true)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
