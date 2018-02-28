using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using XCYN.API.Areas.Lottery.Models.BBS;
using XCYN.API.Areas.Lottery.Models.Common;
using XCYN.API.Areas.Lottery.ORM.EF;
using XCYN.Common;

namespace XCYN.API.Areas.Lottery.Controllers
{
    public class BBSController : Controller
    {

        [HttpGet]
        public string GetIndex(int category_id = 0)
        {
            PlainModel result_model = new PlainModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            result_model.obj = dict;
            //string[] color = { "blue", "purple", "red", "orange", "green", "cyan", "rose" };
            try
            {
                using (MeetingSysEntities db = new MeetingSysEntities())
                {
                    //论坛分类
                    var query = from a in db.article_category
                                where a.channel_id == 14 
                                select new IndexCategoryViewModel
                                {
                                    category_id = a.id,
                                    category_name = a.title
                                };

                    var list_category = query.ToList();
                    list_category.Insert(0, new IndexCategoryViewModel
                    {
                        category_name = "全部"
                    });
                    for (int i = 0; i < list_category.Count; i++)
                    {
                        if(category_id == list_category[i].category_id)
                        {
                            list_category[i].is_active = 1;
                            break;
                        }
                    }
                    //if (category_id == m.category_id)
                    //{
                    //    m.is_active = 1;
                    //    return m;
                    //}
                    //else
                    //{
                    //    return m;
                    //}
                    //帖子列表
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
                                                      where b.article_id == a.id && b.state == 1
                                                      select b).Count(),
                                     click = a.click,
                                     great_count = (from c in db.zcp_post_great
                                                    where c.post_id == a.id && c.state == 1
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
                    var list_hot = query2.Where(i => category_id == 0 ? true : i.category_id == category_id * 100).OrderByDescending(m => m.is_top).ThenByDescending(m => m.hotTopic_time).Take(6).ToList();

                    //最新更新
                    var list_top = query2.Where(i => category_id == 0 ? true : i.category_id == category_id * 100).OrderByDescending(m => m.is_top).ThenByDescending(m => m.add_time).Take(10).ToList();

                    dict.Add("list_category", list_category);
                    dict.Add("list_hot", list_hot);
                    dict.Add("list_top", list_top);

                    result_model.result = JSONResult.success;

                    Newtonsoft.Json.Converters.IsoDateTimeConverter iso = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                    iso.DateTimeFormat = "yyyy-MM-dd HH:mm";
                    return JsonConvert.SerializeObject(result_model, iso);
                }
            }
            catch (Exception ex)
            {
                result_model.result = JSONResult.fail;
                result_model.message = ex.Message;
                return null;
            }
        }

        /*
        /// <summary>
        /// 论坛详情
        /// </summary>
        /// <param name="id"></param>
        public string GetDetail(int id)
        {
            PlainModel result_model = new PlainModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            using (MeetingSysEntities db = new MeetingSysEntities())
            {
                var query = from p in db.zcp_post
                            where p.id == id && p.status == 1
                            select p;
                if(query.Count() <= 0)
                {
                    result_model.result = 0;
                    result_model.message = "找不到该帖子或者该帖子被删除了!";
                    return JsonConvert.SerializeObject(result_model);
                }
                else
                {
                    //点击量+1
                    var model = query.FirstOrDefault();
                    int click = Utils.ObjToInt(model.click);
                    click = Interlocked.Increment(ref click);
                    db.SaveChanges();
                }

                //主贴
                var query2 = from a in db.zcp_post
                             join u in db.users on a.user_id equals u.id into temp
                             from au in temp.DefaultIfEmpty()
                             join r in db.zcp_post_recommend on a.id equals r.post_id into temp2
                             from ar in temp2.DefaultIfEmpty()
                             where a.status == 1 && a.id == id
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
                                 hotTopic_time = ar.hotTopic_time,
                                 content = a.content
                             };
                

                var post = query2.FirstOrDefault();

                //跟帖
                var query3 = from c in db.zcp_comment
                             join u in db.users on c.user_id equals u.id into temp
                             from uc in temp.DefaultIfEmpty()
                             where c.article_id == id && c.parent_id == 0 && c.state == 1
                             select new DetailViewModel
                             {
                                 id = c.id,
                                 post_id =  c.article_id,
                                 user_id = c.user_id,
                                 nick_name = uc.nick_name,
                                 avatar = uc.avatar,
                                 content = c.content,
                                 add_time = c.add_time
                             };
                var list_reply = query3.OrderByDescending(m => m.id).Skip(0).Take(5).ToList();

                dict.Add("post", post);
                dict.Add("list_reply", list_reply);
                result_model.obj = dict;
                result_model.result = 1;
                IsoDateTimeConverter isoDateTime = new IsoDateTimeConverter();
                isoDateTime.DateTimeFormat = "yyyy-MM-dd HH:mm";
                return JsonConvert.SerializeObject(result_model, isoDateTime);
            }
        }
        */
        /// <summary>
        /// 论坛详情
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public string GetDetail(int id, int pageIndex = 0, int pageSize = 10)
        {
            PlainModel result_model = new PlainModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            using (MeetingSysEntities db = new MeetingSysEntities())
            {
                var query = from p in db.zcp_post
                            where p.id == id && p.status == 1
                            select p;
                if (query.Count() <= 0)
                {
                    result_model.result = 0;
                    result_model.message = "找不到该帖子或者该帖子被删除了!";
                    return JsonConvert.SerializeObject(result_model);
                }
                else
                {
                    //点击量+1
                    var model = query.FirstOrDefault();
                    int click = Utils.ObjToInt(model.click);
                    click = Interlocked.Increment(ref click);
                    db.SaveChanges();
                }

                //主贴
                var query2 = from a in db.zcp_post
                             join u in db.users on a.user_id equals u.id into temp
                             from au in temp.DefaultIfEmpty()
                             join r in db.zcp_post_recommend on a.id equals r.post_id into temp2
                             from ar in temp2.DefaultIfEmpty()
                             where a.status == 1 && a.id == id
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
                                 hotTopic_time = ar.hotTopic_time,
                                 content = a.content
                             };


                var post = query2.FirstOrDefault();

                //跟帖
                var query3 = from c in db.zcp_comment
                             join u in db.users on c.user_id equals u.id into temp
                             from uc in temp.DefaultIfEmpty()
                             where c.article_id == id && c.parent_id == 0 && c.state == 1
                             select new DetailViewModel
                             {
                                 id = c.id,
                                 post_id = c.article_id,
                                 user_id = c.user_id,
                                 nick_name = uc.nick_name,
                                 avatar = uc.avatar,
                                 content = c.content,
                                 add_time = c.add_time
                             };
                var list_reply = query3.OrderByDescending(m => m.id).Skip(pageSize * pageIndex).Take(pageSize).ToList();

                dict.Add("post", post);
                dict.Add("list_reply", list_reply);
                result_model.obj = dict;
                result_model.result = JSONResult.success;
                IsoDateTimeConverter isoDateTime = new IsoDateTimeConverter();
                isoDateTime.DateTimeFormat = "yyyy-MM-dd HH:mm";
                return JsonConvert.SerializeObject(result_model, isoDateTime);
            }
        }

        /// <summary>
        /// 跟帖数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetReplyCount(int id)
        {
            PlainModel result_model = new PlainModel();
            using (MeetingSysEntities db = new MeetingSysEntities())
            {
                //跟帖
                var query = from c in db.zcp_comment
                            join u in db.users on c.user_id equals u.id into temp
                            from uc in temp.DefaultIfEmpty()
                            where c.article_id == id && c.parent_id == 0 && c.state == 1
                            select new DetailViewModel
                            {
                                id = c.id,
                                post_id = c.article_id,
                                user_id = c.user_id,
                                nick_name = uc.nick_name,
                                avatar = uc.avatar,
                                content = c.content,
                                add_time = c.add_time
                            };
                var count = query.Count();
                result_model.result = JSONResult.success;
                result_model.obj = count;
                return JsonConvert.SerializeObject(result_model);
            }

        }
    }
}