using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Observer
{
    class ConcreteObserver1 : IObserver
    {
        ISubject _subject = null;

        string _name = string.Empty;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="subject">订阅者</param>
        /// <param name="name">观察者名字</param>
        public ConcreteObserver1(ISubject subject,string name)
        {
            _subject = subject;
            _name = name;
        }

        //监听Subject发生改变后，做出的反应
        public void Modify()
        {
            Console.WriteLine("{0}被修改，{1}做出了反应", _subject.SutjectState, _name);
        }
    }
}
