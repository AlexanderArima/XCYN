using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.WS.Models
{
    public class User
    {
        public string ConnectionID { get; set; }
        public string RandomName { get; set; }
        public int SayNumber { get; set; }
        public string action { get; set; }
        public string data { get; set; }
        public string group_name { get; set; }
    }
}