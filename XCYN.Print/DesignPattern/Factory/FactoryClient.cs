using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Factory
{
    public class FactoryClient
    {
        public void Fun1()
        {
            DataBaseFactory factory = new DataBaseFactory();
            var db = factory.GetDataBase(DataBaseName.SQLITE);
            db.Create();
            db.Remove();
        }
    }
}
