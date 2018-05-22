using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using XCYN.API.Controllers;

namespace XCYN.Test
{
    /// <summary>
    /// APITest 的摘要说明
    /// </summary>
    [TestClass]
    public class APITestCase
    {
        public APITestCase()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        /// <summary>
        /// 第一个控制器单元测试
        /// </summary>
        [TestMethod]
        public void TestNewGreetingAdd()
        {
            //准备
            var greetingName = "newgreeting";
            var greetingMessage = "Hello Test!";
            var fakeRequest = new HttpRequestMessage(HttpMethod.Post,
                "http://localhost:55898/api/greeting");
            var greeting = new Greeting
            {
                Name = greetingName,
                Message = greetingMessage,
            };
            var service = new GreetingController();
            service.Request = fakeRequest;

            //操作
            var response = service.PostGreeting(greeting);

            //断言
            Assert.IsNotNull(response);
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(new Uri("http://localhost:55898/api/greeting/newgreeting"),
                response.Headers.Location);

            
        }
    }
}
