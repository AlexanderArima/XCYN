using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoTask
    {

        /// <summary>
        /// .NET 4为了简化多线程编程，提供了System.Threading.Tasks命名空间中的类来帮助开发者进行多线程编程，
        ///  其中，Task类用来表示一个线程。最简单的Task类接受一个Action委托作为要执行的方法,调用Start方法开始在另一个线程中运行。
        /// </summary>
        public void Fun1()
        {
            Task task = new Task(() => Console.WriteLine("开启子线程"));
            task.Start();
            Console.WriteLine("主线程");
            Console.ReadLine();
        }

        /// <summary>
        /// 使用Task.Factory.StartNew方法，这个方法会构造一个Task并且立刻开始运行，相当于将Task的构造函数和Start方法连在一起执行。
        /// </summary>
        public void Fun2()
        {
            for (int i = 0; i < 10000; i++)
            {
                Task t = new Task(obj => Console.WriteLine("Thread No " + obj), i);
                t.Start();
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 可以使用Task<T>类来获得返回值，T是返回值的类型
        /// </summary>
        public void Fun3()
        {
            Task<int> t = new Task<int>(() =>
            {
                int s = 0;
                for (int i = 0; i < 10000; i++)
                    s += i;
                return s;
            });
            t.Start();
            Console.WriteLine("I'm computing");
            //在访问t.Result的时候，.net 会保证此时Task的代码已经执行完毕，Result已经获得，否则该线程会阻塞，直到Result计算完毕。
            Console.WriteLine(t.Result);
            Console.ReadLine();
        }

        /// <summary>
        /// Task库提供了一种主动终止线程的方法，先创建一个CancellationTokenSource，将其Token属性通过Task构造函数传进去，
        /// 在Task内部轮询token的IsCancellationReqeusted属性，如果检测到为true，则主动终止线程。
        /// 在父线程内调用tokenSource的Cancel方法，可以终止线程。注意，这是线程主动终止自己的方法，
        /// 必须在Task内的代码自己终止，.NET不会强行终止task线程，即使父线程调用了tokenSource的Cancel方法。
        /// </summary>
        public void Fun4()
        {
            CancellationTokenSource tks = new CancellationTokenSource();
            CancellationToken token = tks.Token;
            long i = 0;
            Task t = new Task(() => {
                while (true)
                {
                    if(!token.IsCancellationRequested)
                    {
                        i++;
                    }
                    else
                    {
                        Console.WriteLine("Task is Canceled,it loop {0} times",i);
                        break;
                    }
                }
            }, token);
            t.Start();
            Console.WriteLine("Press Enter to Cancel task");
            Console.ReadLine();
            tks.Cancel();
            Console.ReadLine();
        }
    }
}
