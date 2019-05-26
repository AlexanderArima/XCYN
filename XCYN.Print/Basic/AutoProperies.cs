using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    /// <summary>
    /// 自动属性
    /// </summary>
    public class AutoProperies
    {
        public int id { get; set; }
        
        private int age;

        public void set_age(int age)
        {
            this.age = age;
        }

        public int get_age()
        {
            return this.age;
        }
    }
}
