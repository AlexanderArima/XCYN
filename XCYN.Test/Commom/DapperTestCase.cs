using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common.Dapper;
using XCYN.Linq.Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace XCYN.Test.Common
{
    /// <summary>
    /// DapperTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DapperTestCase
    {
        public DapperTestCase()
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
        public void SelectInsert()
        {
            //不带参数
            var list = DapperHelper.Query<user>("SELECT * FROM users");
            Assert.AreNotEqual(0, list.Count);

            //带上参数
            var list2 = DapperHelper.Query<user>("SELECT * FROM users WHERE ID = @id",new { id = 2 });
            Assert.AreEqual(1, list2.Count);

            //join操作
            var list3 = DapperHelper.Query("SELECT * FROM users left join zcp_users on users.id = zcp_users.user_id");
            Assert.AreNotEqual(0, list3.AsList().Count);
        }

        public static string connectionString = ConfigurationManager.ConnectionStrings["MeetingSys"].ConnectionString;

        [TestMethod]
        public void SelectInsert2()
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                var list = conn.Query<user>("select * from users");
                Assert.AreNotEqual(0, list.AsList().Count);
                var list2 = conn.Query<user>("SELECT * FROM users WHERE ID = @id", new { id = 2 });
                Assert.AreEqual(1, list2.AsList().Count);
            }
        }
    }
}
