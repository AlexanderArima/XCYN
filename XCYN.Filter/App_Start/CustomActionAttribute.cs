using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.Filter.App_Start
{
    public class CustomActionAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// 在动作方法之后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 在动作方法之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ActionName = filterContext.ActionDescriptor.ActionName;
            var ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //filterContext.Result = new HttpNotFoundResult();    //方法结果返回404页面
        }
    }
}