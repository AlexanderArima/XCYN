using System;
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
            var racers = Formula1.GetChampions().Where(r => r.Wins > 15 && (r.Country == "USA" || r.Country == "UK")).Select(r => r);
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


    }
}
