using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCYN.EasyUI.model;
using XCYN.EasyUI.ViewModel;

namespace XCYN.EasyUI.ashx
{
    /// <summary>
    /// TreeHandler 的摘要说明
    /// </summary>
    public class TreeHandler : IHttpHandler
    {

        List<NavViewModel> _list = new List<NavViewModel>();

        public void ProcessRequest(HttpContext context)
        {
            using(MeetingSysEntities db = new MeetingSysEntities())
            {
                var query = from a in db.navigations
                            select a;
                var list = query.ToList();
                _list = RecurNav(list,0);
            }
            var json = JsonConvert.SerializeObject(_list);
            context.Response.Write(json);
            context.Response.ContentType = "application/json";
            context.Response.End();
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="p_id">父级id</param>
        private List<NavViewModel> RecurNav(List<model.navigation> list,int? p_id)
        {
            var list_temp = list.FindAll((i) => { return i.parent_id == p_id; });//筛选父id下的所有子节点
            List<NavViewModel> list_child_temp = new List<NavViewModel>();
            for (int i = 0; i < list_temp.Count; i++)
            {
                var is_child = list.Exists(n => n.id == list_temp[i].id);
                if(is_child)
                {
                    //存在子节点
                    var list_child = RecurNav(list, list_temp[i].id);
                    list_child_temp.Add(new NavViewModel
                    {
                        id = list_temp[i].id,
                        text = list_temp[i].title,
                        children = list_child,
                        state = TREE_STATE.closed.ToString(),
                    });
                }
                else
                {
                    list_child_temp.Add(new NavViewModel
                    {
                        id = list_temp[i].id,
                        text = list_temp[i].title,
                    });
                }
            }
            return list_child_temp;

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