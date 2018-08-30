using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.MVC.Models;

namespace XCYN.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            var date = DateTime.Now;
            if(date.Hour <= 11)
            {
                ViewBag.Greeting = "Good Morning";
            }
            else if(date.Hour > 11 && date.Hour < 18)
            {
                ViewBag.Greeting = "Good Afternoon";
            }
            else if(date.Hour < 24)
            {
                ViewBag.Greeting = "Good Evening";
            }
            return View();
        }

        public RedirectResult Redir()
        {
            return Redirect("Index");
        }

        public HttpUnauthorizedResult UnAuth()
        {
            return new HttpUnauthorizedResult();
        }

        public ViewResult AutoProperty()
        {
            return View("ShowModel", (object)string.Format("输出字段：{0}", 17));
        }

        public ViewResult CreateCollection()
        {
            string[] array = { "001", "002", "003" };
            List<int> list = new List<int> { 98, 99, 100 };
            Dictionary<string, int> dict = new Dictionary<string, int>
            {
                { "Cheng",18 },
                { "Wang",28 },
                { "Qin",17 }
            };
            return View((object)dict);
        }

        [HttpGet]
        public ViewResult GetProduct()
        {
            Product pro = new Product()
            {
                ProductName = "电脑",
                ProductID = 1,
                UnitPrice = 4000
            };
            return View(pro);
        }

        public ViewResult UseExtensionEnumerable()
        {
            /*
             new Product()
                {
                    UnitPrice = 10
                },
                new Product()
                {
                    UnitPrice = 12.1m
                },
                new Product()
                {
                    UnitPrice = 17.1m
                }
             */
            var price = new ShoppingCart()
            {
                product = new List<Product>()
                {
                    new Product()
                    {
                        UnitPrice = 10
                    },
                    new Product()
                    {
                        UnitPrice = 12.1m
                    },
                    new Product()
                    {
                        UnitPrice = 17.1m
                    }
                }
            }.TotalPrice();
            return View("ShowModel",(object)price);
        }
    }
}