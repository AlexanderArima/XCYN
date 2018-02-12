using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.API.Areas.Lottery.Models.BBS
{
    public class DetailViewModel
    {
        /// <summary>
        /// 回帖id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 主贴id
        /// </summary>
        public int? post_id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int? user_id { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nick_name { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 回帖内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 回帖时间
        /// </summary>
        public DateTime? add_time { get; set; }
    }
}