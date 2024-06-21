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
        public static int Insert()
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
        public static void Query()
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
        /// <param name="predicate">筛选条件的Lambda表达式.</param>
        /// <param name="keySelector">排序的Lambda表达式.</param>
        /// <typeparam name="TKey">排序字段的类型.</typeparam>
        /// <returns>返回顾客列表.</returns>
        public static List<Customers> Sort<TKey>(Expression<Func<Customers, bool>> predicate, Expression<Func<Customers, TKey>> keySelector)
        {
            using (northwindEntities db = new northwindEntities())
            {
                return db.Customers.Where(predicate).OrderBy(keySelector).ToList();
            }
        }

        /// <summary>
        /// 分页查询.
        /// </summary>
        /// <param name="pageIndex">页签下标.</param>
        /// <param name="pageSize">每页显示个数.</param>
        /// <param name="predicate">筛选条件的Lambda表达式.</param>
        /// <param name="keySelector">排序的Lambda表达式.</param>
        /// <typeparam name="TKey">排序字段的类型.</typeparam>
        /// <returns>返回顾客列表.</returns>
        public static List<Customers> Query<TKey>(int pageIndex, int pageSize, Expression<Func<Customers, bool>> predicate, Expression<Func<Customers, TKey>> keySelector)
        {
            using (northwindEntities db = new northwindEntities())
            {
                return db.Customers.Where(predicate).OrderBy(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 修改.
        /// </summary>
        public static void Update()
        {
            using (northwindEntities db = new northwindEntities())
            {
                var model = db.Customers.Where(m => m.CustomerID == "zouqj").FirstOrDefault();
                if (model == null)
                {
                    Console.WriteLine("查询对象为空");
                    return;
                }

                Console.WriteLine("修改前的ContactName=" + model.ContactName);
                model.ContactName = "邹琼军";
                db.SaveChanges();
                Console.WriteLine("修改成功");
                Console.WriteLine("修改后的ContactName=" + model.ContactName);
            }
        }

        /// <summary>
        /// 优化后的修改，更具有通用性.
        /// </summary>
        /// <param name="predicate">筛选条件的Lambda表达式.</param>
        /// <param name="modified_customers">修改后的对象，想要修改成的对象.</param>
        public static void Update(Expression<Func<Customers, bool>> predicate, Customers modified_customers)
        {
            using (northwindEntities db = new northwindEntities())
            {
                var model = db.Customers.Where(predicate).FirstOrDefault();
                if (model == null)
                {
                    Console.WriteLine("查询对象为空");
                    return;
                }

                Console.WriteLine("修改前的ContactName=" + model.ContactName);
                Console.WriteLine("修改前的Country=" + model.Country);
                model.Country = modified_customers.Country;
                model.ContactName = modified_customers.ContactName;
                var flag = db.SaveChanges();
                Console.WriteLine("修改成功");
                Console.WriteLine("修改后的ContactName=" + model.ContactName);
                Console.WriteLine("修改后的Country=" + model.Country);
            }
        }

        /// <summary>
        /// 删除.
        /// </summary>
        /// <param name="predicate">筛选条件的Lambda表达式.</param>
        public static void Delete(Expression<Func<Customers, bool>> predicate)
        {
            using (northwindEntities db = new northwindEntities())
            {
                var model = db.Customers.Where(predicate).FirstOrDefault();
                if (model == null)
                {
                    Console.WriteLine("查询对象为空");
                    return;
                }

                db.Customers.Attach(model);
                db.Customers.Remove(model);
                var flag = db.SaveChanges();
                if (flag > 0)
                {
                    Console.WriteLine("删除成功");
                }
                else
                {
                    Console.WriteLine("删除失败");
                }
            }
        }
    }
}
