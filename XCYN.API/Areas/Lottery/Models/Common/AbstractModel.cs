using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.API.Areas.Lottery.Models.Common
{
    public abstract class AbstractModel
    {
        public object obj { get; set; }

        public JSONResult result { get; set; }

        public string message { get; set; }
    }

    public enum JSONResult
    {
        fail = 0,
        success = 1
    }
}