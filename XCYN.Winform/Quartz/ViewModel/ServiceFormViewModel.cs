using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common.Access;
using XCYN.Winform.Quartz.Model;

namespace XCYN.Winform.Quartz.ViewModel
{
    public class ServiceFormViewModel
    {
        public DataSet GetList()
        {
            T_ServiceList list = new T_ServiceList();
            return list.GetList("");
        }

        public bool Delete(List<int> list_id)
        {
            var id = string.Join(",", list_id);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_ServiceList] set ");
            strSql.Append("IsDelete=True ");
            strSql.Append(" where ID in(@ID) ");
            OleDbParameter[] parameters = {
                    new OleDbParameter("@ID", OleDbType.VarChar)};
            parameters[0].Value = id;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
