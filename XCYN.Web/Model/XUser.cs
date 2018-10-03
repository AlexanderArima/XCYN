using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.Web.Model
{
    public class XUser
    {
        public XUser()
        {

        }

        public XUser(int ID,string UserName,int Age)
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Age = Age;
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}