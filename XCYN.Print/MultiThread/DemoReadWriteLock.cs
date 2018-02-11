using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    public class DemoReadWriteLock
    {

        /// <summary>
        /// 读写锁
        /// </summary>
        static ReaderWriterLock rw_lock = new ReaderWriterLock();

        /// <summary>
        /// 读锁
        /// </summary>
        public void Fun1()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => {
                    while(true)
                    {
                        Thread.Sleep(10);
                        //开启读锁
                        rw_lock.AcquireReaderLock(int.MaxValue);

                        Thread.Sleep(10);
                        Console.WriteLine("ReadThread:t={0},time={1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);

                        //释放
                        rw_lock.ReleaseReaderLock();
                    }
                });
            }
        }

        /// <summary>
        /// 写锁
        /// </summary>
        public void Fun2()
        {
            Task.Factory.StartNew(() => {
                while (true)
                {
                    Thread.Sleep(1000);
                    //开启写锁
                    rw_lock.AcquireWriterLock(int.MaxValue);

                    Thread.Sleep(1000);
                    Console.WriteLine("WriteThread:t={0},time={1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);

                    rw_lock.ReleaseWriterLock();
                }
                
            });
        }
       

        public void Fun3()
        {
            int sum = 10000;
            int sum2 = 5000;
            int sum3 = 2000;
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => {
                    while(sum > 0)
                    {
                        sum--;
                        Console.WriteLine("读取Orders表，sum={0}",sum);
                    }
                    
                }).ContinueWith((obj)=> {
                    while (sum2 > 0)
                    {
                        sum2--;
                        Console.WriteLine("读取Products表，sum2={0}", sum2);
                    }
                    
                }).ContinueWith((obj) => {
                    while (sum3 > 0)
                    {
                        sum3--;
                        Console.WriteLine("读取Logs表，sum3={0}", sum3);
                    }
                    
                });
            }
        }
    }
}
