using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    public class MyConnection : IDisposable
    {

        public void Open()
        {
            Console.WriteLine("MyConnection打开连接");
        }

        public void Dispose()
        {
            Console.WriteLine("MyConnection已被释放");
        }
    }
}
