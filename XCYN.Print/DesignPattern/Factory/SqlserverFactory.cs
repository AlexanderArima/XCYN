using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Factory
{
    public class SqlserverFactory:IFactory
    {
        public IDataBase CreateInstance()
        {
            return new SqlServerDataBase();
        }
    }
}
