using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.GridView
{
    public class School
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string XXMC { get; set; }

        /// <summary>
        /// 学校列表
        /// </summary>
        public static string[] XXLB = new string[] {
            "华中科技大学",
            "武汉大学",
            "华中农业大学",
            "武汉理工大学",
            "武汉科技大学",
        };

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static List<School> Query()
        {
            List<School> list = new List<School>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                //生成随机数据
                School item = new School()
                {
                    ID = Guid.NewGuid().ToString(),
                    BH = random.Next(0,10).ToString() + random.Next(0, 10).ToString() + random.Next(0, 10).ToString() + random.Next(0, 10).ToString(),
                    XXMC = School.XXLB.ElementAt(random.Next(0, 4))
                };
                list.Add(item);
            }
            return list;
        }
    }
}
