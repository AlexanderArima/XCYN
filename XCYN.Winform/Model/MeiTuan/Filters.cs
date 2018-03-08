using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCYN.Winform.Model.MeiTuan.EF;
using Dapper;
using XCYN.Common.Dapper;

namespace XCYN.Winform.Model.MeiTuan
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class Filters
    {
        
        public List<T_Areas> areas { get; set; }

        public List<T_Areas> cates { get; set; }

        public List<T_Areas> dinnerCountsAttr { get; set; }

        public List<T_Areas> sortTypesAttr { get; set; }
        
        /// <summary>
        /// 批量导入数据
        /// </summary>
        public T Insert<T>()
        {
            var typeName = typeof(T).Name;
            if (typeName.Equals(typeof(int).Name))
            {
                int count = 0;
                using (MeiTuanEntities db = new MeiTuanEntities())
                {
                    var query = from a in db.T_Areas
                                where a.State == true
                                select a;
                    List<T_Areas> list = new List<T_Areas>();
                    //默认值
                    for (int i = 0; i < areas.Count; i++)
                    {
                        areas[i].State = true;
                        areas[i].P_ID = 0;
                        areas[i].AddTime = DateTime.Now;
                        var subAreas = areas[i].subAreas;
                        for (int j = 0; j < subAreas.Count; j++)
                        {
                            subAreas[j].State = true;
                            subAreas[j].P_ID = areas[i].ID;
                            subAreas[j].AddTime = DateTime.Now;
                        }
                        subAreas.Remove(subAreas.Find(m => m.Name.Equals("全部")));
                        list.AddRange(subAreas);
                        //db.T_Areas.AddRange(subAreas);
                    }
                    list.AddRange(areas);
                    //var temp_areas = db.T_Areas.AddRange(areas);
                    List<int> list_id_source = query.Select(m => m.ID).ToList();
                    List<int> list_id_target = list.Select(m => m.ID).ToList();
                    //找出需要插入的值
                    List<int> list_id_insert = list_id_target.Except(list_id_source).Distinct().OrderBy(m => m).ToList();
                    var list_inserted = list.FindAll(m => list_id_insert.Contains(m.ID));
                    db.T_Areas.AddRange(list_inserted);
                    db.SaveChanges();
                }
                return (T)(object)(count);
            }
            else if (typeName.Equals(typeof(string).Name))
            {
                return (T)(object)$"";
                //return (T)(object)$"导入地区:{count}条，类别:{count2}，用餐人数:{count3}，排序规则:{count4}";// count + count2 + count3 + count4;
            }
            else
                return default(T);
        }

        public int Delete()
        {
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                return db.Database.ExecuteSqlCommand(@" TRUNCATE TABLE T_Areas
                                TRUNCATE TABLE T_Cate
                                TRUNCATE TABLE T_DinnerCountsAttr
                                TRUNCATE TABLE T_SortTypesAttr");
            }
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
        public static int Insert(List<T_Areas> areas)
        {
            /*
            int sum = 0;
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                db.T_Areas.AddRange(areas);
                for (int i = 0; i < areas.Count; i++)
                {


                    if (areas[i].subAreas.Count > 0)
                    {
                        for (int j = 0; j < areas[i].subAreas.Count; j++)
                        {
                            if (j == 0) continue;
                            var query2 = from a in db.T_Areas
                                        where a.ID == areas[i].subAreas[j].id
                                        select a;
                            if (query2.Count() <= 0)
                            {
                                T_Areas are = new T_Areas()
                                {
                                    ID = areas[i].subAreas[j].id,
                                    Name = areas[i].subAreas[j].name,
                                    URL = areas[i].subAreas[j].url,
                                    P_ID = areas[i].id
                                };
                                db.T_Areas.Add(are);//插入二级目录
                                Interlocked.Add(ref sum, 1);
                            }
                        }
                    }

                    var query = from a in db.T_Areas
                                where a.ID == areas[i].id
                                select a;
                    if (query.Count() <= 0)
                    {
                        T_Areas are = new T_Areas()
                        {
                            ID = areas[i].id,
                            Name = areas[i].name,
                            URL = areas[i].url,
                            P_ID = 0
                        };
                        db.T_Areas.Add(are);//插入一级目录
                        Interlocked.Add(ref sum, 1);
                    }
                }
            }
            return sum;
              */
            return 0;
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
            using(MeiTuanEntities db = new MeiTuanEntities())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var query = from a in db.T_Cate
                                where a.ID == list[i].id
                                select a;
                    if(query.Count() <= 0)
                    {
                        var model = new T_Cate()
                        {
                            ID = list[i].id,
                            Name = list[i].name,
                            URL = list[i].url
                        };
                        db.T_Cate.Add(model);
                        Interlocked.Add(ref sum, 1);
                    }
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
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var query = from a in db.T_DinnerCountsAttr
                                where a.ID == list[i].id
                                select a;
                    if (query.Count() <= 0)
                    {
                        var model = new T_DinnerCountsAttr()
                        {
                            ID = list[i].id,
                            Name = list[i].name,
                            URL = list[i].url
                        };
                        db.T_DinnerCountsAttr.Add(model);
                        Interlocked.Add(ref sum, 1);
                    }
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
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var query = from a in db.T_SortTypesAttr
                                where a.ID == list[i].id
                                select a;
                    if (query.Count() <= 0)
                    {
                        var model = new T_SortTypesAttr()
                        {
                            ID = list[i].id,
                            Name = list[i].name,
                            URL = list[i].url
                        };
                        db.T_SortTypesAttr.Add(model);
                        Interlocked.Add(ref sum, 1);
                    }
                }
            }
            return sum;
        }
    }
}
