using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartupAttribute(typeof(XCYN.WS.Startup))]
namespace XCYN.WS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //基本配置
            //app.MapSignalR<Connection102>("/myConn");
            //跨域场景中使用JSONP和CORS
            //app.Map("/myConn", map => {
            //    map.MapSignalR<Connection102>("/myConn", new Microsoft.AspNet.SignalR.ConnectionConfiguration() {
            //        EnableJSONP = true,
            //    });
            //    map.UseCors(CorsOptions.AllowAll);
            //});

            //以Redis为底板(中间件)，作为一个代理，实现多个Server都能收到同一个BoardCast
            //GlobalHost.DependencyResolver.UseRedis("localhost", 6379, string.Empty, "WSKeys");

            //以SqlServer为底板(中间件)，作为一个代理，实现多个Server都能收到同一个BoardCast
            //GlobalHost.DependencyResolver.UseSqlServer("Data Source=.;Initial Catalog=WSMiddle;Integrated Security=True");

            app.MapSignalR("/myConn", new Microsoft.AspNet.SignalR.HubConfiguration()
            {
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = true,
                EnableJSONP = true,
            });

            //注入管道流
            GlobalHost.HubPipeline.AddModule(new MyHubPipelineModule());
        }
    }

    /// <summary>
    ///  继承HubPipelineModule，并注入到GlobalHost.HubPipeline中，可以达到监控和记录日志的作用
    /// </summary>
    public class MyHubPipelineModule2 : HubPipelineModule
    {
        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            return base.OnBeforeIncoming(context);
        }

        protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
        {
            return base.OnBeforeOutgoing(context);
        }
    }

    /// <summary>
    /// 实现IHubPipelineModule，并注入到GlobalHost.HubPipeline中，可以达到监控和记录日志的作用
    /// </summary>
    public class MyHubPipelineModule : IHubPipelineModule
    {
        public Func<HubDescriptor, IRequest, bool> BuildAuthorizeConnect(Func<HubDescriptor, IRequest, bool> authorizeConnect)
        {
            return authorizeConnect;
        }

        public Func<IHub, Task> BuildConnect(Func<IHub, Task> connect)
        {
            return connect;
        }

        public Func<IHub, bool, Task> BuildDisconnect(Func<IHub, bool, Task> disconnect)
        {
            return disconnect;
        }

        public Func<IHubIncomingInvokerContext, Task<object>> BuildIncoming(Func<IHubIncomingInvokerContext, Task<object>> invoke)
        {
            return (context) => {
                var methodName = context.MethodDescriptor.Name;
                var ConnectionId = context.Hub.Context.ConnectionId;
                return invoke(context);
            };
        }

        public Func<IHubOutgoingInvokerContext, Task> BuildOutgoing(Func<IHubOutgoingInvokerContext, Task> send)
        {
            return (context) => {
                var Invocation = context.Invocation.Method;
                var Signal = context.Signal;
                var Connection = context.Connection;
                var ExcludedSignals = context.ExcludedSignals;
                return send(context);
            };
        }

        public Func<IHub, Task> BuildReconnect(Func<IHub, Task> reconnect)
        {
            return reconnect;
        }

        public Func<HubDescriptor, IRequest, IList<string>, IList<string>> BuildRejoiningGroups(Func<HubDescriptor, IRequest, IList<string>, IList<string>> rejoiningGroups)
        {
            return rejoiningGroups;
        }
    }
}
