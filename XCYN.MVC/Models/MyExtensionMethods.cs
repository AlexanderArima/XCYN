using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCYN.Print.XmlAndJson;

namespace XCYN.MVC.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrice(this IEnumerable<Product> product)
        {
            decimal total = 0;
            for (int i = 0; i < product.Count(); i++)
            {
                total += product.ElementAt(i).UnitPrice;
            }
            return total;
        }
    }
}