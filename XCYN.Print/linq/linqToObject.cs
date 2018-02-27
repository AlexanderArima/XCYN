
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Print.linq.DataLib;

namespace XCYN.Print.linq
{
    public class LinqToObject
    {
        public void Get()
        {
            //goto myCase;
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            dic.Add(1, new List<int> { 10, 20, 30, 40 });
            dic.Add(2, new List<int> { 10, 20, 30, 40 });
            dic.Add(3, new List<int> { 10, 20, 30, 40 });
            //将序列中每个元素合并到一个数组中
            var query = dic.SelectMany(i => i.Value);
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            int[] num1 = new int[] { 10, 20, 30, 40, 50 };
            int[] num2 = new int[] { 10, 20, 30, 40 };
            //比较数组中每一个元素是否相等
            var query2 = num1.SequenceEqual(num2);
            Console.WriteLine(query2);

            //myCase:
            int[] num3 = new int[] { 10, 20, 30, 40, 50 };
            int[] num4 = new int[] { 10, 20, 30, 40 };
            int[] num5 = new int[] { 10, 20, 30 };
            var query3 = num3.Zip(num4, (i, j) => i + j).Zip(num5, (i, j) => i + j);
            //对两个函数进行操作
            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// OfType方法，用于类型的筛选
        /// </summary>
        public void Fun1()
        {
            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();//筛选字符串
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

        }

        /// <summary>
        /// 条件筛选
        /// </summary>
        public void Fun2()
        {
            var query = from r in Formula1.GetChampions()
                        where r.Wins > 15
                        select r;
            foreach (var item in query)
            {
                Console.WriteLine($"{item:A}");
            }
        }

        /// <summary>
        /// group分组
        /// </summary>
        public void Fun3()
        {
            var query = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        orderby g.Count() descending, g.Key
                        where g.Count() > 2
                        select new
                        {
                            Country = g.Key,
                            Count = g.Count()
                        };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Country,-10}{item.Count}");
            }
        }

        /// <summary>
        /// 对嵌套的对象分组
        /// </summary>
        public void Fun4()
        {
            var query = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        orderby g.Count() descending, g.Key
                        where g.Count() >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = g.Count(),
                            Racers = from r1 in g
                                     orderby r1.LastName
                                     select r1.FirstName + " " + r1.LastName
                        };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Country,-10}{item.Count}");
                foreach (var i in item.Racers)
                {
                    Console.Write($"{i},");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 内连接
        /// </summary>
        public void Fun5()
        {
            //使用join子句可以根据特定的条件合并两个数据源，但之前要获得两个要连接的列表.
            //赛车手
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            //车队
            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            //根据赛车手获得冠军的年份和车队获得冠军的年份进行连接
            var racersAndTeam = from r in racers
                                join t in teams on r.Year equals t.Year
                                select new
                                {
                                    r.Year,
                                    Champion = r.Name,
                                    Constructor = t.Name
                                };
            foreach (var item in racersAndTeam)
            {
                Console.WriteLine($"{item.Year}年，冠军:{item.Champion,-20}，冠军车队:{item.Constructor}");
            }

            //foreach (var item in teams.OrderBy(i=>i.Year))
            //{
            //    Console.WriteLine($"Year:{item.Year} , Name:{item.Name}");
            //}
            //foreach (var item in racers.OrderBy(i=>i.Year))
            //{
            //    Console.WriteLine($"Year:{item.Year} , Name:{item.Name}");
            //}
        }

        /// <summary>
        /// 左外连接用join子句和DefaultIfEmpty方法定义
        /// 使用左外连接，返回左边序列中的全部元素，即使他们在右边的序列中没有匹配的元素
        /// </summary>
        public void Fun6()
        {
            //赛车手
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            //车队
            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };
            //由于赛车手冠军从1950年就有，赛车冠军从1958才有。使用左连接就能获取所有的赛车手冠军，即使他们在右边的序列中没有匹配的元素
            var racersAndTeam = from r in racers
                                join t in teams on r.Year equals t.Year into rt
                                from a in rt.DefaultIfEmpty()
                                orderby r.Year
                                select new
                                {
                                    Year = r.Year,
                                    Champion = r.Name,
                                    Constructor = a == null ? "no constructor championship" : a.Name
                                };
            foreach (var item in racersAndTeam)
            {
                Console.WriteLine($"{item.Year}年，冠军:{item.Champion,-20}，冠军车队:{item.Constructor}");
            }
        }

        /// <summary>
        /// 集合操作
        /// </summary>
        public void Fun7()
        {
            //下面创造一个驾驶法拉第的一级方程式冠军和驾驶迈凯伦的一级方程式冠军，然后确定是否有驾驶法拉第和迈凯伦的冠军
            //var q = from r in Formula1.GetChampions()
            //        from c in r.Cars
            //        where c == "Ferrari"
            //        orderby r.LastName
            //        select r;

            //var q2 = from r in Formula1.GetChampions()
            //        from c in r.Cars
            //        where c == "McLaren"
            //        orderby r.LastName
            //        select r;

            //等价于

            Func<string, IEnumerable<Racer>> racersByCar =
                car => from r in Formula1.GetChampions()
                       from c in r.Cars
                       where c == car
                       orderby r.LastName
                       select r;

            foreach (var item in racersByCar("Ferrari").Intersect(racersByCar("McLaren")))
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }
            
        }

        /// <summary>
        /// 分页
        /// </summary>
        public void Fun8(int pageSize,int pageIndex)
        {
            var numberPages = (int)Math.Ceiling(Formula1.GetChampions().Count() / (double)pageSize);
            if (numberPages < pageIndex)
                return;
            //赛车手
            var racers = (from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         }).Skip(pageSize * pageIndex).Take(pageSize);
            foreach (var item in racers)
            {
                Console.WriteLine(item.Year);
            }
        }

        /// <summary>
        /// 生成操作符
        /// </summary>
        public void Fun9()
        {
            var values = Enumerable.Range(1, 20);//生成一定范围的数字
            Console.WriteLine(string.Join(",",values));
            var values2 = Enumerable.Empty<int>();//用于需要一个集合的参数
            Console.WriteLine(values2);
            var values3 = Enumerable.Repeat<string>("我是谁?", 100);//把同一个值重复特定的次数
            Console.WriteLine(string.Join(",", values3));
            //foreach (var item in values)
            //{
            //    Console.WriteLine($"{item}");
            //}
        }
    }
    
}
