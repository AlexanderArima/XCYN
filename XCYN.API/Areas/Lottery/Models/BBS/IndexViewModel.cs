using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.API.Areas.Lottery.Models.BBS
{
    public class IndexViewModel
    {

        /// <summary>
        /// 帖子ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int category_id { get; set; }

        /// <summary>
        /// 频道ID
        /// </summary>
        public int channel_id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? add_time { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int comment_count { get; set; }

        /// <summary>
        /// 点击量
        /// </summary>
        public int? click { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int great_count { get; set; }

        /// <summary>
        /// 文章类型(原创or转载)
        /// </summary>
        public int? title_type { get; set; }

        /// <summary>
        /// 置顶
        /// </summary>
        public byte? is_top { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int? user_id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nick_name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }

        public DateTime? hotTopic_time { get; set; }
    }
}