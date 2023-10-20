namespace XCYN.Print.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCYN.Print.linq.DataLib;

    /// <summary>
    /// Linq的拓展方法.
    /// </summary>
    public class ExtenLinq
    {
        /// <summary>
        /// 测试Linq中的Select().
        /// </summary>
        public static List<Racer> Fun1()
        {
            var list = Formula1.GetChampions();
            var result = list.Select((r) =>
            {
                r.FirstName = r.FirstName.ToUpper();
                r.LastName = r.LastName.ToUpper();
                r.Country = r.Country.ToUpper();
                r.Cars = r.Cars.Select((m) => m.ToUpper()).ToList();
                return r;
            }).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                var item = result.ElementAt(i);
                Console.WriteLine(string.Format("FirstName：{0}，LastName：{1}，Country：{2}", item.FirstName, item.LastName, item.Country));
            }

            return result;
        }

        /// <summary>
        /// 测试Linq中的SelectMany().
        /// </summary>
        public static List<string> Fun2()
        {
            //var list = Formula1.GetChampions();
            //var result2 = list.SelectMany(m =>
            //{
            //    //  new List<string>(m.Country)
            //});
            //var result = list.SelectMany((r) =>
            //{
            //    r.Years = r.Years.Select(m => m).ToList();
            //    return r.Years;
            //}).ToList();
            //for (int i = 0; i < result2.Count; i++)
            //{
            //    var item = result2.ElementAt(i);
            //    Console.WriteLine(string.Format("Country：{0}", item));
            //}

            //return result;
            return null;
        }

        /// <summary>
        /// 实现和Linq中的SelectMany()相同的操作.
        /// </summary>
        public static List<int> Fun2_2()
        {
            var list = Formula1.GetChampions();
            List<int> result = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list.ElementAt(i);
                for (int j = 0; j < item.Years.Count(); j++)
                {
                    var item2 = item.Years.ElementAt(j);
                    result.Add(item2);
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                var item = result.ElementAt(i);
                Console.WriteLine(string.Format("Years：{0}", item));
            }

            return result;
        }
    }
}
