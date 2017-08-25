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
            string sort = context.Request["sort"];
            string order = context.Request["order"];
            context.Response.ContentType = "text/plain";
            //查询所有用户
            using (MeetingSysEntities db = new MeetingSysEntities())
            {
                var query = from a in db.users
                            where a.status != 0
                            select new {
                                id = a.id,
                                user_name = a.user_name,
                                reg_time = SqlFunctions.DateName("yyyy",a.reg_time) + "-" + SqlFunctions.DateName("mm", a.reg_time) + "-" + SqlFunctions.DatePart("dd", a.reg_time),
                            };
                int count = query.Count();
                query = query.OrderBy(m => m.id);
                if (order.ToLower().Equals("asc"))
                {
                    if(sort.ToLower().Equals("id"))
                    {
                        query = query.OrderBy(m => m.id);
                    }
                    else if(sort.ToLower().Equals("reg_time"))
                    {
                        query = query.OrderBy(m => m.reg_time);
                    }
                    else if (sort.ToLower().Equals("user_name"))
                    {
                        query = query.OrderBy(m => m.user_name);
                    }
                }
                else
                {
                    if (sort.ToLower().Equals("id"))
                    {
                        query = query.OrderByDescending(m => m.id);
                    }
                    else if (sort.ToLower().Equals("reg_time"))
                    {
                        query = query.OrderByDescending(m => m.reg_time);
                    }
                    else if (sort.ToLower().Equals("user_name"))
                    {
                        query = query.OrderByDescending(m => m.user_name);
                    }
                }
                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var list = query.ToList();
                //for (int i = 0; i < list.Count; i++)
                //{
                //    list[i].reg_time = 
                //}
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