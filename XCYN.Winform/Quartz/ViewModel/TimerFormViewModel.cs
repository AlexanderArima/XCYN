using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Winform.Quartz.Model;

namespace XCYN.Winform.Quartz.ViewModel
{
    public class TimerFormViewModel
    {
        public DataSet GetList()
        {
            T_SimpleTrigger trigger = new T_SimpleTrigger();
            var ds = trigger.GetList("");
            return ds;
        }
    }
}
