using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoTimer
    {
        public void Fun1()
        {
            ThreadPool.RegisterWaitForSingleObject(new AutoResetEvent(true), new WaitOrTimerCallback((obj, b) =>
            {
                Console.WriteLine("obj:{0},ThreadID:{1},DateTime:{2}", obj, Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            }), "hello World", 1000, false);

        }

        /// <summary>
        /// 轮询
        /// </summary>
        public void Fun2()
        {
            Timer t = new Timer(new TimerCallback((obj) => {
                Console.WriteLine("obj:{0},ThreadID:{1},DateTime:{2}", obj, Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            }), "hello world", 0, 1000);

        }
    }
}
