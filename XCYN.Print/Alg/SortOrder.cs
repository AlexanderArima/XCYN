using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Alg
{
    public class SortOrder
    {
        /*
         首先排序分为四种： 

          交换排序： 包括冒泡排序，快速排序。

          选择排序： 包括直接选择排序，堆排序。

          插入排序： 包括直接插入排序，希尔排序。

          合并排序： 合并排序。
         */

        //我们都知道，C#类库提供的排序是快排

        /// <summary>
        /// 冒泡排序
        /// </summary>
        public static List<int> BubbleSort(List<int> list)
        {
            int temp;
            //第一层循环： 表明要比较的次数，比如list.count个数，肯定要比较count-1次
            for (int i = 0; i < list.Count - 1; i++)
            {
                //list.count - 1：取数据最后一个数下标，
                //j > i: 从后往前的的下标一定大于从前往后的下标，否则就超越了。
                for (int j = list.Count - 1; j > i; j--)
                {
                    //如果前面一个数大于后面一个数则交换
                    if (list[j - 1] > list[j])
                    {
                        temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }
    }
}
