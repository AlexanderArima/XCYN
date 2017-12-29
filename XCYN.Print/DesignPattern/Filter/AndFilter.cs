using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Filter
{
    class AndFilter : IFilter
    {

        public List<IFilter> _list { get; set; }

        public AndFilter(List<IFilter> list)
        {
            _list = list;
        }

        public List<Person> Filter(List<Person> list)
        {
            var temp = new List<Person>(list);
            //一次过滤多个过滤器
            foreach(var item in _list)
            {
                temp = item.Filter(list);
            }
            return temp;
        }
    }
}
