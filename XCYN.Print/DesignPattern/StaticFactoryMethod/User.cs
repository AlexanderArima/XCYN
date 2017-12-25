using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace XCYN.Print.DesignPattern.StaticFactoryMethod
{
    public class User
    {
        public static string db = ConfigurationManager.AppSettings["StaticFactoryMethod_db"];
        public void Insert()
        {
            var conn = DbFactory.GetInstance(db);
        }
    }
}
