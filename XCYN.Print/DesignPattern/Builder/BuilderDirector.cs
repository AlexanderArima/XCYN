using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    public class BuilderDirector
    {
        public AbstractPerson person { get; set; }

        public BuilderDirector()
        {

        }

        public string Create()
        {
            //调用建造类
            var head = person.CreateHead();
            var body = person.CreateBody();
            var hand = person.CreateHand();
            var foot = person.CreateFoot();
            return head + "_" + body + "_" + head + "_" + foot;
        }
    }
}
