using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.ViewEngine.Models
{
    public class DebugDataViewEngine : IViewEngine
    {
        /// <summary>
        /// 查找分部视图
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="partialViewName">分部视图名称</param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return new ViewEngineResult(new string[] { "No View" });
        }

        /// <summary>
        /// 查找视图
        /// </summary>
        /// <param name="controllerContext">控制器内容</param>
        /// <param name="viewName">视图名称</param>
        /// <param name="masterName">模板名称</param>
        /// <param name="useCache">是否使用缓存</param>
        /// <returns></returns>
        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if(viewName == "DebugData")
            {
                return new ViewEngineResult(new DebugDataView(), this);
            }
            else
            {
                return new ViewEngineResult(new string[] { "No View"});
            }
        }

        /// <summary>
        /// 释放视图
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="view"></param>
        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            
        }
    }
}