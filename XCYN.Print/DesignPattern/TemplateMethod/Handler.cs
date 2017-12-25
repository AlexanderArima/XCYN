using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.TemplateMethod
{
    public class Handler
    {
        public void HanderQuestion()
        {
            XieCheng x = new XieCheng();
            x.Question();

            XiongWei xw = new XiongWei();
            xw.Question();
        }
    }
}
