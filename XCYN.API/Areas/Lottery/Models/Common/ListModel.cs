using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.API.Areas.Lottery.Models.Common
{
    public class ListModel:AbstractModel
    {
        public int count { get; set; }

        public int index { get; set; }
    }
}