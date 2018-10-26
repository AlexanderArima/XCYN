using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Factory
{
    /// <summary>
    /// 被工厂创建的接口类
    /// </summary>
    public interface IDataBase
    {
        void Create();

        void Remove();
    }
}
