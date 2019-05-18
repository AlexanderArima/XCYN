using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Winform.Quartz.Model;

namespace XCYN.Winform.Quartz.ViewModel
{
    public class AddTriggerFormViewModel_Add
    {
        public int ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int RepeatTime { get; set; }

        public int RepeatInterval { get; set; }

        public string ServiceName { get; set; }

        public int Add(AddTriggerFormViewModel_Add model)
        {
            T_ServiceList serviceList = new T_ServiceList();
            var id = serviceList.GetID(model.ServiceName);
            T_SimpleTrigger trigger = new T_SimpleTrigger()
            {
                ID = model.ID,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                RepeatTime = model.RepeatTime,
                RepeatInterval = model.RepeatInterval,
                SID = id
            };
            return trigger.Add(trigger);
        }


    }
    public class AddTriggerFormViewModel_GetList
    {
        public AddTriggerFormViewModel_GetList()
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

        public DataSet GetList()
        {
            T_ServiceList service = new T_ServiceList();
            return service.GetList("");
        }
    }

}
