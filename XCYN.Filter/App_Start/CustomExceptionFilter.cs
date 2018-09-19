using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.Filter.App_Start
{
    /// <summary>
    /// 自定义异常处理类
    /// </summary>
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var values = filterContext.RouteData.Values;
            var controllerName = values["controller"].ToString();
            var actionName = values["action"].ToString();
            filterContext.Result = new RedirectResult("~/Content/ErrorPage.html");
            filterContext.ExceptionHandled = true;  //将它设置为true，系统就不会出现“黄色屏幕”了
        }
    }
}