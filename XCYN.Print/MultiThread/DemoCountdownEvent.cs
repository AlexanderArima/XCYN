using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 CountdownEvent 表示在计数变为0时处于有信号状态的同步基元 通过信号机制
 CountdownEvent基于这样一个简单的规则：
 当有新的需要同步的任务产生时，就调用AddCount增加它的计数，
 当有任务到达同步点是，就调用Signal函数减小它的计数，
 当CountdownEvent的计数为零时，就表示所有需要同步的任务已经完成，可以开始下一步任务了。
  */
namespace XCYN.Print.MultiThread
{
    public class DemoCountdownEvent
    {

        static CountdownEvent cd = new CountdownEvent(10);

        public void Fun1()
        {
            //10个线程写入Order表
            cd.Reset(10);
            for (int i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(() => {
                    WriteOrder();
                });

            }
            cd.Wait();//等待线程执行完毕

            //5个线程写入Order表
            cd.Reset(5);
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => {
                    WriteProduct();
                });
            }
               
            cd.Wait();//等待线程执行完毕

            //2个线程写入Order表
            cd.Reset(2);
            for (int i = 0; i < 2; i++)
            {
                Task.Factory.StartNew(() => {
                    WriteUser();
                });
            } 
            cd.Wait();//等待线程执行完毕

            Console.WriteLine("所有线程执行完毕!");
            Console.Read();
        }

        /// <summary>
        /// 写入Order表
        /// </summary>
        private void WriteOrder()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("写入Order表,t={0}", Thread.CurrentThread.ManagedThreadId);

            }

            cd.Signal();//这个方法一定要放到方法的末尾!
        }

        /// <summary>
        /// 写入Product表
        /// </summary>
        private void WriteProduct()
        {
            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine("写入Product表,t={0}", Thread.CurrentThread.ManagedThreadId);
            }
            cd.Signal();//这个方法一定要放到方法的末尾!
        }

        /// <summary>
        /// 写入User表
        /// </summary>
        private void WriteUser()
        {
            for (int i = 0; i < 200; i++)
            {
                Console.WriteLine("写入User表,t={0}", Thread.CurrentThread.ManagedThreadId);
            }

            cd.Signal();//这个方法一定要放到方法的末尾!
        }
    }
}
