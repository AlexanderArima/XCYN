using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoThread
    {
        public void Fun1()
        {
            Thread thread = new Thread(Run1);
            Thread thread2 = new Thread(Run2);
            thread.Start();
            thread2.Start();
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
