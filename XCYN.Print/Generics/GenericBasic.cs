using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Generics
{
    public class GenericBasic
    {
        /// <summary>
        /// 泛型特性1：类型检查
        /// </summary>
        public void Fun1()
        {
            List<int> list = new List<int>();
            list.Add(2);    //编译器报错
            //list.Add("a");

            ArrayList array = new ArrayList();
            array.Add(2);   //编译器不会报错
            array.Add("a");

            int sum = 0;
            for (int i = 0; i < array.Count; i++)
            {
                //虽然在编译阶段没有报错，可是在程序运行阶段却会抛出异常，因为字母无法转换为数字。。
                sum += Convert.ToInt32(array[i]);
            }
        }
    }
}
