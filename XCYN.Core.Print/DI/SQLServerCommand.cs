using System;
using System.Collections.Generic;
using System.Text;

namespace XCYN.Core.Print.DI
{
    /// <summary>
    /// 这里类一定要用public修饰符
    /// </summary>
    public class SQLServerCommand : ICommand
    {
        Dictionary<int, string> dict = new Dictionary<int, string>();

        [MyLog]
        public void Add(int id, string msg)
        {
            if (!dict.ContainsKey(id))
            {
                dict[id] = msg;
            }
        }

        [MyLog]
        public string Get(int id)
        {
            if (dict.ContainsKey(id))
            {
                return dict[id];
            }
            else
            {
                throw new System.ArgumentNullException("字典中不包含这个键");
            }
        }
    }
}
