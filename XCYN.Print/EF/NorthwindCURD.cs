namespace XCYN.Print.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Northwind的增删改查操作类.
    /// </summary>
    internal class NorthwindCURD
    {
        /// <summary>
        /// 添加.
        /// </summary>
        /// <returns>返回影响行数.</returns>
        public static int Fun01()
        {
            using (northwindEntities db = new northwindEntities())
            {
                var customer = new Customers()
                {
                    CustomerID = "zouqj",
                    Address = "南山区新能源创新产业园",
                    City = "深圳",
                    CompanyName = "深圳跨境电商商务有限公司",
                    ContactName = "邹琼俊",
                };

                db.Customers.Add(customer);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 查询.
        /// </summary>
        public static void Fun02()
        {
            using (northwindEntities db = new northwindEntities())
            {
                // 查询顾客编号为zouqj的实体，并打印它的联系人名称
                var order = db.Customers.Where(m => m.CustomerID == "zouqj").FirstOrDefault();
                Console.WriteLine(order.ContactName);

                // 遍历所有的联系人
                var list_order = db.Orders;
                foreach (var item in list_order)
                {
                    Console.WriteLine(item.OrderID + "：ContactName=" + item.Customers.ContactName);
                }
            }
        }

        /// <summary>
        /// 排序&条件查询.
        /// </summary>
        public static List<Customers> Fun03<TKey>(Expression<Func<Customers, bool>> predicate, Expression<Func<Customers, TKey>> keySelector)
        {
            using (northwindEntities db = new northwindEntities())
            {
                return db.Customers.Where(predicate).OrderBy(keySelector).ToList();
            }
        }
    }
}
