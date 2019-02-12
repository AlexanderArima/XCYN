using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common.Access;

namespace XCYN.Winform.Quartz
{
    public class T_SimpleTrigger
    {

        public int ID { get; set; }
        
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int RepeatTime { get; set; }

        public int RepeatInterval { get; set; }

        public int SID { get; set; }

        /// <summary>
        /// 添加
        /// </summary>
        public int Add()
        {
            //判断
            return OleDbHelper.ExecuteNonQuery(@"
                INSERT INTO T_SimpleTrigger(StartTime,EndTime,RepeatTime,RepeatInterval) 
                Values(@StartTime,@EndTime,@RepeatTime,@RepeatInterval)",
                new OleDbParameter("@StartTime", this.StartTime),
                new OleDbParameter("@EndTime", this.EndTime),
                new OleDbParameter("@RepeatTime", this.RepeatTime),
                new OleDbParameter("@RepeatInterval", this.RepeatInterval));
        }
    }
}
