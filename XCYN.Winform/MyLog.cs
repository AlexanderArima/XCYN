using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;

namespace XCYN.Winform
{
    public class MyLog
    {
        public static ILog logger = LogManager.GetLogger(typeof(Program));
    }
}
