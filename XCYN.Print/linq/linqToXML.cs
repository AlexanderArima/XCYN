using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XCYN.Print.linq
{
    public class linqToXML
    {

        /// <summary>
        /// 从数据库中读取数据，保存在XML文件中  
        /// </summary>
        public void post()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            //Linq to XML
            //保存数据
            var xml = new XElement("users",
                                            from a in context.users
                                            select new XElement("user",
                                                    new XAttribute("id", Guid.NewGuid().ToString()),
                                                    new XElement("id", a.id),
                                                    new XElement("username", a.user_name)));
            xml.Save("D:\\linqToXML.xml");
        }

        /// <summary>
        /// 从XML文件中读取数据
        /// </summary>
        public void get()
        {
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
    }
}
