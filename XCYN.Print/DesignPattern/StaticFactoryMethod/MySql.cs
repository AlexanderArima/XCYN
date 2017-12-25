using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.StaticFactoryMethod
{
    class MySql:IDataBase
    {
        public string ip { get; set; }
        public int port { get; set; }

        public MySql()
        {
            this.ip = "192.168.1.1";
            this.port = 3306;
        }

        public MySql(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public override IDataBase GetInstance()
        {
            return new SqlServer();
        }

        public IDataBase GetInstance(string ip, int port)
        {
            return new SqlServer(ip, port);
        }
    }
}
