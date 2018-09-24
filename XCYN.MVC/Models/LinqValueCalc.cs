using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.MVC.Models
{
    public class LinqValueCalc: IValueCalc
    {
        private IDiscountHelper discounter;

        public LinqValueCalc(IDiscountHelper discounter)
        {
            this.discounter = discounter;
        }

        public decimal ValueProduct(IEnumerable<Product> products)
        {
            return discounter.ApplyDiscount(products.Sum(m => m.UnitPrice));
        }
    }
}