using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    public class BasicPrgLng
    {

        /// <summary>
        /// 属性Conditional带有DEBUG参数时，表示C#编译器以DEBUG符号创建代码时。Fun1方法才作为类的一部分可以调用
        /// 否则调用Fun1方法是无效的。
        /// </summary>
        [Conditional("DEBUG")]
        public static void Fun1()
        {
            Console.WriteLine("哈哈哈");
        }

        /// <summary>
        /// Obsolete属性用于定义正在被替换或者不再有效的代码。
        /// 该方法有两个参数：Message和IsError，Message用于设置错误信息字符串，IsError默认为false，即编译代码时发出警告，如果该值为true编译器将生成错误。
        /// </summary>
        [Obsolete("该方法已失效，请调用Fun3方法!")]
        public static void Fun2()
        {
            Console.WriteLine("呵呵呵");
        }

        [Book("嘻嘻嘻")]
        public static void Fun3()
        {
            Console.Write("哦哦哦");
        }

        /// <summary>
        /// C#数组类型转换
        /// </summary>
        public static void Fun4()
        {
            int[] i = new int[] { 1, 2, 3, 4, 5 };
            string[] s = Array.ConvertAll<int, string>(i, new Converter<int, string>((m) => m.ToString()));
            foreach (var item in s)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// StringReader对象实例，调用Read方法将读取到的字符数据保存在字符数组中，从而实现将字符串转换为字符数组的功能。
        /// </summary>
        public static void Fun5()
        {
            string str = "Are you sure?从这里开始吗?";
            char[] c = new char[str.Length];
            using (StringReader read = new StringReader(str))
            {
                read.Read(c, 0, c.Length);
            }
            foreach (var item in c)
            {
                Console.WriteLine((int)item);
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
