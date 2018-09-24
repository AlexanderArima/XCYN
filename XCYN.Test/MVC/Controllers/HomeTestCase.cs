using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using XCYN.MVC;
using XCYN.MVC.Controllers;
using XCYN.MVC.Models;

namespace XCYN.Test.MVC.Controllers
{
    [TestClass()]
    public class HomeTestCase
    {
        [TestMethod()]
        public void IndexTest()
        {
            HomeController contr = new HomeController();
            contr.Index();
            Assert.IsNotNull(contr.ViewBag.Greeting);
        }

        [TestMethod]
        public void Index4Test()
        {
            //创建模仿对象
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            //选择方法，并传递参数，Return方法返回结果
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            //读取Mock对象的Object属性
            var target = new LinqValueCalc(mock.Object);
        }

        /// <summary>
        /// 工具方法，模仿请求响应
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        private  HttpContextBase CreateHttpContext(string targetUrl = null,string httpMethod = "GET")
        {
            //创建模仿请求
            Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();
            request.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            request.Setup(m => m.HttpMethod).Returns(httpMethod);
            //创建模仿响应
            Mock<HttpResponseBase> response = new Mock<HttpResponseBase>();
            response.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
            //创建使用上述请求和响应的上下文
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.Setup(m => m.Request).Returns(request.Object);
            context.Setup(m => m.Response).Returns(response.Object);
            //返回上下文
            return context.Object;
        }

        /// <summary>
        /// 路由匹配
        /// </summary>
        /// <param name="url"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="routeProperty"></param>
        /// <param name="httpMetod"></param>
        private void TestRouteMatch(string url,string controller,string action,object routeProperty = null,string httpMetod = "GET")
        {
            //准备
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            //处理路由
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMetod));
            //断言
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// 判断URL不起作用
        /// </summary>
        /// <param name="url"></param>
        private void TestRouteFail(string url)
        {
            //准备
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            //处理路由
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            //断言
            Assert.IsNotNull(result == null || result.Route == null);
        }

        [TestMethod]
        public void TestIncomingRoutes()
        {
            TestRouteMatch("~/Admin/Index", "Admin", "Index2");
            TestRouteMatch("~/One/Two", "One", "Two");

            TestRouteFail("~/Admin/Index/Segment");

            TestRouteFail("~/Admin");
        }

    }
}