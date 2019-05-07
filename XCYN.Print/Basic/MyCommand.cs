using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    public class MyCommand
    {
        public void Fun1()
        {
            using (MyConnection conn = new MyConnection())
            {
                try
                {
                    conn.Open();
                }
                finally
                {
                    conn.Dispose();
                }
            }
        }
    }
}
