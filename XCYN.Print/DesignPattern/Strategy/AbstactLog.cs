using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCYN.Print.DesignPattern.Strategy
{
    /// <summary>
    /// 策略抽象类
    /// </summary>
    public abstract class AbstactLog
    {
        public abstract void Write(string msg);
    }
}