using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Factory
{
    public class SqliteDataBase : IDataBase
    {
        public void Create()
        {
            Console.WriteLine("创建sqlite");
        }

        public void Remove()
        {
            Console.WriteLine("移除sqlite");
        }
    }
}
