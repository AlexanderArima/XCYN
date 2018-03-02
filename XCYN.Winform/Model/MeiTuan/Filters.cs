using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCYN.Winform.Model.MeiTuan
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class Filters
    {
        
        public List<Areas> areas { get; set; }

        public List<Cate> cates { get; set; }

        public List<DinnerCountsAttr> dinnerCountsAttr { get; set; }

        public List<SortTypesAttr> sortTypesAttr { get; set; }
        
        /// <summary>
        /// 批量导入数据
        /// </summary>
        public T Insert<T>()
        {
            var typeName = typeof(T).Name;
            if (typeName.Equals(typeof(int).Name))
            {
                int count = Areas.Insert(areas);
                int count2 = Cate.Insert(cates);
                int count3 = DinnerCountsAttr.Insert(dinnerCountsAttr);
                int count4 = SortTypesAttr.Insert(sortTypesAttr);
                return (T)(object)(count + count2 + count3 + count4);
            }
            else if (typeName.Equals(typeof(string).Name))
            {
                int count = Areas.Insert(areas);
                int count2 = Cate.Insert(cates);
                int count3 = DinnerCountsAttr.Insert(dinnerCountsAttr);
                int count4 = SortTypesAttr.Insert(sortTypesAttr);
                return (T)(object)$"导入地区:{count}条，类别:{count2}，用餐人数:{count3}，排序规则:{count4}";// count + count2 + count3 + count4;
            }
            else
                return default(T);
        }

        public int Delete()
        {
             return Common.Dapper.DapperHelper.Execute(@" TRUNCATE TABLE T_Areas
                                                          TRUNCATE TABLE T_Cate
                                                          TRUNCATE TABLE T_DinnerCountsAttr
                                                          TRUNCATE TABLE T_SortTypesAttr");
        }
    }

    public class Areas
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public List<Areas> subAreas { get; set; }

        /// <summary>
        /// 批量导入数据
        /// </summary>
        public static int Insert(List<Areas> areas)
        {
            int sum = 0;
            int is_exist = 0;
            for (int i = 0; i < areas.Count; i++)
            {
                if (areas[i].subAreas.Count > 0)
                {
                    for (int j = 0; j < areas[i].subAreas.Count; j++)
                    {
                        if (j == 0) continue;
                        is_exist = Common.Dapper.DapperHelper.ExecuteScalar("SELECT COUNT(*) FROM T_Areas WHERE ID = @ID", new { id = areas[i].subAreas[j].id });
                        if (is_exist <= 0)
                        {
                            var count = Common.Dapper.DapperHelper.Execute("INSERT INTO T_Areas(ID,Name,URL,P_ID) Values(@id,@name,@url,@p_id)",
                             new
                             {
                                 id = areas[i].subAreas[j].id,
                                 name = areas[i].subAreas[j].name,
                                 url = areas[i].subAreas[j].url,
                                 p_id = areas[i].id
                             });
                            Interlocked.Add(ref sum, count);
                        }
                    }
                }
                is_exist = Common.Dapper.DapperHelper.ExecuteScalar("SELECT COUNT(*) AS Count FROM T_Areas WHERE ID = @ID", new { id = areas[i].id });
                if (is_exist <= 0)
                {
                    var c = Common.Dapper.DapperHelper.Execute("INSERT INTO T_Areas(ID,Name,URL,P_ID) Values(@id,@name,@url,@p_id)",
                       new
                       {
                           id = areas[i].id,
                           name = areas[i].name,
                           url = areas[i].url,
                           p_id = 0
                       });
                    Interlocked.Add(ref sum, c);
                }

            }
            return sum;

        }
    }

    /// <summary>
    /// 分类
    /// </summary>
    public class Cate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        /// <summary>
        /// 批量导入数据
        /// </summary>
        public static int Insert(List<Cate> list)
        {
            int sum = 0;
            int is_exist = 0;
            for (int i = 0; i < list.Count; i++)
            {
                is_exist = Common.Dapper.DapperHelper.ExecuteScalar("SELECT COUNT(*) AS Count FROM T_Cate WHERE ID = @id", new { id = list[i].id });
                if (is_exist <= 0)
                {
                    var c = Common.Dapper.DapperHelper.Execute("INSERT INTO T_Cate(ID,Name,URL) Values(@id,@name,@url)",
                       new
                       {
                           id = list[i].id,
                           name = list[i].name,
                           url = list[i].url
                       });
                    Interlocked.Add(ref sum, c);
                }
            }
            return sum;
        }
    }

    /// <summary>
    /// 用餐人数
    /// </summary>
    public class DinnerCountsAttr
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        /// <summary>
        /// 批量导入数据
        /// </summary>
        public static int Insert(List<DinnerCountsAttr> list)
        {
            int sum = 0;
            int is_exist = 0;
            for (int i = 0; i < list.Count; i++)
            {
                is_exist = Common.Dapper.DapperHelper.ExecuteScalar("SELECT COUNT(*) AS Count FROM T_DinnerCountsAttr WHERE ID = @id", new { id = list[i].id });
                if (is_exist <= 0)
                {
                    var c = Common.Dapper.DapperHelper.Execute("INSERT INTO T_DinnerCountsAttr(ID,Name,URL) Values(@id,@name,@url)",
                       new
                       {
                           id = list[i].id,
                           name = list[i].name,
                           url = list[i].url
                       });
                    Interlocked.Add(ref sum, c);
                }
            }
            return sum;
        }
    }

    /// <summary>
    /// 排序规则
    /// </summary>
    public class SortTypesAttr
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        /// <summary>
        /// 批量导入数据
        /// </summary>
        public static int Insert(List<SortTypesAttr> list)
        {
            int sum = 0;
            int is_exist = 0;
            for (int i = 0; i < list.Count; i++)
            {
                is_exist = Common.Dapper.DapperHelper.ExecuteScalar("SELECT COUNT(*) AS Count FROM T_SortTypesAttr WHERE ID = @id", new { id = list[i].id });
                if (is_exist <= 0)
                {
                    var c = Common.Dapper.DapperHelper.Execute("INSERT INTO T_SortTypesAttr(ID,Name,URL) Values(@id,@name,@url)",
                       new
                       {
                           id = list[i].id,
                           name = list[i].name,
                           url = list[i].url
                       });
                    Interlocked.Add(ref sum, c);
                }
            }
            return sum;
        }
    }
}
