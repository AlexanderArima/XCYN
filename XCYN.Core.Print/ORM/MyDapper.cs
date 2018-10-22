using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace XCYN.Core.Print.ORM
{

    /// <summary>
    /// Dapper本质上是重写了IConnection，实现了一系列拓展方法，它主要做了两件事：
    /// 1. 简化了参数化查询
    /// 2. 将数据库表与对象映射了起来
    /// </summary>
    public class MyDapper
    {

        /// <summary>
        /// 查询所有
        /// </summary>
        public void Fun1()
        {
            MySqlConnection conn = new MySqlConnection("server=192.168.43.136;port=3306;userid=root;pwd=900424;database=MyDB");
            var stu = conn.Query<Students>("SELECT * FROM Students");
            foreach (var item in stu)
            {
                Console.WriteLine($"ID:{item.ID},Name:{item.Name},Age:{item.Age}");
            }
        }
        
        /// <summary>
        /// 带参数的查询
        /// </summary>
        public void Fun2()
        {
            MySqlConnection conn = new MySqlConnection("server=192.168.43.136;port=3306;userid=root;pwd=900424;database=MyDB");
            var stu = conn.QueryFirst<Students>("SELECT * FROM Students WHERE ID = @ID",new { ID = 1 });
            Console.WriteLine($"ID:{stu.ID},Name:{stu.Name},Age:{stu.Age}");
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public void Fun3()
        {
            MySqlConnection conn = new MySqlConnection("server=192.168.43.136;port=3306;userid=root;pwd=900424;database=MyDB");
            
            var num = conn.Execute("INSERT INTO Students values(3,'Tim',13)");
        }


        
    }

    class Students
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
