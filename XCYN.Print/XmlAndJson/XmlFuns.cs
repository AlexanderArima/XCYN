using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace XCYN.Print.XmlAndJson
{
    public class XmlFuns
    {

        /// <summary>
        /// 读取所有文本内容
        /// </summary>
        public void Fun1()
        {
            using (XmlReader reader = XmlReader.Create("books.xml"))
            {
                while(reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        Console.WriteLine(reader.Value);
                    }
                }
            }
            Console.Read();
        }

        /// <summary>
        /// 读取节点的值
        /// </summary>
        public void Fun2()
        {
            using (XmlReader reader = XmlReader.Create("books.xml"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "price")
                        {
                            var price = reader.ReadElementContentAsDecimal();
                            Console.WriteLine($"price:{price}");
                        }
                        else if(reader.Name == "title")
                        {
                            var title = reader.ReadElementContentAsString();
                            Console.WriteLine($"title:{title}");
                        }

                        
                    }
                }
            }
        }

        string bookFile = "books.xml";

        /// <summary>
        /// 读取节点特性
        /// </summary>
        public void Fun3()
        {
            using (XmlReader reader = XmlReader.Create(bookFile))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            Console.WriteLine(reader.GetAttribute(i));
                        }

                    }
                }
            }
        }

        public string newXml = "newbook.xml";

        /// <summary>
        /// 使用XmlWriter类将Xml写入一个流，文件中
        /// </summary>
        public void Fun4()
        {
            var setting = new XmlWriterSettings()
            {
                Indent = true,//缩进
                IndentChars = "  ",//缩进字符
                NewLineOnAttributes = false,
                Encoding = Encoding.UTF8,
                WriteEndDocumentOnClose = true,
            };

            StreamWriter stream = File.CreateText(newXml);//打开一个写入流
            using (XmlWriter writer = XmlWriter.Create(stream, setting))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("book");
                writer.WriteAttributeString("gener", "Mystery");
                writer.WriteAttributeString("publicationdate", "2001");
                writer.WriteAttributeString("ISBN", "123456789");
                writer.WriteElementString("title", "Case of missing Cookie");
                writer.WriteStartElement("author");
                writer.WriteElementString("name", "Cookie Monster");
                writer.WriteEndElement();
                writer.WriteElementString("price", "9.99");
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// 使用XmlDocument
        /// </summary>
        public void Fun5()
        {
            using (FileStream stream = File.OpenRead(bookFile))
            {
                XmlDocument document = new XmlDocument();
                document.Load(bookFile);//加载文件
                XmlNodeList list = document.GetElementsByTagName("title");
                foreach (XmlNode item in list)
                {
                    Console.WriteLine(item.OuterXml);
                }
                Console.Read();
            }
                
        }

        /// <summary>
        /// 遍历层次结构
        /// </summary>
        public void Fun6()
        {
            using (FileStream stream = File.OpenRead(bookFile))
            {
                var doc = new XmlDocument();
                doc.Load(stream);
                var nodeList = doc.GetElementsByTagName("author");
                foreach (XmlNode item in nodeList)
                {
                    //OuterXml包含此节点及子节点
                    Console.WriteLine($"OuterXml:{item.OuterXml}");
                    //InnerXml包含子节点
                    Console.WriteLine($"InnerXml:{item.InnerXml}");
                    Console.WriteLine($"兄弟节点(下一个节点)的OuterXml:{item.NextSibling.OuterXml}");
                    Console.WriteLine($"兄弟节点(上一个节点)的OuterXml:{item.PreviousSibling.OuterXml}");
                    Console.WriteLine($"父节点的OuterXml:{item.ParentNode.OuterXml}");
                    break;
                }
                Console.Read();
            }
        }

        public string newbook2 = "newbook2.xml";

        /// <summary>
        /// XmlDocument写入文件，XmlDocument可以随机地读取文档
        /// </summary>
        public void Fun7()
        {
            var doc = new XmlDocument();
            using (FileStream stream = File.OpenRead(bookFile))
            {
                doc.Load(stream);
            }
            var book = doc.CreateElement("book");
            book.SetAttribute("genre", "MyStery");
            book.SetAttribute("publicationdate", "2001");
            book.SetAttribute("ISBN", "123456789");
            var title = doc.CreateElement("title");
            title.InnerText = "Case of Missing Cookie";
            book.AppendChild(title);//将title节点添加到book子节点的末尾
            var author = doc.CreateElement("author");
            book.AppendChild(author);//将author节点添加到book子节点的末尾
            var name = doc.CreateElement("name");
            name.InnerText = "Cookie Monster";
            author.AppendChild(name);
            var price = doc.CreateElement("price");
            price.InnerText = "9.99";
            book.AppendChild(price);

            doc.DocumentElement.AppendChild(book);//将book节点添加到根节点(bookStore)的子节点的末尾
            var setting = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = Environment.NewLine,
            };
            using (StreamWriter stream = File.CreateText(newbook2))
            {
                //创建或打开一个写入流
                using (XmlWriter writer = XmlWriter.Create(stream, setting))
                {
                    //创建一个写入器，并将所有内容写入进去
                    doc.WriteContentTo(writer);
                }
                var list = doc.GetElementsByTagName("title");
                foreach (XmlNode item in list)
                {
                    Console.WriteLine(item.OuterXml);
                }
                Console.Read();
            }
        }

        /// <summary>
        /// 使用XPathNavigator类
        /// </summary>
        public void Fun8()
        {
            var doc = new XPathDocument(bookFile);

            var xPathNavigator = doc.CreateNavigator();

            var iter = xPathNavigator.Select("/bookstore/book[@genre='novel']");

            while(iter.MoveNext())
            {
                var newiter = iter.Current.SelectDescendants(XPathNodeType.Element, false);
                while(newiter.MoveNext())
                {
                    Console.WriteLine($"{newiter.Current.Name} {newiter.Current.Value}");
                }
            }
            Console.Read();
        }

        public void Fun9()
        {
            var product = new Product()
            {
                ProductID = 10,
                ProductName = "联想笔记本",
                SupplierID = 2,
            };

            var book = new BookProduct()
            {
                ProductID = 101,
                ProductName = "钢铁是怎样炼成的",
                SupplierID = 10,
                ISBN = "985034592834732"
            };

            var person = new Person
            {
                Age = 12
            };

            Product[] item = { product, book, person };

            Inventory inventory = new Inventory
            {
                InventoryItems = item
            };

            using (FileStream stream = File.Create("Inventory.xml"))
            {
                var serilizer = new XmlSerializer(typeof(Inventory));
                serilizer.Serialize(stream, inventory);
            }

            Console.Read();
        }

       
    }
}
