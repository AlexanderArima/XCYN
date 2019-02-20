using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common.Access;

namespace XCYN.Winform.Quartz.Model
{
    public class T_ServiceList
    {
        public T_ServiceList()
        { }
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ServiceName,AssemblyName,NameSpace,ClassName,MethodName,IsDelete ");
            strSql.Append(" FROM [T_ServiceList] ");
            strSql.Append(" where IsDelete = False");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }
            var ds = DbHelperOleDb.Query(strSql.ToString());
            var cols = ds.Tables[0].Columns;
            cols["ServiceName"].Caption = "服务名";
            cols["AssemblyName"].Caption = "程序集";
            cols["NameSpace"].Caption = "命名空间";
            cols["ClassName"].Caption = "类名";
            cols["MethodName"].Caption = "方法名";
            return ds; 
        }

    }
}
