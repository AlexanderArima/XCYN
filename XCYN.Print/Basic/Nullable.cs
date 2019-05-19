using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    /// <summary>
    /// 可空类型
    /// </summary>
    public class Nullable
    {
        //public string? Name { get; set; } //编译器会报错，因为引用类型本身就是可以为空的。。

        public string Name { get; set; }

        public int? Age { get; set; }   //int为值类型，值类型不是可为空的类型，但Nullable将值类型拓展为可为空类型了

        //public Nullable<int> Age { get; set; }    //也可以这样声明

        public void Fun1()
        {
            
        }
    }
}
