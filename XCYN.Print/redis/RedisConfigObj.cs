using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.redis
{
    public class RedisConfigObj
    {
        public string WriteServerList { get; set; }
        public string ReadServerList { get; set; }
        public string MaxWritePoolSize { get; set; }
        public string MaxReadPoolSize { get; set; }
        public string AutoStart { get; set; }

    }
}
