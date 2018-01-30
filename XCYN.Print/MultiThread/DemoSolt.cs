using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoSolt
    {
        /// <summary>
        /// 为所有线程分配存储的数据槽
        /// </summary>
        public void Fun1()
        {
            var solt = Thread.AllocateDataSlot();

            Thread.SetData(solt, "cheng");

            Thread t = new Thread(() => {
                //工作线程
                var name2 = Thread.GetData(solt);
            });
            t.Start();

            //主线程
            var name = Thread.GetData(solt);
        }

        /// <summary>
        /// 有名字的数据插槽
        /// </summary>
        public void Fun2()
        {
            var solt = Thread.AllocateNamedDataSlot("name");

            Thread.SetData(solt, "cheng");
            
            Thread t2 = new Thread(() =>
            {
                var age = Thread.GetData(solt);
            });

            Thread t = new Thread(() => {
                //工作线程
                var name2 = Thread.GetData(solt);

                solt = Thread.AllocateNamedDataSlot("age");

                Thread.SetData(solt, 12);

                var age = Thread.GetData(solt);

                t2.Start();
            });
            t.Start();

            //主线程
            var name = Thread.GetData(solt);

            
        }

        [ThreadStatic]
        static string username = "Hello World";
        /// <summary>
        /// ThreadStatic是一种高性能的数据存储
        /// </summary>
        public void Fun3()
        {
            Thread t = new Thread(() =>
            {
                Console.WriteLine("当前工作线程:"+username);
            });
            t.Start();
            Console.WriteLine("当前主线程:"+username);
            Console.Read();
        }

        /// <summary>
        /// ThreadLocal存储线程本地数据
        /// </summary>
        public void Fun4()
        {
            ThreadLocal<string> local = new ThreadLocal<string>();
            local.Value = "Hello World";
            Thread t = new Thread(()=> {
                Console.WriteLine("当前工作线程"+local.Value);
            });
            t.Start();
            Thread.Sleep(100);
            Console.WriteLine("当前主线程:"+local.Value);
            Console.Read();
        }

        public void Fun5()
        {
            ThreadLocal<string> local = new ThreadLocal<string>();
            Thread t = new Thread(() => {
                local.Value = "Hello World";
                Console.WriteLine("当前工作线程" + local.Value);
            });
            t.Start();
            Thread.Sleep(100);
            Console.WriteLine("当前主线程:" + local.Value);
            Console.Read();
        }
    }
}
