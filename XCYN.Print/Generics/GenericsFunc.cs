using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Generics
{
    public class GenericsFunc
    {
        public void Swap<T>(ref T x, ref T y)
        {
            var logger = LogManager.GetLogger(typeof(GenericsFunc));
            logger.Info("哈哈哈");
            T temp = x;
            x = y;
            y = temp;
        }
    }

    //普通类
    public class Account
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Account(string name,decimal balance)
        {
            Name = name;
            Balance = balance;
        }
    }

    //实现接口
    public class GnrAccount : IAccount
    {
        public string Name { get; set; }
        public decimal Balance { get ; set; }
        public GnrAccount(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }
    }

    /// <summary>
    /// 泛型接口
    /// </summary>
    public interface IAccount
    {
        string Name { get; set; }

        decimal Balance { get; set; }
    }

    public static class Algorithms
    {
        //不使用泛型接口实现，只能处理Account这一个对象
        public static decimal AccumlateSimple(IEnumerable<Account> source)
        {
            decimal sum = 0;
            foreach (var item in source)
            {
                sum += item.Balance;
            }
            return sum;
        }

        //使用泛型接口实现，只要实现了IAccount接口都能通用
        public static decimal AccumlateSimple<TAccount>(IEnumerable<TAccount> source)
            where TAccount : IAccount
        {
            decimal sum = 0;
            foreach (var item in source)
            {
                sum += item.Balance;
            }
            return sum;
        }
    }

}
