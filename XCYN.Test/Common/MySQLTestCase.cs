using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Test.Common
{
    [TestClass]
    public class MySQLTestCase
    {
        [TestMethod]
        public void Query()
        {
            var ds =  MySqlHelper.Query("SELECT * FROM actor");
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);

        }

        [TestMethod]
        public void GetReader()
        {
            var reader = MySqlHelper.ExecuteReader("SELECT * FROM actor");
            Assert.IsTrue(reader.HasRows);
        }

        [TestMethod]
        public void GetScalar()
        {
            var actor = Convert.ToInt32(MySqlHelper.ExecuteScalar("SELECT count(*) FROM actor"));
            Assert.AreEqual(200, actor);
        }

        [TestMethod]
        public void Exists()
        {
            var flag = MySqlHelper.Exists("SELECT * FROM actor where actor_id = 1000");
            Assert.IsFalse(flag);
            flag = MySqlHelper.Exists("SELECT * FROM actor");
            Assert.IsTrue(flag);
        }
    }
}
