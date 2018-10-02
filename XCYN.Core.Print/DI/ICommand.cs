using System;
using System.Collections.Generic;
using System.Text;

namespace XCYN.Core.Print.DI
{
    public interface ICommand
    {
        void Add(int id,string msg);
        
        string Get(int id);
    }
}
