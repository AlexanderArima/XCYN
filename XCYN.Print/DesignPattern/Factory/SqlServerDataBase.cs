using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Factory
{
    /// <summary>
    /// 被工厂创建的实现类
    /// </summary>
    public class SqlServerDataBase:IDataBase
    {
        public void Create()
        {
            Console.WriteLine("创建sqlserver");
        }

        public void Remove()
        {
            Console.WriteLine("移除sqlserver");
        }
    }
}
