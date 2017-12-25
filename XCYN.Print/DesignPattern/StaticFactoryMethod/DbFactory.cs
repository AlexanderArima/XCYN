using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.StaticFactoryMethod
{
    public class DbFactory
    {
        public static IDataBase GetInstance(string db)
        {
            switch(db)
            {
                case "SqlServer":
                    return new SqlServer();
                case "MySql":
                    return new MySql();
                default:
                    return null;
            }
        }
    }
}
