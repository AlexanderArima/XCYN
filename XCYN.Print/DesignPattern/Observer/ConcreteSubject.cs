using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Observer
{
    class ConcreteSubject : ISubject
    {
        public string SutjectState { get ; set; }

        List<IObserver> _list = new List<IObserver>();

        /// <summary>
        /// 添加观察者
        /// </summary>
        /// <param name="observer"></param>
        public void Add(IObserver observer)
        {
            _list.Add(observer);
        }

        /// <summary>
        /// 通知观察者
        /// </summary>
        public void Notify()
        {
            foreach(var item in _list)
            {
                item.Modify();
            }
        }

        /// <summary>
        /// 移除观察者
        /// </summary>
        /// <param name="observer"></param>
        public void Remove(IObserver observer)
        {
            _list.Remove(observer);
        }
    }
}
