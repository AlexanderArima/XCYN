using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XCYN.Test.Print
{
    [TestClass]
    public class LinqTestCase
    {
       
        public LinqTestCase()
        {
            
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        [TestMethod]
        public void TestAll()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var flag = list.All(m => {
                var last = m.Name.Last();
                return last.Equals('e');
            });
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void TestAny()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var flag = list.Any(m => m.Name.Equals("we"));
            Assert.IsTrue(flag);
            flag = list.Any(m => m.Name.Equals("wo"));
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void TestContains()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var flag = list.Contains(new Product
            {
                Name = "we"
            },new ProductCoomparer());
            Assert.IsTrue(flag);    //对于复杂类型，需要实现IEqualityComparer才能比较

            flag = list.Contains(new Product
            {
                Name = "we"
            });
            Assert.IsFalse(flag);

            var str = "we";
            Assert.IsTrue(str.Contains("we"));  //简单类型，可以直接比较

        }


        [TestMethod]
        public void TestCount()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var count = list.Count();
            Assert.AreEqual(2, count);
            Assert.AreEqual(1, list.Count(m => m.Name.Equals("we")));
            
        }

        [TestMethod]
        public void TestFirst()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };
            
            Assert.AreEqual("we", list.First().Name);
            Assert.AreEqual("fsdfasfwe", list.First(m => m.Name.Equals("fsdfasfwe")).Name);

        }
    }

    class ProductCoomparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            return x.Name.Equals(y.Name) && x.Age == y.Age;
        }

        public int GetHashCode(Product obj)
        {
            return obj.GetHashCode();
        }
    }

    class Product
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}