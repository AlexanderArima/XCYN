using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XCYN.Print.XmlAndJson
{
    public class LinqToXML
    {
        /// <summary>
        /// Linq生成XML
        /// </summary>
        public void Fun1()
        {
            XNamespace ns = "http://org.com/";//定义一个命名空间
            XElement book = new XElement(ns + "book",
                new XAttribute("id",1),//设置属性
                new XAttribute("c_id",123),
                new XElement("Name", "C#高级编程"),//设置元素
                new XElement("Price", 110),
                new XElement("ISBN","7799-2325-3242-998B"),
                new XElement("Authors",
                    new XElement("Author","Wang"),
                    new XElement("Author", "Li"),
                    new XElement("Author", "Scott"))
                );
            Console.WriteLine(book);
            Console.Read();
            book.Save("MyBook.xml");
        }

        /// <summary>
        /// 使用Linq读取XML
        /// </summary>
        public void Fun2()
        {
            XDocument document = XDocument.Load("MyBook.xml");
            var query = from a in document.Descendants("book")
                        select a;
            foreach (var item in query)
            {
                var elements = item.Elements();//获取子元素的集合
                foreach (var item2 in elements)
                {
                    if(item2.Name.LocalName.Equals("Price"))
                    {
                        //将价格改为200
                        item2.SetValue("200");
                    }
                    Console.WriteLine(item2);
                }
            }
            document.Save("MyBook2.xml");
            Console.Read();
        }
    }
}
