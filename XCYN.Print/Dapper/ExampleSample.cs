using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XCYN.Linq.Model;

namespace XCYN.Print.Dapper
{
    
    public class ExampleSample
    {
        public static string connectionString = @"Data Source=.\MSSQL2008;Initial Catalog=XPMS;Persist Security Info=True;User ID=sa;Password=900424";

        /// <summary>
        /// 查询数据
        /// </summary>
        public void Fun1()
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                var dog = conn.Query<user>("select user_name from users where id > @id",new { id = 200 } );
                
                foreach (var item in dog)
                {
                    Console.WriteLine($"{item.user_name}");
                }
            }
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        public void Fun2()
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                List<zcp_post> list = new List<zcp_post>();
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new zcp_post()
                    {
                        title = "测试发帖问题" + (i + 1),
                        content = "新人报道，灌个水......",
                        user_name = "111111",
                        add_time = DateTime.Now
                    });
                }
                var count = conn.Execute(@"INSERT INTO [MeetingSys].[dbo].[zcp_post]
               ([channel_id]
               ,[category_id]
               ,[call_index]
               ,[title]
               ,[content]
               ,[sort_id]
               ,[click]
               ,[status]
               ,[user_name]
               ,[add_time]
               ,[user_id]
               ,[title_type]
               ,[is_reply])
                VALUES
               (14
               ,6100
               ,''
               ,@title
               ,@content
               ,0
               ,0
               ,1
               ,@user_name
               ,@add_time
               ,2
               ,1
               ,1)", list
                );
                if(count > 0)
                {
                    Console.WriteLine("插入成功!");
                }
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        public void Fun3()
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                //var count = conn.Execute("UPDATE zcp_post SET title = @title WHERE id in @id", new { title = "测试修改内容", id = 250 });
                var count = conn.Execute("UPDATE zcp_post SET title = @title WHERE id in @ids", new { title = "测试修改内容" ,ids = new int[]{ 249,250 } });

                Console.WriteLine($"影响行数:{count}");
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        public void Fun4()
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                //var count = conn.Execute("DELETE FROM zcp_post where id = @id", new { id = 250 });
                var count = conn.Execute("DELETE FROM zcp_post where id in @ids", new { ids = new int[] { 248, 249 } });

                Console.WriteLine($"影响行数:{count}");
            }
        }

        /// <summary>
        /// 事务操作
        /// </summary>
        public void Fun5()
        {
            IDbConnection conn = null;
            IDbTransaction tran = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();//事务操作需要打开连接

                tran = conn.BeginTransaction();
                var count = conn.Execute("DELETE FROM zcp_post where id in(SELECT MAX(ID) FROM ZCP_POST)", null, tran);

                var count2 = conn.Execute(@"INSERT INTO [MeetingSys].[dbo].[zcp_post]
               ([channel_id]
               ,[category_id]
               ,[call_index]
               ,[title]
               ,[content]
               ,[sort_id]
               ,[click]
               ,[status]
               ,[user_name]
               ,[add_time]
               ,[user_id]
               ,[title_type]
               ,[is_reply])
                VALUES
               (14
               ,6100
               ,''
               ,@title
               ,@content
               ,0
               ,0
               ,1
               ,@user_name
               ,@add_time
               ,2
               ,1
               ,1)", new
                {
                    title = "测试发帖问题99",
                    content = "新人报道，灌个水......",
                    user_name = "111111",
                    add_time = DateTime.Now
                }
               , tran);

                //tran.Commit();
            }
            catch(Exception ex)
            {
                tran.Rollback();
                
            }
            finally
            {
                tran.Dispose();
                conn.Close();
            }
            
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public void Fun6(int pageIndex,int pageSize)
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                var dog = conn.Query<user>($@"SELECT * FROM 
                                            (
                                                SELECT *, ROW_NUMBER() over
                                                (
                                                    order by id desc
                                                ) n
                                                from zcp_post
                                            ) hhh
                                            WHERE hhh.n > @start and n <= @end",new { start = pageSize * pageIndex , end = pageSize * (pageIndex + 1) });
                foreach (var item in dog)
                {
                    Console.WriteLine($"{item.id}");
                }
            }
        }
    }
}
