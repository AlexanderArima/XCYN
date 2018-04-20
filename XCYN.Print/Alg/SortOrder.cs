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

        #region 快速排序

        ///<summary>
        /// 分割函数
        ///</summary>
        ///<param name="list">待排序的数组</param>
        ///<param name="left">数组的左下标</param>
        ///<param name="right"></param>
        ///<returns></returns>
        public static int Division(List<int> list, int left, int right)
        {
            //首先挑选一个基准元素
            int baseNum = list[left];

            while (left < right)
            {
                //从数组的右端开始向前找，一直找到比base小的数字为止(包括base同等数)
                while (left < right && list[right] >= baseNum)
                    right = right - 1;

                //最终找到了比baseNum小的元素，要做的事情就是此元素放到base的位置
                list[left] = list[right];

                //从数组的左端开始向后找，一直找到比base大的数字为止（包括base同等数）
                while (left < right && list[left] <= baseNum)
                    left = left + 1;


                //最终找到了比baseNum大的元素，要做的事情就是将此元素放到最后的位置
                list[right] = list[left];
            }
            //最后就是把baseNum放到该left的位置
            list[left] = baseNum;

            //最终，我们发现left位置的左侧数值部分比left小，left位置右侧数值比left大
            //至此，我们完成了第一篇排序
            return left;
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void QuickSort(List<int> list, int left, int right)
        {
            //左下标一定小于右下标，否则就超越了
            if (left < right)
            {
                //对数组进行分割，取出下次分割的基准标号
                int i = Division(list, left, right);

                //对“基准标号“左侧的一组数值进行递归的切割，以至于将这些数值完整的排序
                QuickSort(list, left, i - 1);

                //对“基准标号“右侧的一组数值进行递归的切割，以至于将这些数值完整的排序
                QuickSort(list, i + 1, right);
            }
        }

        #endregion

        #region  选择排序
        
        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<int> SelectionSort(List<int> list)
        {
            //要遍历的次数
            for (int i = 0; i < list.Count - 1; i++)
            {
                //假设tempIndex的下标的值最小
                int tempIndex = i;

                for (int j = i + 1; j < list.Count; j++)
                {
                    //如果tempIndex下标的值大于j下标的值,则记录较小值下标j
                    if (list[tempIndex] > list[j])
                        tempIndex = j;
                }

                //最后将假想最小值跟真的最小值进行交换
                var tempData = list[tempIndex];
                list[tempIndex] = list[i];
                list[i] = tempData;
            }
            return list;
        }

        #endregion
    }
}
