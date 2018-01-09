using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    public class FatPerson:AbstractPerson
    {

        public override string CreateHead()
        {
            return "胖人的头部";
        }

        public override string CreateBody()
        {
            return "胖人的身体";
        }

        public override string CreateHand()
        {
            return "胖人的手";
        }

        public override string CreateFoot()
        {
            return "胖人的脚";
        }
    }
}
