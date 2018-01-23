using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.Web.Model
{
    public class User
    {

        public User(DateTime add_time, int id = 0,string name = "")
        {
            this.add_time = add_time;
            this.id = id;
            this.name = name;
        }

        public int id { get; set; }
        public string name { get; set; }
        public DateTime add_time { get; set; }
    }
}