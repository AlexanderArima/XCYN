using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCYN.Core.MVC.Models
{
    public interface IFormula
    {
        /// <summary>
        /// 编号
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// 开奖期数
        /// </summary>
        string kjqs { get; set; }

        /// <summary>
        /// 开奖日期
        /// </summary>
        DateTime kjrq { get; set; }
    }
}
