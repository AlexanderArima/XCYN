
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common;
using XCYN.Common.Access;

namespace XCYN.Winform.Quartz.ViewModel
{
    public class EditServiceFormViewModel
    {
        #region Model
        private int _id;
        private string _servicename;
        private string _assemblyname;
        private string _namespace;
        private string _classname;
        private string _methodname;
        private bool _isdelete = false;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName
        {
            set { _servicename = value; }
            get { return _servicename; }
        }
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName
        {
            set { _assemblyname = value; }
            get { return _assemblyname; }
        }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName
        {
            set { _methodname = value; }
            get { return _methodname; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        #endregion Model

        /// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_ServiceList] set ");
            strSql.Append("ServiceName=@ServiceName,");
            strSql.Append("AssemblyName=@AssemblyName,");
            strSql.Append("NameSpace=@NameSpace,");
            strSql.Append("ClassName=@ClassName,");
            strSql.Append("MethodName=@MethodName");
            strSql.Append(" where ID=@ID ");
            OleDbParameter[] parameters = {
                    new OleDbParameter("@ServiceName", OleDbType.VarChar,255),
                    new OleDbParameter("@AssemblyName", OleDbType.VarChar,255),
                    new OleDbParameter("@NameSpace", OleDbType.VarChar,255),
                    new OleDbParameter("@ClassName", OleDbType.VarChar,255),
                    new OleDbParameter("@MethodName", OleDbType.VarChar,255),
                    new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = ServiceName;
            parameters[1].Value = AssemblyName;
            parameters[2].Value = NameSpace;
            parameters[3].Value = ClassName;
            parameters[4].Value = MethodName;
            parameters[5].Value = ID;

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

        /// <summary>
		/// 获得模型对象
		/// </summary>
		public EditServiceFormViewModel GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ServiceName,AssemblyName,NameSpace,ClassName,MethodName,IsDelete ");
            strSql.Append(" FROM [T_ServiceList] ");
            strSql.Append(" where ID = @ID And IsDelete = False ");
            var ds = DbHelperOleDb.Query(strSql.ToString(), 
                new OleDbParameter("@ID",id));
            if(ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                var row = ds.Tables[0].Rows[0];
                EditServiceFormViewModel model = new EditServiceFormViewModel()
                {
                    ID = ConvertHelper.ToInt(row["ID"]),
                    ServiceName = row["ServiceName"].ToString(),
                    AssemblyName = row["AssemblyName"].ToString(),
                    NameSpace = row["NameSpace"].ToString(),
                    ClassName = row["ClassName"].ToString(),
                    MethodName = row["MethodName"].ToString(),
                };
                return model;
            }
        }
    }
}
