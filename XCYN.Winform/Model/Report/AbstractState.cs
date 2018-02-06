using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.Model.Report
{
    public abstract class AbstractState
    {
        public abstract object Handle(Context context);
    }
}
