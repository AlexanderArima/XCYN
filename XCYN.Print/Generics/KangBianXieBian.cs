using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Generics
{
    public class KangBianXieBian
    {
        public void Fun1()
        {
            Person p = new Man();
            Person[] p2 = new Man[3];
            IEnumerable<Person> p3 = new List<Man>();

            //协变例子
            IEnergy<Person> p4 = new Person();
            IEnergy<Man> p5 = new Man();
            //p5 = p4;  
            p4 = p5;

            //抗变例子
            IPhysiclalStatus<Person> p7 = new Person();
            IPhysiclalStatus<Man> p8 = new Man();
            //p7 = p8;
            p8 = p7;

        }
    }

    /// <summary>
    /// 接口实现类
    /// </summary>
    public class Person : IEnergy<Person>, IPhysiclalStatus<Person>
    {
        //协变
        public Person Fun1()
        {
            throw new NotImplementedException();
        }

        //抗变
        public void Fun1(Person obj)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Person的子类
    /// </summary>
    public class Man : Person, IEnergy<Man>,IPhysiclalStatus<Man>
    {

        public void Fun1(Man obj)
        {
            throw new NotImplementedException();
        }

        Man IEnergy<Man>.Fun1()
        {
            throw new NotImplementedException();
        }
    }

    public interface IEnergy<out T>
    {
        //协变，支持隐式转换，可以将方法的返回值设置为T，不能把T作为输入参数
        T Fun1();
    }

    public interface IPhysiclalStatus<in T>
    {
        //抗变，支持强制转换，可以将T作为输入参数，不能把T作为返回值
        void Fun1(T obj);
    }
}
