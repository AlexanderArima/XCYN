using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
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

        [TestMethod]
        public void ExecuteSql()
        {
            MySql.Data.MySqlClient.MySqlParameter[] param = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("first_name","cheng")
            };
            var flag = MySqlHelper.ExecuteSql("UPDATE actor SET first_name = @first_name where actor_id = 1", param);
            MySql.Data.MySqlClient.MySqlParameter[] param2 = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("actor_id","1")
            };
            var ds = MySqlHelper.Query("SELECT * FROM actor WHERE actor_id = @actor_id", param2);
            if(ds.Tables[0].Rows.Count > 0)
            {
                Assert.AreEqual("cheng", ds.Tables[0].Rows[0]["first_name"].ToString());
            }
        }

        [TestMethod]
        public void ExecuteSqlTran()
        {
            Hashtable table = new Hashtable();
            MySql.Data.MySqlClient.MySqlParameter[] param = new MySql.Data.MySqlClient.MySqlParameter[] {
                new MySql.Data.MySqlClient.MySqlParameter("first_name","wang")
            };
            //DictionaryEntry entry = new DictionaryEntry(@"UPDATE actor SET first_name = @first_name where actor_id = 1", param);

            MySql.Data.MySqlClient.MySqlParameter[] param2 = new MySql.Data.MySqlClient.MySqlParameter[] {
                new MySql.Data.MySqlClient.MySqlParameter("first_name","cheng")
            };
            //DictionaryEntry entry2 = new DictionaryEntry(@"UPDATE actor SET first_name = @first_name where actor_id = 2", param2);
            table.Add(@"UPDATE actor SET first_name = @first_name where actor_id = 1", param);
            table.Add(@"UPDATE actor SET first_name = @first_name where actor_id2 = 2", param2);
            MySqlHelper.ExecuteSqlTran(table);
        }
    }
}
