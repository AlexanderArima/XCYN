using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.MVC.Models;

namespace XCYN.MVC.Common
{
    /// <summary>
    /// 依赖解析器
    /// </summary>
    public class NinjectDepResolver:IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDepResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBinding();
        }
        
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBinding()
        {
            kernel.Bind<IValueCalc>().To<LinqValueCalc>();
        }

        //private void AddBinding<I,T>()  where T : I 
        //{
        //    kernel.Bind<I>().To<T>();
        //}

    }
}