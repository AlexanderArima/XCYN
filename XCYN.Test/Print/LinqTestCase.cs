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
            },new ProductEqualityComparer());
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

        [TestMethod]
        public void TestFirstOrDefault()
        {
            Product[] list = new Product[] {
            };

            Assert.AreEqual(default(string), list.FirstOrDefault());
            
        }
        
        [TestMethod]
        public void TestLast()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "Wu"
            },
            new Product
            {
                Name = "Li"
            }
            };

            Assert.AreEqual("Li", list.Last().Name);
            Assert.AreEqual("Wu", list.Last(m => m.Name.Equals("Wu")).Name);
            try
            {
                var last = list.Last(m => m.Name.Equals("Cheng"));
            }
            catch(Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void TestLastOrDefault()
        {
            Product[] list = new Product[] {
            };

            Assert.AreEqual(default(string), list.LastOrDefault());

        }

        [TestMethod]
        public void TestMaxAndMin()
        {
            Product[] list = new Product[] {
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                },
            };

            Assert.AreEqual(20, list.Max(m => m.Age));
            Assert.AreEqual(10, list.Min(m => m.Age));
        }

        [TestMethod]
        public void TestOrderBy()
        {
            Product[] list = new Product[] {
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                },
                new Product
                {
                    Name = "Anne",
                    Age = 15,
                },
                 new Product
                {
                    Name = "Worker",
                    Age = 15,
                },
            };

            Assert.AreEqual("Jack", list.OrderBy(m => m.Age).First().Name);
            Assert.AreEqual("John", list.OrderByDescending(m => m.Age).First().Name);
            Assert.AreEqual("Worker", list.OrderBy(m => m.Name,new ProductComparer()).First().Name);
            Assert.AreEqual("Jim", list.OrderByDescending(m => m.Name, new ProductComparer()).First().Name);
        }


    }

    class ProductComparer : IComparer<string>
    {
        /// <summary>
        /// 求出最常的字符串
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            return -(x.Length - y.Length);
        }
    }

    class ProductEqualityComparer : IEqualityComparer<Product>
    {
        //如果名字和年龄都相等则这两个对象是相等的。
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