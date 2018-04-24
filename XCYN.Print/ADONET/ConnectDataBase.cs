using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.ADONET
{
    public class ConnectDataBase
    {
        private static string sqlStr = "Data Source=.;Initial Catalog=Shopping;Integrated Security=True";

        /// <summary>
        /// 查询ExecuteReader()
        /// </summary>
        private static void Fun1()
        {
            using (SqlConnection conn = new SqlConnection(sqlStr))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM T_User WHERE ID = @ID";
                    //command.Parameters.AddWithValue("ID", 1);
                    command.Parameters.Add("ID", SqlDbType.Int);
                    command.Parameters["ID"].Value = 1;
                    conn.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"id:{reader.GetInt32(0)} name:{reader.GetString(1)} 是否删除:{ (reader.GetBoolean(4) ? "已删除" : "未删除") }");
                        //等同于下面索引的形式
                        Console.WriteLine($"id:{reader[0]} name:{reader[1]} 是否删除:{ (Convert.ToBoolean(reader[4]) ? "已删除" : "未删除") }");
                    }
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 增删改ExecuteNonQuery()
        /// </summary>
        private static void Fun2()
        {
            using (SqlConnection conn = new SqlConnection(sqlStr))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    string AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                    command.CommandText = @"INSERT INTO [dbo].[T_User]
                                                               ([UserName]
                                                               ,[PassWord]
                                                               ,[AddTime]
                                                               ,[IsDelete])
                                                         VALUES
                                                               (@UserName
                                                               ,@PassWord
                                                               ,@AddTime
                                                               ,0)";
                    command.Parameters.AddWithValue("UserName", "李四");
                    command.Parameters.AddWithValue("PassWord", "123");
                    command.Parameters.AddWithValue("AddTime", AddTime);
                    conn.Open();
                    //var count = command.ExecuteNonQuery();
                    var count = command.ExecuteScalar();
                    //Console.WriteLine($"影响了{count}条数据");
                }
            }
        }

        /// <summary>
        /// 返回第一行第一列的数据ExecuteScalar()
        /// </summary>
        private static void Fun3()
        {
            using (SqlConnection conn = new SqlConnection(sqlStr))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    string AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                    command.CommandText = @"SELECT COUNT(1) FROM T_User";
                    conn.Open();
                    //var count = command.ExecuteNonQuery();
                    var count = command.ExecuteScalar();
                    Console.WriteLine($"用户总数：{count}");
                    //Console.WriteLine($"影响了{count}条数据");
                }
            }
        }

        /// <summary>
        /// 填充数据集 Fill()
        /// </summary>
        private static void Fun4()
        {
            using (SqlConnection conn = new SqlConnection(sqlStr))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM T_User", conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Console.WriteLine($"ID:{ds.Tables[0].Rows[i]["ID"]} UserName:{ds.Tables[0].Rows[i]["UserName"]}");
                    }
                }
            }
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        private static void Fun5()
        {
            using (SqlConnection conn = new SqlConnection(sqlStr))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "QueryT_User";
                command.CommandType = CommandType.StoredProcedure;//标识存储过程
                SqlParameter param = new SqlParameter("@ID", 1);
                command.Parameters.Add(param);
                SqlDataAdapter adapter = new SqlDataAdapter("QueryT_User", conn);
                adapter.SelectCommand = command;//调用存储过程
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Console.WriteLine($"ID:{ds.Tables[0].Rows[i]["ID"]} UserName:{ds.Tables[0].Rows[i]["UserName"]}");
                }
            }
        }
    }
}
