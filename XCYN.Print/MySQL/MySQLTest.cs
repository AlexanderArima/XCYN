using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MySQL
{
    public class MySQLTest
    {
        public void Fun1()
        {
            //server=localhost;user id=root;password=root;database=abc
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;password=password;database=sakila"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM actor";
                command.CommandType = System.Data.CommandType.Text;
                var reader =  command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                }
            }
        }
    }
}
