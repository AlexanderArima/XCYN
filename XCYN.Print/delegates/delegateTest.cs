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
        /// 冒泡排序
        /// </summary>
        /// <param name="sortArray"></param>
        /// <returns></returns>
        public static int[] BubbleSorter(int[] sortArray)
        {
            bool flag = true;
            do
            {
                flag = false;
                for (int i = 0; i < sortArray.Length - 1; i++)
                {
                    if(sortArray[i] > sortArray[i + 1])
                    {
                        //交换位置
                        var temp = sortArray[i];
                        sortArray[i] = sortArray[i + 1];
                        sortArray[i + 1] = temp;
                        flag = true;
                    }
                }
            } while (flag);
            return sortArray;
        }

        /// <summary>
        /// 冒泡排序(不仅仅能对数组进行排序，还能对对象中的数组进行排序)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortArray"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IList<T> BubbliSorter<T>(IList<T> sortArray,Func<T,T,bool> comparison)
        {
            bool flag = true;
            do
            {
                flag = false;
                for (int i = 0; i < sortArray.Count - 1; i++)
                {
                    if (comparison(sortArray[i],sortArray[i + 1]))
                    {
                        //交换位置
                        var temp = sortArray[i];
                        sortArray[i] = sortArray[i + 1];
                        sortArray[i + 1] = temp;
                        flag = true;
                    }
                }
            } while (flag);
            return sortArray;
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
