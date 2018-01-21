using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Command
{
    public class Invoker
    {
        List<ICommand> _list = new List<ICommand>();
        
        /// <summary>
        /// 批量执行命令
        /// </summary>
        public void execute()
        {
            foreach (var item in _list)
            {
                item.execute();
            }
        }

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="command"></param>
        public void SetCommand(ICommand command)
        {
            _list.Add(command);
        }

        /// <summary>
        /// 撤销命令
        /// </summary>
        public void Redo()
        {
            if(_list.Count > 0)
                _list.RemoveAt(_list.Count - 1);
        }
    }
}
