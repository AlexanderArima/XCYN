using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.Model.Report
{
    public class ReportConfig
    {
        private static ReportConfig report = new ReportConfig();
        

        private ReportConfig()
        {

        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static ReportConfig GetInstance()
        {
            return report;
        }

        public SortedList<string, string> GetList()
        {
            SortedList<string, string> dict_result = new SortedList<string, string>();
            IDictionary dict = ConfigurationManager.GetSection("Report") as IDictionary;
            foreach (var item in dict.Keys)
            {
                dict_result[item.ToString()] = dict[item].ToString();
            }
            return dict_result;
        }
    }
}
