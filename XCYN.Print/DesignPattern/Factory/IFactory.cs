﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Factory
{
    interface IFactory
    {
        IDataBase CreateInstance();
    }
}