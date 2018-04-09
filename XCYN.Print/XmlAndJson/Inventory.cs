using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCYN.Print.XmlAndJson
{
    public class Inventory
    {
        [XmlArrayItem("Product",typeof(Product)),XmlArrayItem("Book",typeof(BookProduct))]
        public Product[] InventoryItems { get; set; }

        public override string ToString()
        {
            var outText = new StringBuilder();
            for (int i = 0; i < InventoryItems.Length; i++)
            {
                var name = InventoryItems[i].GetType().Name;
            }
            //foreach (Product item in InventoryItems)
            //{
            //    outText.AppendLine(item.ProductName);
            //}
            return outText.ToString();
        }
    }
}
