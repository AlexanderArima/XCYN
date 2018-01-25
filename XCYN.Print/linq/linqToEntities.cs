using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Linq.Model;

namespace XCYN.Print.linq
{
    class linqToEntities
    {
        static void Fun1()
        {
            using (MeetingSysEntities entity = new MeetingSysEntities())
            {
                //取出所有的公式，带上类别名称
                var query = from f in entity.zcp_formula
                            join c in entity.zcp_formula_category
                            on f.category_id equals c.id
                            join r in entity.zcp_formula_rank
                            on f.grade_id equals r.id
                            select new {
                                formula_name = f.formula_name,
                                category_name = c.name,
                                grade_name = r.name
                            };

                //列出所有公式的类型，并显示数量
                var query2 = from a in
                             (
                             from b in entity.zcp_formula
                             group b by b.category_id into c
                             select new {
                                 category_id = c.Key,
                                 c_count = c.Count()
                             }
                             )
                             join d in entity.zcp_formula_category
                             on a.category_id equals d.id
                             select new {
                                 a.category_id,
                                 d.name,
                                 a.c_count
                             };

                //列出用户111111的所购买的公式
                var query3 = from b in
                            (from a in entity.users
                             where a.user_name.Equals("111111")
                             select new { user_id = a.id })
                             join c in entity.zcp_formu_order
                             on b.user_id equals c.user_id
                             join d in entity.zcp_formula
                             on c.formu_id equals d.id
                             select new {
                                 formula_name = d.formula_name,
                                 point = c.point
                             };
                foreach (var item in query3.ToList())
                {
                    Console.WriteLine(item);
                }
                //列出用户111111购买公式的总价
                var query4 = query3.Sum(i => i.point);
                Console.WriteLine(query4);
            }
        }

    }
}
