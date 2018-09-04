using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.MVC.Models
{
    public class LinqValueCalc: IValueCalc
    {
        public decimal ValueProduct(IEnumerable<Product> products)
        {
            return products.Sum(m => m.UnitPrice);
        }
    }
}