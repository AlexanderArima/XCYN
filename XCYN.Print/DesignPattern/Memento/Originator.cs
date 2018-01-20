using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Memento
{
    public class Originator
    {
        public string msg { get; set; }

        public string name { get; set; }

        /// <summary>
        /// 创建一个备忘录
        /// </summary>
        /// <returns></returns>
        public Memento CreateMemento()
        {
            return new Memento()
            {
                msg = this.msg
            };
        }

        /// <summary>
        /// 恢复之前的状态
        /// </summary>
        /// <param name="memento"></param>
        public void RecoverMemento(Memento memento)
        {
            this.msg = memento.msg;
        }
    }
}
