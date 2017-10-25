using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XCYN.Linq.Model;

namespace XCYN.Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            //Linq to XML
            //保存
            //var xml = new XElement("users", 
            //                                from a in context.users
            //                                select new XElement("user",
            //                                                    new XAttribute("id",Guid.NewGuid().ToString()),
            //                                                    new XElement("id",a.id),
            //                                                    new XElement("username",a.user_name)));
            //xml.Save("D:\\linqToXML.xml");

            //提取数据
            XElement ele = XElement.Load("D:\\linqToXML.xml");
            var query = from a in ele.Elements()
                        where a.Element("id").Value == "2"
                        select a;
            foreach (var item in query.ToList())
            {
                //Console.WriteLine(item.Element("username").Value);
                Console.WriteLine(item.Attribute("id").Value);
            }
            Console.ReadKey();
        }

        static void Fun2()
        {
            //Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();

            //dic.Add(1, new List<int> { 10, 20, 30, 40 });
            //dic.Add(2, new List<int> { 10, 20, 30, 40 });
            //dic.Add(3, new List<int> { 10, 20, 30, 40 });
            //将序列中每个元素合并到一个数组中
            //var query = dic.SelectMany(i => i.Value);
            //foreach (var item in query)
            //{
            //    Console.WriteLine(item);
            //}

            //int[] num1 = new int[] { 10, 20, 30, 40 };
            //int[] num2 = new int[] { 10, 20, 30, 40 };
            ////比较数组中每一个元素是否相等
            //var query = num1.SequenceEqual(num2);
            //Console.WriteLine(query);

            //int[] num1 = new int[] { 10, 20, 30, 40 ,50 };
            //int[] num2 = new int[] { 10, 20, 30, 40 };
            //var query = num1.Zip(num2, (i, j) => i + j);
            ////对两个函数进行操作
            //foreach (var item in query)
            //{
            //    Console.WriteLine(item);
            //}
        }

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
                            select new { formula_name = f.formula_name, category_name = c.name, grade_name = r.name };

                //列出所有公式的类型，并显示数量
                var query2 = from a in
                             (
                             from b in entity.zcp_formula
                             group b by b.category_id into c
                             select new { category_id = c.Key, c_count = c.Count() }
                             )
                             join d in entity.zcp_formula_category
                             on a.category_id equals d.id
                             select new { a.category_id, d.name, a.c_count };

                //列出用户111111的所购买的公式
                var query3 = from b in
                            (from a in entity.users
                             where a.user_name.Equals("111111")
                             select new { user_id = a.id })
                             join c in entity.zcp_formu_order
                             on b.user_id equals c.user_id
                             join d in entity.zcp_formula
                             on c.formu_id equals d.id
                             select new { formula_name = d.formula_name, point = c.point };
                foreach (var item in query3.ToList())
                {
                    Console.WriteLine(item);
                }
                //列出用户111111购买公式的总价
                var query4 = query3.Sum(i => i.point);
                Console.WriteLine(query4);

            }
        }

        static void DelegateFun()
        {
            //调用委托
            MyAction ac1 = SayHello;
            ac1("ac1");

            //匿名委托
            MyAction2 ac2 = delegate (string str)
            {
                Console.WriteLine(str);
                return 1;
            };
            ac2("ac2");

            //使用lambda表达式 
            MyAction2 ac3 = str => 1;
            Console.WriteLine(ac3("ac3"));

            //使用系统委托
            Action<int, int> ac4 = (str, str2) => Console.WriteLine(str + str2);
            ac4(1, 2);

            Func<int, int, bool> ac5 = (str, str2) =>
            {
                if (str > str2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };

            Console.WriteLine(ac5(5, 3));

            Action<int, int> ac6 = (str, str2) =>
            {
                Console.WriteLine(str + str2);
            };
        }
        
        static void SayHello(string str)
        {
            Console.WriteLine(str);
        }

        delegate void MyAction(string str);

        delegate int MyAction2(string str);
    }
}
