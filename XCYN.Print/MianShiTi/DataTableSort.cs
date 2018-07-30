using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    public class DataTableSort
    {
        public void Fun1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name",typeof(string));
            dt.Columns.Add("Age",typeof(int));//因为是字符串，所以排序不对
            dt.Rows.Add("小明", 21);
            dt.Rows.Add("小张", 10);
            dt.Rows.Add("小红", 9);
            dt.Rows.Add("小伟", 7);
            dt.Rows.Add("小美", 3);
            dt.DefaultView.Sort = "Age ASC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow s in dt.Rows)
            {
                Console.WriteLine(s["Age"].ToString() + "--" + s["Name"].ToString());
            }
            Console.WriteLine();
        }
    }
}
