using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCYN.Core.Print.DI
{
    /// <summary>
    /// 依赖注入的调用者
    /// </summary>
    public class HandlerFun
    {
        public void Fun1()
        {
            //服务的集合，这里我们将一个一个的类看成是服务了。
            ServiceCollection services = new ServiceCollection();
            //每次从容器 （IServiceProvider）中获取的时候都是一个新的实例
            //services.AddTransient<IEnable, Human>();

            //每次从同根容器中（同根 IServiceProvider）获取的时候都是同一个实例
            services.AddSingleton<IEnable, Human>();
            
            //创建一个服务提供者
            var provider = services.BuildServiceProvider();
            
            var human = provider.GetService<IEnable>();
            human = provider.GetService<IEnable>();
        }

        public void Fun2()
        {
            //服务的集合，这里我们将一个一个的类看成是服务了。
            ServiceCollection services = new ServiceCollection();

            //每次从同一个容器中获取的实例是相同的、
            services.AddScoped<IEnable, Human>();

            //创建一个服务提供者
            var provider = services.BuildServiceProvider();

            var scope1 = provider.CreateScope().ServiceProvider;  //第一个范围
            var scope2 = provider.CreateScope().ServiceProvider;  //第二个范围
            
            var human = scope1.GetService<IEnable>();
            human = scope2.GetService<IEnable>();
        }

    }
}
