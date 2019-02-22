using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Winform.Quartz.Model;

namespace XCYN.Winform.Quartz.ViewModel
{
    public class TriggerFormViewModel
    {

        public int ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int RepeatTime { get; set; }

        public int RepeatInterval { get; set; }

        public string ServiceName { get; set; }

        //public int Add(TimerFormViewModel model)
        //{
        //    T_ServiceList serviceList = new T_ServiceList();
        //    var id =  serviceList.GetID(model.ServiceName);
        //    T_SimpleTrigger trigger = new T_SimpleTrigger()
        //    {
        //        ID = model.ID,
        //        StartTime = model.StartTime,
        //        EndTime = model.EndTime,
        //        RepeatTime = model.RepeatTime,
        //        RepeatInterval = model.RepeatInterval,
        //        SID = id
        //    };
        //    return trigger.Add(trigger);
        //}

        public DataSet GetList()
        {
            T_SimpleTrigger trigger = new T_SimpleTrigger();
            var ds = trigger.GetList("");
            return ds;
        }
    }
}
