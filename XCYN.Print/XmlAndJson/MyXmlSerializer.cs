using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCYN.Print.XmlAndJson
{
    public class MyXmlSerializer
    {

        /// <summary>
        /// 序列化一个对象
        /// </summary>
        public void Fun1()
        {
            Product product = new Product()
            {
                CategoryID = 1,
                Discontinued = true,
                Discount = 15,
                ProductID = 1,
                ProductName = "戴尔笔记本电脑",
                QuantityPerUnit = "6",
                ReorderLevel = 1,
                SupplierID = 1,
                UnitPrice = 4000,
                UnitsInStock = 10,
                UnitsOnOrder = 0
            };
            //打开一个文件流
            var stream = File.OpenWrite("product.xml");
            //向文件流中写入字符
            using (TextWriter writer = new StreamWriter(stream))
            {
                //创建一个序列化实例对象
                XmlSerializer serializer = new XmlSerializer(typeof(Product));
                serializer.Serialize(writer, product);//将对象写入到文件流中
            }
            Console.Read();
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        public void Fun2()
        {
            Product product;
            //打开一个文件流
            using (var stream = new FileStream("product.xml", FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(Product));
                product = (Product)serializer.Deserialize(stream);
            }
        }
    }
}
