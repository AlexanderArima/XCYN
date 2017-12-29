using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Filter
{
    interface IFilter
    {
        List<Person> Filter(List<Person> list);
    }
}
