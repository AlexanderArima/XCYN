using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.Operators
{
    public delegate void PropertyChange(string str);

    /// <summary>
    /// 主要运算符
    /// </summary>
    public class PrimaryOperators
    {

        /// <summary>
        /// ?操作符用于在执行成员访问 (?.) 或索引 (?[) 操作之前，测试是否存在 NULL。
        /// 这些运算符可帮助编写更少的代码来处理 null 检查，尤其是对于下降到数据结构。
        /// </summary>
        public void Fun1()
        {
            ArrayList list = null;
            int? len = list?.Count;//null if list is null  
            Console.WriteLine("length:"+len);

            string item = list?[0].ToString();//最后一个示例演示 NULL 条件运算符会短路。 如果条件成员访问和索引操作链中的某个操作返回 NULL，则该链其余部分的执行将停止。
            Console.WriteLine("item:"+len);

            //NULL 条件成员访问的另一个用途是使用非常少的代码以线程安全的方式调用委托。
            //旧方法需要如下所示的代码：
            PropertyChange handler = null;
            if (handler != null)
            {
                handler.Invoke("hello world");
            }
            //新的方法是要简单得多：
            PropertyChange handler2 = null;
            handler2?.Invoke("hello world");
        }

        /// <summary>
        /// 方括号 ([]) 可用于数组、索引器和属性。 还可用于指针。
        /// </summary>
        public void Fun2()
        {
            //声明一个数组
            int[] arr = new int[100];

            //给数组赋值
            arr[0] = 0;

            //数组取值
            int num = arr[0];

            //定义散列表的索引
            Hashtable table = new Hashtable();
            table["name"] = "cheng";

            //[]还能用于用于指定特性
            /*
                [System.Serializable]  
                public class SampleClass  
                {  
                    // Objects of this type can be serialized.  
                }  
             */
        }

        /// <summary>
        /// 递增运算符 (++) 按 1 递增其操作数。 递增运算符可以在其操作数之前或之后出现：
        /// </summary>
        public void Fun3()
        {
            double x;
            x = 1.5;
            Console.WriteLine(++x);
            x = 1.5;
            Console.WriteLine(x++);
            Console.WriteLine(x);
            /*
            Output
            2.5
            1.5
            2.5
            */
        }

        /// <summary>
        /// new运算符
        /// </summary>
        public void Fun4()
        {
            //new运算符用于创建对象和调用构造函数
            ArrayList list = new ArrayList();

            //还能用于创建匿名类型的实例
            var person = new
            {
                name = "cheng",
                age = 12,
            };

            //还能用于值类型的默认构造函数
            int i = new int();
            //等同于
            int q = 0;

            //值类型对象（例如结构）是在堆栈上创建的，而引用类型对象（例如类）是在堆上创建的。
            //这两种类型的对象均被自动销毁，但基于值类型的对象是在它们超出范围时被销毁，
            //而基于引用类型的对象是在删除对它们的最后一个引用后在非指定时间被销毁。 
            //对于占用固定资源（如大量内存、文件句柄或网络连接）的引用类型，有时需要应用确定性终结，以确保尽快销毁对象。
        }

        public int temp = 0;

        /// <summary>
        /// typeof用于为类型获取 System.Type 对象
        /// </summary>
        public void Fun5()
        {
            var type = typeof(PrimaryOperators);
            Console.WriteLine(type);

            PrimaryOperators p = new PrimaryOperators();
            //若要获取表达式的运行时类型，可以使用 .NET Framework 方法 GetType
            Console.WriteLine(p.GetType());

            //获取Type的公共方法和公共成员
            Console.WriteLine("Methods:");
            foreach (var item in type.GetMethods())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Members:");
            foreach (var item in type.GetMembers())
            {
                Console.WriteLine(item);
            }
            Console.Read();
        }

        /// <summary>
        /// check和unchecked运算符
        /// </summary>
        public void Fun6()
        {
            byte b = byte.MaxValue;
            //执行后会抛出异常
            //checked
            //{
            //b++;
            //}
            //不会抛出异常，但会丢失数据，因为byte类型不包含256，溢出的位会被丢弃，所得到的变量是0
            unchecked
            {
                b++;
            }
            Console.WriteLine("b:"+b);
            Console.Read();
        }

        /// <summary>
        /// default表达式
        /// </summary>
        public void Fun7()
        {
            //默认值表达式生成类型的默认值。 默认值表达式在泛型类和泛型方法中非常有用。 
            //使用泛型类和泛型方法时出现的一个问题是，如何在无法提前知道以下内容的情况下将默认值赋值给参数化类型
            var s = default(string);
            var d = default(dynamic);
            var i = default(int);
            var n = default(int?); // n is a Nullable int where HasValue is false.
        }
        
        /// <summary>
        /// delegate关键字
        /// </summary>
        public void Fun8()
        {
            //由于使用匿名方法无需创建单独的方法，因此可减少对委托进行实例化的编码开销。
            Thread thread = new Thread(delegate() {
                Thread.Sleep(500);
                Console.WriteLine("工作线程");
            });
            thread.Start();
            Console.WriteLine("主线程");
            Console.Read();
        }

        
    }
}
