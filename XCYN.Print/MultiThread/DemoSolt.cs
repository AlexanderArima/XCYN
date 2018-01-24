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
    }
}
