using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using XCYN.Core.MVC.Models;

namespace XCYN.Core.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 开发者在这里注册服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //注册MVC各个服务，并兼容2.1版本
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(option => {
                option.RespectBrowserAcceptHeader = true;   //后台服务考虑浏览器的Accept头
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddJsonOptions(options => {
                //设置不使用驼峰格式处理，由后台字段确定大小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //不返回值为NULL的属性
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddTransient(typeof(IFormula),typeof(MainFormula));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 注册Http请求的管线
        /// </summary>
        /// <param name="app">应用程序配置</param>
        /// <param name="env">运行环境</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
        {
           
            var logger = factory.CreateLogger("");
            
            app.Use(async(context,next) => {
                //有next()继续后续的中间件调用
                logger.LogDebug("继续调用后面的逻辑");
                await next();
            });

            app.Map(new PathString("/admin"), (builder) => {
                //匹配/admin这个URL路径
                builder.Run(async (context) => {
                    logger.LogDebug("登陆后台");
                    await context.Response.WriteAsync("欢迎登陆后台管理");
                });
            });

            app.MapWhen(context => {
                //匹配/formula这个URL
                return context.Request.Path.Value.Contains("/formula");
            }, builder => {
                builder.Run(async cont => {
                    logger.LogDebug("登陆公式超市");
                    await cont.Response.WriteAsync("欢迎登陆公式超市");
                });
            });

            app.UseWhen((context) => {
                if (context.Request.Path.Value.Contains(".png"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, builder => {
                builder.Use(async (context,next) => {
                    logger.LogDebug("直接访问了图片");
                    //调用了Use的next可以继续往后传递
                    await next();
                });
            });

            //错误异常处理
            if (env.IsDevelopment())
            {
                //开发环境返回错误页
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境返回错误页
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
