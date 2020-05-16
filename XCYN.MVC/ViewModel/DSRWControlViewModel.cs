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
    public class DSRWControlViewModel_AddCalendar
    {
        /// <summary>
        /// 日历名称
        /// </summary>
        public string calendarName { get; set; }

        /// <summary>
        /// 日历类型
        /// </summary>
        public string calendarType { get; set; }

        /// <summary>
        /// 日历的值
        /// </summary>
        public string calendarValue { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 触发器的组名
        /// </summary>
        public string triggerGroup { get; set; }

        /// <summary>
        /// 触发器的名称
        /// </summary>
        public string triggerName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
    }
}