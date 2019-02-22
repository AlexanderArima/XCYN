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
    public class AddServiceFormViewModel
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
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_ServiceList] (");
            strSql.Append("ServiceName,AssemblyName,NameSpace,ClassName,MethodName,IsDelete)");
            strSql.Append(" values (");
            strSql.Append("@ServiceName,@AssemblyName,@NameSpace,@ClassName,@MethodName,@IsDelete)");
            OleDbParameter[] parameters = {
                    new OleDbParameter("@ServiceName", OleDbType.VarChar,255),
                    new OleDbParameter("@AssemblyName", OleDbType.VarChar,255),
                    new OleDbParameter("@NameSpace", OleDbType.VarChar,255),
                    new OleDbParameter("@ClassName", OleDbType.VarChar,255),
                    new OleDbParameter("@MethodName", OleDbType.VarChar,255),
                    new OleDbParameter("@IsDelete", OleDbType.Boolean,1)};
            parameters[0].Value = ServiceName;
            parameters[1].Value = AssemblyName;
            parameters[2].Value = NameSpace;
            parameters[3].Value = ClassName;
            parameters[4].Value = MethodName;
            parameters[5].Value = IsDelete;

            return DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<string> GetAssemblyList()
        {
            List<string> list = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct AssemblyName ");
            strSql.Append(" FROM [T_ServiceList] ");
            strSql.Append(" where IsDelete = False");
            var ds = DbHelperOleDb.Query(strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var row = ds.Tables[0].Rows[i];
                list.Add(row["AssemblyName"].ToString());
            }
            return list;
        }

        /// <summary>
        /// 获得命名空间
        /// </summary>
        public List<string> GetNameSpaceList()
        {
            List<string> list = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct NameSpace ");
            strSql.Append(" FROM [T_ServiceList] ");
            strSql.Append(" where IsDelete = False");
            var ds = DbHelperOleDb.Query(strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var row = ds.Tables[0].Rows[i];
                list.Add(row["NameSpace"].ToString());
            }
            return list;
        }

    }
}
