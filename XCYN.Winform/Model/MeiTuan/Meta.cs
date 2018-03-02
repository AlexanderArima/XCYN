using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.Model.MeiTuan
{
    /// <summary>
    /// 美食元数据
    /// </summary>
    public class Meta
    {
        public int areaId { get; set; }
        public string attrs { get; set; }
        public int cateId { get; set; }
        public int ci { get; set; }
        public string cityName { get; set; }
        public string comFooter { get; set; }
        public string comHeader { get; set; }
        public List<object> crumbNav { get; set; }
        public string description { get; set; }

        /// <summary>
        /// 过滤器
        /// </summary>
        public Filters filters { get; set; }
        public string keyword { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string pageId { get; set; }

        /// <summary>
        /// 页面下标
        /// </summary>
        public int pn { get; set; }

        /// <summary>
        /// 美食列表
        /// </summary>
        public PoiList poiLists { get; set; }

        public List<object> prefer { get; set; }

        public string sort { get; set; }

        public string title { get; set; }

        public int userId { get; set; }

        public string uuid { get; set; }
    }
}
