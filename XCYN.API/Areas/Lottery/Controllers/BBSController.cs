using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.API.Areas.Lottery.Models.BBS;
using XCYN.API.Areas.Lottery.Models.Common;
using XCYN.API.Areas.Lottery.ORM.EF;

namespace XCYN.API.Areas.Lottery.Controllers
{
    public class BBSController : Controller
    {
        [HttpGet]
        public string GetIndex()
        {
            PlainModel result_model = new PlainModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            result_model.obj = dict;
            //string[] color = { "blue", "purple", "red", "orange", "green", "cyan", "rose" };
            try
            {
                using (MeetingSysEntities db = new MeetingSysEntities())
                {
                    var query = from a in db.article_category
                                where a.channel_id == 14
                                select new ArticleCategoryViewModel
                                {
                                    category_id = a.id,
                                    category_name = a.title
                                };

                    var list_category = query.ToList();

                    var query2 = from a in db.zcp_post
                                 join u in db.users on a.user_id equals u.id into temp
                                 from au in temp.DefaultIfEmpty()
                                 join r in db.zcp_post_recommend on a.id equals r.post_id into temp2
                                 from ar in temp2.DefaultIfEmpty()
                                 where a.status == 1
                                 select new IndexViewModel
                                 {
                                     id = a.id,
                                     channel_id = a.channel_id,
                                     category_id = a.category_id,
                                     title = a.title,
                                     add_time = a.add_time,
                                     comment_count = (from b in db.zcp_comment
                                                      where b.article_id == a.id
                                                      select b).Count(),
                                     click = a.click,
                                     great_count = (from c in db.zcp_post_great
                                                    where c.post_id == a.id
                                                    select c).Count(),
                                     title_type = a.title_type,
                                     is_top = a.is_top == null ? 0 : a.is_top,
                                     user_id = a.user_id,
                                     user_name = au.user_name,
                                     nick_name = au.nick_name,
                                     avatar = au.avatar,
                                     hotTopic_time = ar.hotTopic_time
                                 };

                    //热门推荐
                    var list_hot = query2.OrderByDescending(m => m.is_top).ThenByDescending(m => m.hotTopic_time).Take(6).ToList();

                    //最新更新
                    var list_top = query2.OrderByDescending(m => m.is_top).ThenByDescending(m => m.add_time).Take(10).ToList();

                    dict.Add("list_category", list_category);
                    dict.Add("list_hot", list_hot);
                    dict.Add("list_top", list_top);

                    result_model.result = 1;
                    return JsonConvert.SerializeObject(result_model);
                }
            }
            catch (Exception ex)
            {
                result_model.result = 0;
                result_model.message = ex.Message;
                return null;
            }
        }
    }
}