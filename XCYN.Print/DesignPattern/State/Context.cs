using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.State
{
    /// <summary>
    /// 暴露在外部的接口，供调用
    /// </summary>
    public class Context
    {
        public int hour { get; set; }

        public AbstractState state { get; set; }

        public Context(int hour,AbstractState state)
        {
            this.hour = hour;
            this.state = state;
        }

        /// <summary>
        /// 开始判断条件，输出结果
        /// </summary>
        public void request()
        {
            state.Handle(this);
        }
    }
}
