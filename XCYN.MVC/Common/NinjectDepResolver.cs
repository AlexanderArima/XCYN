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
            kernel.Bind<IDiscountHelper>().To<DefDiscountHelp>().WithPropertyValue("DiscountSize",10M);   //创建了一个依赖项链，为每一个依赖项进行解析
        }

        //private void AddBinding<I,T>()  where T : I 
        //{
        //    kernel.Bind<I>().To<T>();
        //}

    }
}