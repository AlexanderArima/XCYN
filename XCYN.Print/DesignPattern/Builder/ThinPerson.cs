using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder
{
    public class ThinPerson:AbstractPerson
    {
        public override string CreateHead()
        {
            return "瘦人的头部";
        }

        public override string CreateBody()
        {
            return "瘦人的身体";
        }

        public override string CreateHand()
        {
            return "瘦人的手";
        }

        public override string CreateFoot()
        {
            return "瘦人的脚";
        }
    }
}
