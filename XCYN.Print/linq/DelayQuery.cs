using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Linq.Model;
using XCYN.Print.linq.DataLib;

namespace XCYN.Print.linq
{
    /// <summary>
    /// Linq延迟查询
    /// </summary>
    public class DelayQuery
    {
        #region 在运行期间定义查询表达式时，查询不会运行，查询会再迭代数据项时运行
        
        public void Fun1()
        {
            List<string> list = new List<string>();
            list.Add("Add");
            list.Add("Back");
            list.Add("Can");
            var query = from a in list
                         where a.StartsWith("A")
                         select a;
            Console.WriteLine("第一次打印");
            for (int i = 0; i < query.ToList().Count; i++)
            {
                Console.WriteLine(query.ToList()[i]);
            }

            list.Add("Ada");
            Console.WriteLine("第二次打印");
            for (int i = 0; i < query.ToList().Count; i++)
            {
                Console.WriteLine(query.ToList()[i]);
            }
            Console.Read();
        }

        public void Fun2()
        {
            List<string> list = new List<string>();
            list.Add("Add");
            list.Add("Back");
            list.Add("Can");
            var query = (from a in list
                        where a.StartsWith("A")
                        select a).ToList();
            Console.WriteLine("第一次打印");
            for (int i = 0; i < query.Count; i++)
            {
                Console.WriteLine(query.ToList()[i]);
            }

            list.Add("Ada");
            Console.WriteLine("第二次打印");
            for (int i = 0; i < query.Count; i++)
            {
                Console.WriteLine(query.ToList()[i]);
            }
            Console.Read();
        }

        #endregion

        /// <summary>
        /// Where筛选条件
        /// </summary>
        public void Fun3()
        {
            //并不是所有的查询都可以用Linq查询语法完成。也不是所有的拓展方法都映射到Linq查询子句上。高级查询需要使用拓展方法。
            var racers = Formula1.GetChampions().Where((r,index) =>
            {
                return r.Wins > 15 && (r.Country == "USA" || r.Country == "UK");

            }).Select(r => r);
            Console.WriteLine("使用拓展方法");
            foreach (var item in racers)
            {
                Console.WriteLine(item.ToString("A"));
            }
            Console.WriteLine("------------------------");
            var query = from r in Formula1.GetChampions()
                        where r.Wins > 15 && (r.Country == "USA" || r.Country == "UK")
                        select r;
            Console.WriteLine("使用Linq查询");
            foreach (var item in query)
            {
                Console.WriteLine(item.ToString("A"));
            }
            Console.Read();
        }

        /// <summary>
        /// 用索引筛选
        /// </summary>
        public void Fun4()
        {
            var racers = Formula1.GetChampions().Where((r,index) => r.Wins > 15 && (r.Country == "USA" || r.Country == "UK") && index % 2 == 0).Select(r => r);
            Console.WriteLine("使用拓展方法");
            foreach (var item in racers)
            {
                Console.WriteLine(item.ToString("A"));
            }
            Console.Read();
        }

        /// <summary>
        /// 复合的from子句
        /// </summary>
        public void Fun5()
        {
            Console.WriteLine("----------Linq查询----------");
            var query = from r in Formula1.GetChampions()
                        from c in r.Cars
                        where c == "Ferrari"
                        orderby r.LastName
                        select r.FirstName + " " + r.LastName;
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------拓展方法----------");
            //SelectMany将Racer和Cars合并起来!
            var query2 = Formula1.GetChampions().SelectMany(r => r.Cars,(r,c) => new { Racer = r,Car = c }).
                         Where(r => r.Car == "Ferrari").OrderBy(r => r.Racer.LastName).Select(r => r.Racer.FirstName + " " + r.Racer.LastName);
            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------SelectMany()----------");
            var query3 = Formula1.GetChampions().SelectMany(r => r.Cars).Select(r => r);
            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }
            Console.Read();
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Fun6()
        {
            Console.WriteLine("----------Linq查询----------");
            var query = from r in Formula1.GetChampions()
                        where r.Country == "Brazil"
                        orderby r.Wins descending
                        select r;

            foreach (var item in query)
            {
                Console.WriteLine($"{item.FirstName} Wins:{item.Wins}");
            }

            Console.WriteLine("----------拓展方法----------");

            var query2 = Formula1.GetChampions().Where(r => r.Country == "Brazil").OrderByDescending(r => r.Wins).Select(r => r);
            foreach (var item in query2)
            {
                Console.WriteLine($"{item.FirstName} Wins:{item.Wins}");
            }
            Console.Read();
        }

        /// <summary>
        /// 排序(多条件)
        /// </summary>
        public void Fun7()
        {
            Console.WriteLine("----------Linq查询----------");
            var query = from r in Formula1.GetChampions()
                        where r.Country == "Brazil"
                        orderby r.Wins ascending, r.FirstName descending 
                        select r;

            foreach (var item in query)
            {
                Console.WriteLine($"{item.FirstName} Wins:{item.Wins}");
            }

            Console.WriteLine("----------拓展方法----------");

            var query2 = Formula1.GetChampions().Where(r => r.Country.Contains("Brazil")).OrderBy(r => r.Wins).ThenByDescending(r => r.FirstName).Select(r => r);
            foreach (var item in query2)
            {
                Console.WriteLine($"{item.FirstName} Wins:{item.Wins}");
            }
            Console.Read();
        }

        //接下来的方法请移步GroupByQuery()方法~

    }
}
