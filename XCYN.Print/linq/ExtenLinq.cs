using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.linq.DataLib;

namespace XCYN.Print.linq
{
    /// <summary>
    /// Linq的拓展方法
    /// </summary>
    public class ExtenLinq
    {
        public void Fun1()
        {
            var list = Formula1.GetChampions().Select((r) => {
                r.FirstName = r.FirstName.ToUpper();
                r.LastName = r.LastName.ToUpper();
                r.Country = r.Country.ToUpper();
                r.Cars = r.Cars.Select((m) => m.ToUpper()).ToList();
                return r;

            });
            
        }
    }
}
