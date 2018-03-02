using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.Model.MeiTuan
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class Filters
    {
        public class Areas
        {
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public List<Areas> subAreas { get; set; }
        }

        /// <summary>
        /// 分类
        /// </summary>
        public class Cate
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        
        /// <summary>
        /// 用餐人数
        /// </summary>
        public class DinnerCountsAttr
        {
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        /// <summary>
        /// 排序规则
        /// </summary>
        public class SortTypesAttr
        {
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public List<Areas> areas { get; set; }

        public List<Cate> cates { get; set; }

        public List<DinnerCountsAttr> dinnerCountsAttr { get; set; }

        public List<SortTypesAttr> sortTypesAttr { get; set; }

        /// <summary>
        /// 批量导入数据
        /// </summary>
        public void Insert()
        {
            for (int i = 0; i < areas.Count; i++)
            {
                if(areas[i].subAreas.Count > 0)
                {
                    for (int j = 0; j < areas[i].subAreas.Count; j++)
                    {
                        var count = Common.Dapper.DapperHelper.Execute("INSERT INTO T_Areas(ID,Name,URL,P_ID) Values(@id,@name,@url,@p_id)",
                         new {
                             id = areas[i].subAreas[j].id,
                             name = areas[i].subAreas[j].id,
                             url = areas[i].subAreas[j].url,
                             p_id = areas[i].id
                         });
                    }
                    
                }
                
            }
            var c = Common.Dapper.DapperHelper.Execute("INSERT INTO T_Areas(ID,Name,URL,P_ID) Values(@id,@name,@url,0)",
                   areas);

        }
        
        public int Delete()
        {
             return Common.Dapper.DapperHelper.Execute(@" TRUNCATE TABLE T_Areas
                                                          TRUNCATE TABLE T_Cate
                                                          TRUNCATE TABLE T_DinnerCountsAttr
                                                          TRUNCATE TABLE T_SortTypesAttr");
        }
    }
}
