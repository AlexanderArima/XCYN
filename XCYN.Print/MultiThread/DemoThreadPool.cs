using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    /*
     四：Thread 和 ThreadPool 到底多少区别。。。
     
     现在有10个任务，如果用Thread来做，需要开启10个Thread

	 如果用ThreadPool来做，只需要将10个任务丢给线程池
         
     */
    public class DemoThreadPool
    {
        public void Fun1()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) => {
                var str = obj as Func<string>;
                Console.WriteLine("工作线程ID:{0},obj:{1}",Thread.CurrentThread.ManagedThreadId,str());
            }),new Func<string>(()=> {
                return "Hello World";
            }));
            
            Console.WriteLine("主线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
