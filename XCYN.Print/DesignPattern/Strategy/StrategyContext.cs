using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Strategy
{
    public class StrategyContext
    {

        private AbstactLog _log = null;

        public StrategyContext(AbstactLog log)
        {
            _log = log;
        }

        public StrategyContext()
        {
            _log = new FileLog();
        }

        public void Write(string msg)
        {
            //字符串长度超过10，则写入到数据库中
            if (msg.Length > 10)
            {
                _log = new DBLog();
            }
            _log.Write(msg);
        }
    }
}
