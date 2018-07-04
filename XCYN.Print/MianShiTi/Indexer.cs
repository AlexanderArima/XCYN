using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    /// <summary>
    /// 索引器
    /// </summary>
    public class Indexer
    {
        //索引器的参数不限定为整形
        public string this[string name]
        {
            get
            {
                switch (name)
                {
                    case "a":
                        return "安徽";
                    case "b":
                        return "北京";
                    case "c":
                        return "常山";
                    default:
                        return "";
                }
            }
        }

        private List<int> list = new List<int>();

        //索引器可以重载哦
        public int this[int price]
        {
            get
            {
                return list[price];
            }
            set
            {
                list.Insert(price,value);
            }
        }
    }

}
