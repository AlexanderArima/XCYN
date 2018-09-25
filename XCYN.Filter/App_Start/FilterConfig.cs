using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.Filter.App_Start;

namespace Filters
{
    public class FilterConfig
    {
        /// <summary>
        /// 全局过滤器
        /// </summary>
        /// <param name="filter"></param>
        public static void RegisterGlobalFilter(GlobalFilterCollection filter)
        {
            filter.Add(new HandleErrorAttribute());
            filter.Add(new CustomActionAttribute());
        }
    }
}