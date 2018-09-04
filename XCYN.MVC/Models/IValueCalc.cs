using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.MVC.Models
{
    public interface IValueCalc
    {
        decimal ValueProduct(IEnumerable<Product> products);
    }
}
