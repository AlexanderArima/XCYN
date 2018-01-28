using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Bridge
{
    /// <summary>
    /// 手机品牌，包含软件(Soft)类
    /// </summary>
    public abstract class PhoneBrand
    {
        protected List<Soft> soft = new List<Soft>();

        public void AddSoft(Soft soft)
        {
            this.soft.Add(soft);
        }

        public void RemoveSoft(Soft soft)
        {
            this.soft.Remove(soft);
        }

        public abstract void run();
    }
}
