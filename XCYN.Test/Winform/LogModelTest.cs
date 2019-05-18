using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Winform.Model.LogViewer;

namespace XCYN.Test.Winform
{
    /// <summary>
    /// LogViewerTest 的摘要说明
    /// </summary>
    [TestClass]
    public class LogModelTest
    {
        public LogModelTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
            _logModel = new LogModel();
        }

        private TestContext testContextInstance;
        private LogModel _logModel;

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
        public void GetList()
        {
           var list =  _logModel.GetList("","",null,null);
            Assert.AreEqual(4, list.Count);

            list = _logModel.GetList("", "Info", null, null);
            Assert.AreEqual(1, list.Count);

            list = _logModel.GetList("", "FATAL", null, null);
            Assert.AreEqual(1, list.Count);

            list = _logModel.GetList("", "", new DateTime(2019,1,14,12,0,0).ToString("yyyy-MM-dd HH:mm:ss"), null);
            Assert.AreEqual(4, list.Count);

            list = _logModel.GetList("", "",null, new DateTime(2019, 1, 14, 18, 0, 0));
            Assert.AreEqual(4, list.Count);

            list = _logModel.GetList("", "", new DateTime(2019, 1, 14, 14, 0, 0).ToString("yyyy-MM-dd HH:mm:ss"), new DateTime(2019, 1, 14, 18, 0, 0));
            Assert.AreEqual(3, list.Count);
        }
        
    }
}
