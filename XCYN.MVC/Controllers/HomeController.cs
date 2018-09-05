using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.MVC.Models;
using Ninject;

namespace XCYN.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalc calc;

        public HomeController()
        {

        }

        public HomeController(IValueCalc calc)
        {
            this.calc = calc;
        }

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

        public ActionResult Index2()
        {
            var dis = new DefDiscountHelp();
            dis.DiscountSize = 10;
            IValueCalc value = new LinqValueCalc(dis);
            ShoppingCart shop = new ShoppingCart(value) {
                product = new List<Product>()
                {
                    new Product()
                    {
                        UnitPrice = 100,
                    },
                     new Product()
                    {
                        UnitPrice = 200,
                    },
                }
            };
            var total = shop.CalcProductTotal();
            return View(total);
        }

        public ActionResult Index3()
        {
            //第一阶段：创建一个Ninject的内核(Kernel)实例，它负责解析依赖项并创建新的对象。
            //当我们需要创建对象时，将使用这个内核而不是new关键字。
            IKernel ninject = new StandardKernel();
            //第二阶段：配置Ninject内核，将想要使用的接口设置为bind方法的类型参数，将希望实例化的实现类设置为To方法的类型参数
            ninject.Bind<IDiscountHelper>().To<DefDiscountHelp>().WithPropertyValue("DiscountSize",10M);
            ninject.Bind<IValueCalc>().To<LinqValueCalc>().WithConstructorArgument("IDiscountHelper", ninject.Get<IDiscountHelper>());
            //第三阶段：调用Ninject的Get方法来创建一个对象，Get方法的参数可以告诉Ninject创建的是哪个接口，返回值是那个To方法指定的实现类型的实例。
            IValueCalc value = ninject.Get<IValueCalc>();
            ShoppingCart shop = new ShoppingCart(value)
            {
                product = new List<Product>()
                {
                    new Product()
                    {
                        UnitPrice = 100,
                    },
                     new Product()
                    {
                        UnitPrice = 200,
                    },
                }
            };
            var total = shop.CalcProductTotal();
            return View(total);
        }

        public ActionResult Index4()
        {
            ShoppingCart shop = new ShoppingCart(calc)
            {
                product = new List<Product>()
                {
                    new Product()
                    {
                        UnitPrice = 100,
                    },
                     new Product()
                    {
                        UnitPrice = 200,
                    },
                }
            };
            var total = shop.CalcProductTotal();
            return View(total);
        }


    }
}