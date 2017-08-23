using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using XCYN.EasyUI.model;
using XCYN.EasyUI.ViewModel;

namespace XCYN.EasyUI.ashx
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request["page"]);
            int pageSize = Convert.ToInt32(context.Request["rows"]);
            context.Response.ContentType = "text/plain";
            //查询所有用户
            using (MeetingSysEntities db = new MeetingSysEntities())
            {
                var query = from a in db.users
                            where a.status != 0
                            select a;
                int count = query.Count();
                //var list = query.OrderBy(m => m.id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                query = query.OrderByDescending(m => m.user_name).Skip((page - 1) * pageSize).Take(pageSize);
                var list = query.ToList();
                DataGridViewModel viewModel = new DataGridViewModel
                {
                    total = count,
                    rows = list,
                };
                context.Response.Write(JsonConvert.SerializeObject(viewModel));
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}