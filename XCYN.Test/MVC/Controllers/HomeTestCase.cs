using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.MVC.Controllers;

namespace XCYN.Test.MVC.Controllers
{
    [TestClass()]
    public class HomeTestCase
    {
        [TestMethod()]
        public void IndexTest()
        {
            HomeController contr = new HomeController();
            contr.Index();
            Assert.IsNotNull(contr.ViewBag.Greeting);
        }

    }
}