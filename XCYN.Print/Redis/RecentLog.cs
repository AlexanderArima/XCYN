using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common.Sql.redis;

namespace XCYN.Print.Redis
{

    public enum SEVERITY
    {
        DEBUG = 0,
        INFO = 1,
        WARNING = 2,
        ERROR = 3,
        CRITICAL = 4,
    }

    public class RecentLog
    {

        RedisCommand command = new RedisCommand();

        private void LogRecent(string name = "",string message = "",SEVERITY severity = SEVERITY.INFO)
        {
            string serv = Enum.GetName(typeof(SEVERITY), severity);
            string dest = $"recent:{name}:{serv}";
            string msg = DateTime.Now + " " + message;
            command.ListLeftPush(dest, msg);
            command.ListTrim(dest, 0, 100);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        public void Fun1()
        {
            LogRecent("后台程序出现异常", "空指针错误!", SEVERITY.ERROR);
            LogRecent("后台程序出现异常", "空指针错误!", SEVERITY.ERROR);
            LogRecent("后台程序出现异常", "空指针错误!", SEVERITY.INFO);
            LogRecent("后台程序出现异常", "空指针错误!", SEVERITY.DEBUG);
            LogRecent("后台程序出现异常", "空指针错误!", SEVERITY.CRITICAL);
        }

        /// <summary>
        /// 读取日志
        /// </summary>
        public void Fun2()
        {
            var list = command.Keys("recent:*" + Enum.GetName(typeof(SEVERITY), SEVERITY.INFO));
            foreach (var keys in list)
            {
                var values = command.ListRange(keys.ToString(), 0, -1);
                Console.WriteLine(keys);
                foreach (var item in values)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
            }
        }

    }
}
