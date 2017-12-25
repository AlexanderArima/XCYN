using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.TemplateMethod
{
    public abstract class Exam
    {
        public virtual string Name { get; set; }

        public void Question()
        {
            Console.WriteLine("{0},接口和抽象类有什么区别？{1}", this.Name, this.Answer());
        }

        public virtual string Answer()
        {
            return string.Empty;
        }
    }
}
