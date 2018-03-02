using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.Model.MeiTuan
{
    public class PoiInfo
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int allCommentNum { get; set; }

        /// <summary>
        /// 平均消费
        /// </summary>
        public int avgPrice { get; set; }

        /// <summary>
        /// 平均得分
        /// </summary>
        public float avgScore { get; set; }

        public List<object> dealList { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string frontImg { get; set; }

        public int poiId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
    }
}
