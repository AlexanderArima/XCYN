using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
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
            //获取查询参数
            string user_name = !string.IsNullOrEmpty(context.Request["user_name"]) ? context.Request["user_name"] : string.Empty;
            DateTime reg_from = !string.IsNullOrEmpty(context.Request["reg_from"]) ? Convert.ToDateTime(context.Request["reg_from"]) : new DateTime(1970,1,1);
            DateTime reg_to = !string.IsNullOrEmpty(context.Request["reg_to"]) ? Convert.ToDateTime(context.Request["reg_to"]) : new DateTime(1970, 1, 1);
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
                            select new UserViewModel
                            {
                                id = a.id,
                                user_name = a.user_name,
                                //reg_time = SqlFunctions.DateName("yyyy",a.reg_time) + "-" + SqlFunctions.DateName("mm", a.reg_time) + "-" + SqlFunctions.DatePart("dd", a.reg_time)),
                                reg_time = a.reg_time.Value
                            };
                if(user_name.Length > 0)
                {
                    query = query.Where(i => i.user_name.Contains(user_name));
                }
                if (reg_from.Year != 1970)
                {
                    query = query.Where(i => i.reg_time > reg_from);
                }
                if (reg_to.Year != 1970)
                {
                    query = query.Where(i => i.reg_time < reg_to);
                }
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
                //声明页尾
                //ArrayList list_footer = new ArrayList();
                //Dictionary<string, string> dict = new Dictionary<string, string>();
                //dict.Add("id", "1");
                //dict.Add("reg_time", "2");
                //dict.Add("user_name", "3");
                //list_footer.Add(dict);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].reg_time = Convert.ToDateTime(list[i].reg_time.ToString("yyyy-MM-dd"));
                }
                DataGridViewModel viewModel = new DataGridViewModel
                {
                    total = count,
                    rows = list,
                };
                context.Response.Write(JsonConvert.SerializeObject(viewModel, new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd",
                }));
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