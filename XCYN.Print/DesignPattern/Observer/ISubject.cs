using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Observer
{
    interface ISubject
    {
        string SutjectState { get; set; }

        /// <summary>
        /// 添加观察者
        /// </summary>
        /// <param name="observer"></param>
        void Add(IObserver observer);

        /// <summary>
        /// 移除观察者
        /// </summary>
        /// <param name="observer"></param>
        void Remove(IObserver observer);

        /// <summary>
        /// 通知观察者
        /// </summary>
        void Notify();
    }
}
