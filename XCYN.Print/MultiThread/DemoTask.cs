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
            Task t = Task.Factory.StartNew(() => {
                Console.WriteLine("子线程");
            });
            Console.WriteLine("主线程");
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
            token.Register(() => { Console.WriteLine("停止执行的回调方法"); });
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

        /// <summary>
        /// 要挂起当前线程，等待一个线程执行完成，可以使用执行线程的Wait（）方法，Wait方法有一些重载方法，可以指定等待的时间等。
        /// 要等到多个线程都执行完可以使用Task.WaitAll方法
        /// 还有一个Task.WaitAny,可以等待一组线程中的任何一个方法执行完毕，用法类似。
        /// </summary>
        public void Fun5()
        {
            Task t1 = new Task(() => {
                Console.WriteLine("Task1 is Begin");
                Thread.Sleep(3000);
                Console.WriteLine("Task1 is End");
            });
            t1.Start();

            Task t2 = new Task(()=> {
                Console.WriteLine("Task2 is Begin");
                t1.Wait();
                Console.WriteLine("Task2 is End");
            });

            Task t3 = new Task(()=> {
                Console.WriteLine("Task3 is Begin");
                t1.Wait(1000);
                Console.WriteLine("Task3 is End");
            });

            Task t4 = new Task(() => {
                Console.WriteLine("Task4 is Begin");
                Task.WaitAll(t1, t2, t3);
                Console.WriteLine("Task4 is End");
            });

            Task t5 = new Task(()=> {
                Console.WriteLine("Task5 is Begin");
                Task.WaitAny(t1, t2, t3);
                Console.WriteLine("Task5 is End");
            });
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            Console.Read();

        }

        /// <summary>
        /// 下面介绍Task中的异常处理，通常情况下，线程委托中的异常会导致线程终止，但异常并不会被抛出.
        /// 当调用Wait,WaitAll,WaitAny,Task.Result的时候，会抛出AggerateException ,在AggerateExcepiton中可以处理所有线程抛出的异常:
        /// </summary>
        public void Fun6()
        {
            Task t1 = new Task(() =>
            {
               // throw new Exception();
                Console.WriteLine("T1 Ends");
            });

            Task t2 = new Task(() =>
            {
                //throw new ArgumentException();
                Console.WriteLine("T2 Ends");
            });
            t1.Start();
            t2.Start();
            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.GetType() + " " + inner.Source);
                }
                //有时候需要区分对待某些异常，一些异常直接处理掉，一些异常需要再次抛出，
                //AggregateException提供一个Handle方法，接收一个Func<Exception,bool>委托作为参数，如果不需要再次抛出则返回true，否则返回false。
                ex.Handle((e) =>
                {
                    if (e is ArgumentException)
                    {
                        Console.WriteLine("Argument Exception is captured");
                        return true;
                    }
                    else
                        return false;
                });
            }
            Console.WriteLine("Main Ends");
            Console.Read();
        }

        /// <summary>
        /// Join方法相当于Wait方法
        /// </summary>
        public void Fun7()
        {
            Thread t = new Thread(new ThreadStart(new Action(()=> {
                Thread.Sleep(5000);
                Console.WriteLine("Thread...");
            })));
            t.Start();
            //t.Join(3000);//最多等待3s钟
            t.Join();//一直等待线程执行完成
            Console.WriteLine("Main Thread");
        }

        /// <summary>
        /// Task通过Run方法执行异步线程
        /// </summary>
        public void Fun8()
        {
            Task.Run(() => {
                Thread.Sleep(2000);
                Console.WriteLine("执行子线程");
            });
            Console.WriteLine("执行主线程");
        }

        /// <summary>
        /// Task调用S
        /// </summary>
        public void Fun9()
        {
            Task t = new Task(()=> {
                Thread.Sleep(2000);
                Console.WriteLine("执行子线程");
            });
            t.RunSynchronously();
            Console.WriteLine("执行主线程");

        }

        public void Fun10()
        {
            Task.Factory.StartNew(Run1);
            Task.Factory.StartNew(Run2);
            Console.WriteLine("调用主线程，ThreadID:{0}", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run1()
        {
            Thread.Sleep(5000);
            Console.WriteLine("调用工作线程1，ThreadID:{0}", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run2()
        {
            Thread.Sleep(2000);
            Console.WriteLine("调用工作线程2，ThreadID:{0}", Thread.CurrentThread.ManagedThreadId);
        }

    }
}
