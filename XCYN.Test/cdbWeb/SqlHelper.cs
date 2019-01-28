using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;


public class SqlHelper
{
    public static string connStr = ConfigHelper.ReadConnectionStringConfig("DefaultConnection");

    /// <summary>
    /// 批量导入
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static bool ExecuteNonQuery(DataTable dt)
    {
        SqlConnection connection = null;
        SqlTransaction tran = null;
        SqlBulkCopy sqlbulkcopy = null;
        try
        {
            connection = new SqlConnection(connStr);
            connection.Open();
            tran = connection.BeginTransaction();//开启事务
            sqlbulkcopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, tran);
            sqlbulkcopy.BulkCopyTimeout = 100;  //超时之前操作完成所允许的秒数
            sqlbulkcopy.BatchSize = dt.Rows.Count;  //每一批次中的行数
            sqlbulkcopy.DestinationTableName = dt.TableName;  //服务器上目标表的名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sqlbulkcopy.ColumnMappings.Add(i, i);  //映射定义数据源中的列和目标表中的列之间的关系
            }
            sqlbulkcopy.WriteToServer(dt);  // 将DataTable数据上传到数据表中
            tran.Commit();
            return true;
        }
        catch (Exception e)
        {
            if (connection != null)
            {
                connection.Close();
            }
            tran.Rollback();
            return false;
        }
        finally
        {
            if (connection != null)
            {
                connection.Close();
            }
            if (sqlbulkcopy != null)
            {
                sqlbulkcopy.Close();
            }
        }
    }

    public static string GetString(string cmdText, params SqlParameter[] cmdParms)
    {
        return GetString(null, cmdText, cmdParms);
    }

    public static string GetString(string cmdText, SqlTransaction trans, params SqlParameter[] cmdParms)
    {
        SqlConnection conn = trans.Connection;
        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        {
            cmd.Transaction = trans;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    if (parm != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) && (parm.Value == null))
                        {
                            parm.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parm);
                    }
                }
            }
            return ConvertHelper.GetString(cmd.ExecuteScalar());
        }
    }

    public static string GetString(string dbName, string cmdText, params SqlParameter[] cmdParms)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                PrepareCommand(dbName, cmd, conn, null, cmdText, CommandType.Text, cmdParms);
                return ConvertHelper.GetString(cmd.ExecuteScalar());
            }
        }
    }

    public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms)
    {
        return ExecuteNonQuery(null, cmdText, cmdParms);
    }

    public static int ExecuteNonQuery(string dbName, string cmdText, params SqlParameter[] cmdParms)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                PrepareCommand(dbName, cmd, conn, null, cmdText, CommandType.Text, cmdParms);
                return cmd.ExecuteNonQuery();
            }
        }
    }

    public static DataTable GetTable(string cmdText, params SqlParameter[] cmdParms)
    {
        return GetTable(null, cmdText, cmdParms);
    }

    public static DataTable GetTable(string dbName, string cmdText, params SqlParameter[] cmdParms)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(cmdText, conn))
            {
                PrepareCommand(dbName, da.SelectCommand, conn, null, cmdText, CommandType.Text, cmdParms);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    /// <summary>
    /// 构建 SqlCommand 对象
    /// </summary>
    /// <param name="cmdText">Sql 语句</param>
    /// <param name="type">语句类型 默认为 Text</param>
    /// <param name="cmdParameters">Sql 参数</param>
    /// <returns>SqlCommand</returns>
    private static void PrepareCommand(string dbName, SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
    {
        if (conn.State != ConnectionState.Open)
            QuickOpen(conn);
        if (!string.IsNullOrEmpty(dbName))
            conn.ChangeDatabase(dbName);
        cmd.Connection = conn;
        cmd.CommandText = cmdText;

        if (trans != null)
            cmd.Transaction = trans;

        cmd.CommandType = cmdType;

        if (cmdParms != null)
        {
            foreach (SqlParameter parm in cmdParms)
            {
                if (parm != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) && (parm.Value == null))
                    {
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
    public static void QuickOpen(SqlConnection conn)
    {
        if (conn.State == ConnectionState.Open)
            return;

        int timeout = 8000;
        string strError = "";

        // We'll use a Stopwatch here for simplicity. A comparison to a stored DateTime.Now value could also be used
        Stopwatch sw = new Stopwatch();
        bool connectSuccess = false;

        // Try to open the connection, if anything goes wrong, make sure we set connectSuccess = false
        Thread t = new Thread(delegate () {
            sw.Start();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
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

    /// <summary>
    /// 执行多条SQL语句，实现数据库事务。
    /// </summary>
    /// <param name="SQLStringList">多条SQL语句</param>		
    public static int ExecuteSqlTran(List<String> SQLStringList)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlTransaction tx = conn.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                int count = 0;
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n];
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        count += cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
                return count;
            }
            catch(Exception ex)
            {
                tx.Rollback();
                return 0;
            }
        }
    }
}