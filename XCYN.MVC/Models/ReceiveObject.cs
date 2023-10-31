using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.MVC.Models
{
    public class ReceiveObject
    {
        public string code { get; set; }

        public string msg { get; set; }

        public object data { get; set; }
    }
}