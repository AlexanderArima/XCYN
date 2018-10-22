using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCYN.Core.Print.ORM
{
    public class MyMySQL
    {
        public void Fun1()
        {
            var ds =  MySqlHelper.ExecuteDataset("server=192.168.43.136;port=3306;userid=root;pwd=900424;database=MyDB", 
                "SELECT * FROM Students");
            if(ds.Tables[0].Rows.Count >= 1)
            {
                Console.WriteLine("查到数据了");
            }

        }
    }
}
