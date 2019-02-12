using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;

namespace XCYN.Common.Access
{
    public class OleDbHelper {
        //Database connection strings
        private static string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\MyGitProject\XCYN\XCYN.Winform\Quartz\Timer.accdb";

        // 1
        public static string GetString(OleDbTransaction trans, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdText, cmdType, cmdParms);
                return ConvertHelper.GetString(cmd.ExecuteScalar());
            }
        }

        // 2
        public static string GetString(OleDbTransaction trans, string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetString(trans, cmdText, CommandType.Text, cmdParms);
        }

        // 3
        public static string GetString(OleDbConnection conn, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                PrepareCommand(cmd, conn, null, cmdText, cmdType, cmdParms);
                return ConvertHelper.GetString(cmd.ExecuteScalar());
            }
        }

        // 4
        public static string GetString(OleDbConnection conn, string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetString(conn, cmdText, CommandType.Text, cmdParms);
        }

        // 5
        public static string GetString(string connStr, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                return GetString(conn, cmdText, cmdType, cmdParms);
            }
        }

        // 6
        public static string GetString(string connStr, string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetString(connStr, cmdText, CommandType.Text, cmdParms);
        }

        // 7
        public static string GetString(string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetString(connStr, cmdText, cmdParms);
        }

        // 1
        public static int ExecuteNonQuery(OleDbTransaction trans, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdText, cmdType, cmdParms);
                return cmd.ExecuteNonQuery();
            }
        }

        // 2
        public static int ExecuteNonQuery(OleDbTransaction trans, string cmdText, params OleDbParameter[] cmdParms)
        {
            return ExecuteNonQuery(trans, cmdText, CommandType.Text, cmdParms);
        }

        // 3
        public static int ExecuteNonQuery(OleDbConnection conn, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                PrepareCommand(cmd, conn, null, cmdText, cmdType, cmdParms);
                return cmd.ExecuteNonQuery();
            }
        }

        // 4
        public static int ExecuteNonQuery(OleDbConnection conn, string cmdText, params OleDbParameter[] cmdParms)
        {
            return ExecuteNonQuery(conn, cmdText, CommandType.Text, cmdParms);
        }

        // 5
        public static int ExecuteNonQuery(string connStr, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                return ExecuteNonQuery(conn, cmdText, cmdType, cmdParms);
            }
        }

        // 6
        public static int ExecuteNonQuery(string connStr, string cmdText, params OleDbParameter[] cmdParms)
        {
            return ExecuteNonQuery(connStr, cmdText, CommandType.Text, cmdParms);
        }

        // 7
        private static int ExecuteNonQuery(string cmdText, params OleDbParameter[] cmdParms)
        {
            return ExecuteNonQuery(connStr, cmdText, cmdParms);
        }

        // 1
        public static DataTable GetTable(OleDbTransaction trans, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbDataAdapter da = new OleDbDataAdapter(new OleDbCommand()))
            {
                PrepareCommand(da.SelectCommand, trans.Connection, trans, cmdText, CommandType.Text, cmdParms);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2
        public static DataTable GetTable(OleDbTransaction trans, string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetTable(trans, cmdText, CommandType.Text, cmdParms);
        }

        // 3
        public static DataTable GetTable(OleDbConnection conn, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbDataAdapter da = new OleDbDataAdapter(new OleDbCommand()))
            {
                PrepareCommand(da.SelectCommand, conn, null, cmdText, CommandType.Text, cmdParms);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 4
        public static DataTable GetTable(OleDbConnection conn, string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetTable(conn, cmdText, CommandType.Text, cmdParms);
        }

        // 5
        public static DataTable GetTable(string connStr, string cmdText, CommandType cmdType, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                return GetTable(conn, cmdText, cmdType, cmdParms);
            }
        }

        // 6
        public static DataTable GetTable(string connStr, string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetTable(connStr, cmdText, CommandType.Text, cmdParms);
        }

        // 7
        public static DataTable GetTable(string cmdText, params OleDbParameter[] cmdParms)
        {
            return GetTable(connStr, cmdText, cmdParms);
        }
        
        /// <summary>
        /// 构建 OleDbCommand 对象
        /// </summary>
        /// <param name="cmdText">OleDb 语句</param>
        /// <param name="type">语句类型 默认为 Text</param>
        /// <param name="cmdParameters">OleDb 参数</param>
        /// <returns>OleDbCommand</returns>
        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, CommandType cmdType, OleDbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                QuickOpen(conn);
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;

            if (cmdParms != null) {
                foreach (OleDbParameter parm in cmdParms) {
                    if (parm != null) {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) && (parm.Value == null)) {
                            parm.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }

        /// <summary>
        /// 限制连接超时时间
        /// </summary>
        public static void QuickOpen(OleDbConnection conn)
        {
            if (conn.State == ConnectionState.Open)
                return;

            int timeout = 10000;
            string strError = "";

            // We'll use a Stopwatch here for simplicity. A comparison to a stored DateTime.Now value could also be used
            Stopwatch sw = new Stopwatch();
            bool connectSuccess = false;

            // Try to open the connection, if anything goes wrong, make sure we set connectSuccess = false
            Thread t = new Thread(delegate() {
                sw.Start();
                try {
                    conn.Open();
                }
                catch (Exception ex) {
                    strError = ex.Message;
                }
                connectSuccess = true;
            });

            // Make sure it's marked as a background thread so it'll get cleaned up automatically
            t.IsBackground = true;
            t.Start();

            // Keep trying to join the thread until we either succeed or the timeout value has been exceeded
            while (timeout > sw.ElapsedMilliseconds)
                if (t.Join(1))
                    break;

            // If we didn't connect successfully, throw an exception
            if (!string.IsNullOrEmpty(strError))
                throw new Exception(strError);
            if (!connectSuccess)
                throw new Exception("连接超时！未能连接到数据库！");
        }
    }
}