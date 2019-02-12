using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common.Access;
using System.Reflection;
using XCYN.Common;

namespace XCYN.Test.Winform
{
    /// <summary>
    /// TimerTest 的摘要说明
    /// </summary>
    [TestClass]
    public class TimerTest
    {
        public TimerTest()
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

        [TestMethod]
        public void InvokeTest()
        {
            var table = OleDbHelper.GetTable("SELECT * FROM T_ServiceList");
            var AssemblyName = table.Rows[0]["AssemblyName"].ToString();
            var namespaceName = table.Rows[0]["NameSpace"].ToString();
            var ClassName = table.Rows[0]["ClassName"].ToString();
            var MethodName = table.Rows[0]["MethodName"].ToString();
            var result = ReflectionHelper.InvokeMethod(AssemblyName, namespaceName, ClassName, MethodName);
        }
        
    }
}
