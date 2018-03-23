using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.linq.DataLib;

namespace XCYN.Print.linq
{
    /// <summary>
    /// 该类接着DelayQuery类
    /// </summary>
    public class GroupByQuery
    {

        /// <summary>
        /// 分组
        /// </summary>
        public void Fun1()
        {
            Console.WriteLine("---------Linq查询---------");
            var query = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        orderby g.Count() descending, g.Key
                        where g.Count() >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = g.Count()
                        };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Country , -10} {item.Count}");
            }
            Console.WriteLine("---------拓展方法---------");
            var query2 = Formula1.GetChampions().
                        GroupBy(r => r.Country).
                        OrderByDescending(r =>
                        {
                            Console.WriteLine(r.Key);
                            return r.Count();
                        }).
                        ThenBy(r => r.Key).
                        Where(r => r.Count() >= 2).
                        Select(r => new {
                            Country = r.Key,
                            Count = r.Count()
                        });
            foreach (var item in query2)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }
            Console.Read();
        }

        /// <summary>
        /// Linq查询中的变量
        /// </summary>
        public void Fun2()
        {
            Console.WriteLine("---------Linq查询---------");
            var query = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        let count = g.Count()
                        orderby count descending, g.Key
                        where count >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = count
                        };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }
            Console.WriteLine("---------拓展方法---------");
            var query2 = Formula1.GetChampions().
                        GroupBy(r => r.Country).
                        Select(g => new { Group = g, Count = g.Count() }).
                        OrderByDescending(r => r.Count).
                        ThenBy(r => r.Group.Key).
                        Where(r => r.Count >= 2).
                        Select(r => new {
                            Country = r.Group.Key,
                            Count = r.Count,
                           
                        });
            foreach (var item in query2)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }
            Console.Read();
        }

        /// <summary>
        /// 对嵌套的对象分组
        /// </summary>
        public void Fun3()
        {
            var query = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        let count = g.Count()
                        orderby count descending, g.Key
                        where count >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = count,
                            Racers = from r1 in g
                                     orderby r1.LastName
                                     select r1.FirstName + " " + r1.LastName
                        };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
                foreach (var name in item.Racers)
                {
                    Console.Write(name + ",");
                }
                Console.WriteLine();
            }
            Console.Read();
        }

        /// <summary>
        /// 内连接
        /// </summary>
        public void Fun4()
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from r in Formula1.GetConstructorChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.Name
                         };

            var query = from r in racers
                        join t in teams on r.Year equals t.Year
                        select new
                        {
                            Year = r.Year,
                            Racer = r.Name,
                            Team = t.Name
                        };
            foreach (var item in query)
            {
                Console.WriteLine(item.Racer + "--" + item.Team + "--" + item.Year);
            }
            Console.Read();
        }
    }
}
