using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common.Access;

namespace XCYN.Winform.Quartz.Model
{
    public class T_SimpleTrigger
    {

        public int ID { get; set; }
        
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int RepeatTime { get; set; }

        public int RepeatInterval { get; set; }

        public int SID { get; set; }

        //public string ServiceName { get; set; }

        /// <summary>
        /// 添加
        /// </summary>
        public int Add(string serviceName)
        {
            //判断，ServiceName查找对应的SID
            var ds = DbHelperOleDb.Query(string.Format(@"
                SELECT ID FROM T_ServiceList WHERE ServiceName = '{0}'", serviceName));
            if(ds.Tables[0].Rows.Count == 0)
            {
                throw new ArgumentException("ServiceName:" + serviceName + "，在T_ServiceList表中不存在");
            }
            var sid = ds.Tables[0].Rows[0]["ID"].ToString();
            
            return OleDbHelper.ExecuteNonQuery(@"
                INSERT INTO T_SimpleTrigger(StartTime,EndTime,RepeatTime,RepeatInterval) 
                Values(@StartTime,@EndTime,@RepeatTime,@RepeatInterval)",
                new OleDbParameter("@StartTime", this.StartTime),
                new OleDbParameter("@EndTime", this.EndTime),
                new OleDbParameter("@RepeatTime", this.RepeatTime),
                new OleDbParameter("@RepeatInterval", this.RepeatInterval));
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ST.*,SL.ServiceName AS ServiceName ");
            strSql.Append(" FROM [T_SimpleTrigger] AS ST");
            strSql.Append(" LEFT JOIN T_ServiceList AS SL");
            strSql.Append(" ON ST.SID = SL.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
    }
}
