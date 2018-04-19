using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.delegates
{
    public class DelegateTest
    {
        delegate void MyAction(string str);

        delegate int MyAction2(string str);

        /// <summary>
        /// 普通方法
        /// </summary>
        /// <param name="str"></param>
        public void Fun1(string str)
        {
            Console.WriteLine($"Fun1:{str}");
        }

        /// <summary>
        /// 委托也可以调用static方法
        /// </summary>
        /// <param name="str"></param>
        public static void Fun2(string str)
        {
            Console.WriteLine($"Fun2:{str}");
        }

        
        
        /// <summary>
        /// 调用委托的类
        /// </summary>
        public static void DelegateFun()
        {
            //声明一个委托
            MyAction ac1 = Fun2;
            ac1("ac1");//调用委托
            DelegateTest test = new DelegateTest();
            MyAction ac_fun1 = test.Fun1;
            ac_fun1.Invoke("hello world");//Invoke方法也可以调用委托
            return;

            //匿名委托
            MyAction2 ac2 = delegate (string str)
            {
                Console.WriteLine(str);
                return 1;
            };
            ac2("ac2");

            //使用lambda表达式 
            MyAction2 ac3 = str => 1;
            Console.WriteLine(ac3("ac3"));

            // 使用泛型委托
            // Action是无返回值的泛型委托。它也是用：public delegate void Action<in T>(T obj);声明得到的
            // Action至少0个参数，至多16个参数，无返回值。
            Action<int, int> ac4 = (str, str2) => Console.WriteLine(str + str2);
            ac4(1, 2);

            // Func是有返回值的泛型委托。它是用：public delegate TResult Func<in T, out TResult>(T arg);声明得到的
            // Func至少0个参数，至多16个参数，根据返回值泛型返回。必须有返回值，不可void
            Func<int, int, bool> ac5 = (str, str2) =>
            {
                if (str > str2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            Console.WriteLine(ac5(5, 3));

            //myCase:
            Predicate<int> ac6 = (str) =>
            {
                return str % 2 == 0 ? true : false;
            };
            Console.WriteLine(ac6(0));

        }

        #region 如何操作多个DAL方法，并使用事务提交
        //原文:http://www.cnblogs.com/1996V/p/7798111.html

        /// <summary>
        /// 处理数据库操作
        /// </summary>
        public static void TestTransScope()
        {
            Action action = () => { };
            action += () => {
                Console.WriteLine("往A表中增加一条数据");
            };
            action += () => {
                Console.WriteLine("往B表中增加两条数据");
            };
            ExecTransScope(action);
        }

        /// <summary>
        /// 用Task实现
        /// </summary>
        public static void TestTransScope2()
        {
            Task task = new Task(() => {
                Console.WriteLine("往A表中增加一条数据");
            });
            Task task2 = new Task(() => {
                Console.WriteLine("往B表中增加两条数据");
            });
            Task task3 = new Task(() => {
                try
                {
                    Task.WaitAll(task, task2);
                    Console.WriteLine("提交");
                }
                catch(Exception)
                {
                    Console.WriteLine("回滚");
                }
            });
            task.Start();
            task2.Start();
            task3.Start();
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="action"></param>
        public static void ExecTransScope(Action action)
        {
            try
            {
                action.Invoke();
                Console.WriteLine("提交");
            }
            catch(Exception)
            {
                Console.WriteLine("回滚");
            }
        }

        #endregion

    }

    public class Employee
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Employee(string name,int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString() => $"{Name}，{Age:C}";

        public static bool CompareSalary(Employee e1, Employee e2) => e1.Age > e2.Age;
    }
}
