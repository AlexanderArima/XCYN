using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.MVC.ViewModel
{
    public class DSRWControlViewModel
    {
    }

    public class DSRWControlViewModel_AddTrigger
    {
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string triggerName { get; set; }

        /// <summary>
        /// 触发器组名
        /// </summary>
        public string triggerGroupName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Corn表达式
        /// </summary>
        public string cornExpression { get; set; }

        /// <summary>
        /// Job名称
        /// </summary>
        public string jobName { get; set; }

        /// <summary>
        /// Job组名
        /// </summary>
        public string jobGroupName { get; set; }
    }
}