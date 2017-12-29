using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Filter
{
    class AgeFilter : IFilter
    {
        public List<Person> Filter(List<Person> list)
        {
            return list.Where(i => i.age > 18).ToList();
        }
    }
}
