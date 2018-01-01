using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Observer
{
    interface IObserver
    {
        /// <summary>
        /// 待订阅者发生改变后，供其调用的方法
        /// </summary>
        void Modify();
    }
}
