using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Bridge
{
    public class PhoneXiaoMi : PhoneBrand
    {
        public override void run()
        {
            Console.WriteLine("运行小米手机");
            foreach (var item in soft)
            {
                item.run();
            }
        }
    }
}
