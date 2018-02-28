using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common.Dapper
{
    public class DapperManager
    {

        public static string connectionString = ConfigurationManager.ConnectionStrings["MeetingSys"].ConnectionString;

        private static IDbConnection _instance = null;

        private static object _lock = new object();

        private DapperManager()
        {

        }

        /// <summary>
        /// 连接实例
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlConnection(connectionString);
                    }
                }

            }
            return _instance;
        }
    }
}
