using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.TemplateMethod
{
    public class XieCheng : Exam
    {
        /// <summary>
        /// 重写会改变的属性
        /// </summary>
        public override string Name
        {
            get
            {
                return "xiecheng";
            }
            set
            {
                base.Name = value;
            }
        }

        /// <summary>
        /// 重写会改变的方法
        /// </summary>
        /// <returns></returns>
        public override string Answer()
        {
            return "A";
        }
    }
}
