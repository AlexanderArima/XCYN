using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.ViewEngine.Models
{
    public class DebugDataView : IView
    {
        /// <summary>
        /// 渲染视图
        /// </summary>
        /// <param name="viewContext">取出服务器的返回内容</param>
        /// <param name="writer">向客户端写入响应</param>
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            Write(writer,"---Routing Data---");
            foreach (var item in viewContext.RouteData.Values.Keys)
            {
                Write(writer,string.Format("Key:{0},Value:{1}", item, viewContext.RouteData.Values[item]));
            }
            Write(writer,"---View Data---");
            foreach (var item in viewContext.ViewData.Keys)
            {
                Write(writer,string.Format("Key:{0},Value:{1}", item, viewContext.ViewData[item]));
            }
        }

        private void Write(TextWriter writer,string template,params object[] values)
        {
            writer.Write(string.Format("<p>"+template+"</p>", values));
        }
    }
}