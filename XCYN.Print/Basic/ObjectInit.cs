using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    public class ObjectInit
    {
        public int id { get; set; }

        public string name { get; set; }

        public int age { get; set; }

        /// <summary>
        /// 对象初始化器
        /// </summary>
        public void Fun1()
        {
            //普通写法
            ObjectInit model = new ObjectInit();
            model.id = 1;
            model.name = "Scott";
            model.age = 19;

            //使用了对象初始化器
            ObjectInit model2 = new ObjectInit()
            {
                id = 2,
                name = "Halen",
                age = 13
            };
        }

        /// <summary>
        /// 集合初始化器
        /// </summary>
        public void Fun2()
        {
            //普通写法
            List<ObjectInit> list_normal = new List<ObjectInit>();
            ObjectInit model = new ObjectInit()
            {
                id = 2,
                name = "Halen",
                age = 13
            };
            list_normal.Add(model);
            ObjectInit model2 = new ObjectInit()
            {
                id = 3,
                name = "Rain",
                age = 16
            };
            list_normal.Add(model2);

            //使用了集合初始化器
            List<ObjectInit> list = new List<ObjectInit>()
            {
                new ObjectInit()
                {
                    id = 2,
                    name = "Halen",
                    age = 13
                },
                new ObjectInit()
                {
                    id = 3,
                    name = "Rain",
                    age = 16
                }
            };

        }
    }
}
