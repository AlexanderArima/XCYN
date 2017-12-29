using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Filter
{
    class OrFilter : IFilter
    {
        public List<IFilter> _list { get; set; }

        public OrFilter(List<IFilter> list)
        {
            _list = list;
        }

        public List<Person> Filter(List<Person> list)
        {
            //HashSet去重
            var set = new HashSet<Person>();
            foreach (var item in _list)
            {
                var persons = item.Filter(list);
                for (int i = 0; i < persons.Count; i++)
                {
                    set.Add(persons[i]);
                }
            }
            return set.ToList();
        }
    }
}
