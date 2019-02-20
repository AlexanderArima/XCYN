using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
