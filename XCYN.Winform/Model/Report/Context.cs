using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.Model.Report
{
    public class Context
    {
        public AbstractState state { get; set; }

        public string reportName { get; set; }

        public Context(AbstractState state, string reportName)
        {
            this.reportName = reportName;
            this.state = state;
        }

        /// <summary>
        /// 发出请求
        /// </summary>
        /// <param name="report"></param>
        public object Request()
        {
            return state.Handle(this);
        }
    }
}
