using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Common.Access;

namespace XCYN.Test.Common
{
    [TestClass]
    public class OleDbHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var table = OleDbHelper.GetTable("SELECT * FROM T_ServiceList");
            Assert.AreEqual(2, table.Rows.Count);
        }
    }
}
