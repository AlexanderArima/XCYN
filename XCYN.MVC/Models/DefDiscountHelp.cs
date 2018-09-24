using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.MVC.Models
{
    public class DefDiscountHelp : IDiscountHelper
    {
        public decimal DiscountSize { get; set; }

        public decimal ApplyDiscount(decimal total)
        {
            return total - (DiscountSize / 100 * total);
        }
    }
}