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
using System.Data.SqlClient;

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
        public int Insert()
        {
            int count = Areas.Insert(areas);
            int count2 = Cate.Insert(cates);
            int count3 = DinnerCountsAttr.Insert(dinnerCountsAttr);
            int count4 = SortTypesAttr.Insert(sortTypesAttr);
            return count + count2 + count3 + count4;
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

    /// <summary>
    /// 地区
    /// </summary>
    public class Areas
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int P_ID { get; set; }
        public bool State { get; set; }
        public DateTime AddTime { get; set; }
        public List<Areas> subAreas { get; set; }

        public static int Insert(List<Areas> areas)
        {
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                var query = from a in db.T_Areas
                            where a.State == true
                            select a;
                List<Areas> list = new List<Areas>();
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
                var list_inserted = list.FindAll(m => list_id_insert.Contains(m.ID)).Select(i => {
                    return new T_Areas
                    {
                        ID = i.ID,
                        Name = i.Name,
                        URL = i.URL,
                        P_ID = i.P_ID,
                        State = i.State,
                        AddTime = i.AddTime,
                    };
                });
                foreach (var item in list_inserted)
                {
                    db.Database.ExecuteSqlCommand("INSERT INTO T_Areas(ID,Name,URL,P_ID,State,AddTime) VALUES(@ID,@Name,@URL,@P_ID,@State,@AddTime)",
                        new SqlParameter("@ID", item.ID),
                        new SqlParameter("@Name", item.Name),
                        new SqlParameter("@URL", item.URL),
                        new SqlParameter("@P_ID", item.P_ID),
                        new SqlParameter("@State", item.State),
                        new SqlParameter("@AddTime", item.AddTime));
                }
                db.SaveChanges();
                return list_inserted.Count();
            }
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
        public bool State { get; set; }
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 导入数据
        /// </summary>
        public static int Insert(List<Cate> cate)
        {
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                var query = from a in db.T_Cate
                            where a.State == true
                            select a;
                List<Cate> list = new List<Cate>();
                list.AddRange(cate);
                List<int> list_id_source = query.Select(m => m.ID).ToList();
                List<int> list_id_target = list.Select(m => m.id).ToList();
                //找出需要插入的值
                List<int> list_id_insert = list_id_target.Except(list_id_source).Distinct().OrderBy(m => m).ToList();
                var list_inserted = list.FindAll(m => list_id_insert.Contains(m.id)).Select(i => {
                    return new Cate
                    {
                        id = i.id,
                        name = i.name,
                        url = string.Empty,
                        State = true,
                        AddTime = DateTime.Now
                    };
                });
                foreach (var item in list_inserted)
                {
                    db.Database.ExecuteSqlCommand("INSERT INTO T_Cate(ID,Name,URL,State,AddTime) VALUES(@ID,@Name,@URL,@State,@AddTime)",
                        new SqlParameter("@ID", item.id),
                        new SqlParameter("@Name", item.name),
                        new SqlParameter("@URL", item.url),
                        new SqlParameter("@State", item.State),
                        new SqlParameter("@AddTime", item.AddTime));
                }
                db.SaveChanges();
                return list_inserted.Count();
            }
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
        public bool State { get; set; }
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 导入数据
        /// </summary>
        public static int Insert(List<DinnerCountsAttr> cate)
        {
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                var query = from a in db.T_DinnerCountsAttr
                            where a.State == true
                            select a;
                List<DinnerCountsAttr> list = new List<DinnerCountsAttr>();
                list.AddRange(cate);
                List<int> list_id_source = query.Select(m => m.ID).ToList();
                List<int> list_id_target = list.Select(m => m.id).ToList();
                //找出需要插入的值
                List<int> list_id_insert = list_id_target.Except(list_id_source).Distinct().OrderBy(m => m).ToList();
                var list_inserted = list.FindAll(m => list_id_insert.Contains(m.id)).Select(i => {
                    return new DinnerCountsAttr
                    {
                        id = i.id,
                        name = i.name,
                        url = string.Empty,
                        State = true,
                        AddTime = DateTime.Now
                    };
                });
                foreach (var item in list_inserted)
                {
                    db.Database.ExecuteSqlCommand("INSERT INTO T_DinnerCountsAttr(ID,Name,URL,State,AddTime) VALUES(@ID,@Name,@URL,@State,@AddTime)",
                        new SqlParameter("@ID", item.id),
                        new SqlParameter("@Name", item.name),
                        new SqlParameter("@URL", item.url),
                        new SqlParameter("@State", item.State),
                        new SqlParameter("@AddTime", item.AddTime));
                }
                db.SaveChanges();
                return list_inserted.Count();
            }
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
        public bool State { get; set; }
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 导入数据
        /// </summary>
        public static int Insert(List<SortTypesAttr> cate)
        {
            using (MeiTuanEntities db = new MeiTuanEntities())
            {
                var query = from a in db.T_SortTypesAttr
                            where a.State == true
                            select a;
                List<SortTypesAttr> list = new List<SortTypesAttr>();
                list.AddRange(cate);
                List<int> list_id_source = query.Select(m => m.ID).ToList();
                List<int> list_id_target = list.Select(m => m.id).ToList();
                //找出需要插入的值
                List<int> list_id_insert = list_id_target.Except(list_id_source).Distinct().OrderBy(m => m).ToList();
                var list_inserted = list.FindAll(m => list_id_insert.Contains(m.id)).Select(i => {
                    return new SortTypesAttr
                    {
                        id = i.id,
                        name = i.name,
                        url = string.Empty,
                        State = true,
                        AddTime = DateTime.Now
                    };
                });
                foreach (var item in list_inserted)
                {
                    db.Database.ExecuteSqlCommand("INSERT INTO T_SortTypesAttr(ID,Name,URL,State,AddTime) VALUES(@ID,@Name,@URL,@State,@AddTime)",
                        new SqlParameter("@ID", item.id),
                        new SqlParameter("@Name", item.name),
                        new SqlParameter("@URL", item.url),
                        new SqlParameter("@State", item.State),
                        new SqlParameter("@AddTime", item.AddTime));
                }
                db.SaveChanges();
                return list_inserted.Count();
            }
        }
    }
}
