using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    public class ChainTree<T>
    {
        public T data { get; set; }

        public ChainTree<T> left { get; set; }

        public ChainTree<T> right { get; set; }
    }
}
