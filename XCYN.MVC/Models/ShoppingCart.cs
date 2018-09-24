using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.MVC.Models
{
    public class ShoppingCart : IEnumerable<Product>
    {

        public List<Product> product { get; set; }
        private IValueCalc calc;

        public ShoppingCart()
        {

        }

        public ShoppingCart(IValueCalc calc)
        {
            this.calc = calc;
        }

        public decimal CalcProductTotal()
        {
            return calc.ValueProduct(product);
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return product.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}