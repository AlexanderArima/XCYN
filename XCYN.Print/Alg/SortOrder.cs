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

        #region 堆排序

        ///<summary>
        /// 构建堆
        ///</summary>
        ///<param name="list">待排序的集合</param>
        ///<param name="parent">父节点</param>
        ///<param name="length">输出根堆时剔除最大值使用</param>
        public static void HeapAdjust(List<int> list, int parent, int length)
        {
            //temp保存当前父节点
            int temp = list[parent];

            //得到左孩子(这可是二叉树的定义，大家看图也可知道)
            int child = 2 * parent + 1;

            while (child < length)
            {
                //如果parent有右孩子，则要判断左孩子是否小于右孩子
                if (child + 1 < length && list[child] < list[child + 1])
                    child++;

                //父亲节点大于子节点，就不用做交换
                if (temp >= list[child])
                    break;

                //将较大子节点的值赋给父亲节点
                list[parent] = list[child];

                //然后将子节点做为父亲节点，已防止是否破坏根堆时重新构造
                parent = child;

                //找到该父亲节点较小的左孩子节点
                child = 2 * parent + 1;
            }
            //最后将temp值赋给较大的子节点，以形成两值交换
            list[parent] = temp;
        }

        ///<summary>
        /// 堆排序
        ///</summary>
        ///<param name="list"></param>
        public static void HeapSort(List<int> list)
        {
            //list.Count/2-1:就是堆中父节点的个数
            for (int i = list.Count / 2 - 1; i >= 0; i--)
            {
                HeapAdjust(list, i, list.Count);
            }

            //最后输出堆元素
            for (int i = list.Count - 1; i > 0; i--)
            {
                //堆顶与当前堆的第i个元素进行值对调
                int temp = list[0];
                list[0] = list[i];
                list[i] = temp;

                //因为两值交换，可能破坏根堆，所以必须重新构造
                HeapAdjust(list, 0, i);
            }
        }

        #endregion

        #region 插入排序

        public static void InsertSort(List<int> list)
        {
            //无须序列
            for (int i = 1; i < list.Count; i++)
            {
                var temp = list[i];

                int j;

                //有序序列
                for (j = i - 1; j >= 0 && temp < list[j]; j--)
                {
                    list[j + 1] = list[j];
                }
                list[j + 1] = temp;
            }
        }

        #endregion

        #region 希尔排序

        ///<summary>
        /// 希尔排序
        ///</summary>
        ///<param name="list"></param>
        public static void ShellSort(List<int> list)
        {
            //取增量
            int step = list.Count / 2;

            while (step >= 1)
            {
                //无须序列
                for (int i = step; i < list.Count; i++)
                {
                    var temp = list[i];

                    int j;

                    //有序序列
                    for (j = i - step; j >= 0 && temp < list[j]; j = j - step)
                    {
                        list[j + step] = list[j];
                    }
                    list[j + step] = temp;
                }
                step = step / 2;
            }
        }

        #endregion

        #region 归并排序

        ///<summary>
        /// 数组的划分
        ///</summary>
        ///<param name="array">待排序数组</param>
        ///<param name="temparray">临时存放数组</param>
        ///<param name="left">序列段的开始位置，</param>
        ///<param name="right">序列段的结束位置</param>
        static void MergeSort(int[] array, int[] temparray, int left, int right)
        {
            if (left < right)
            {
                //取分割位置
                int middle = (left + right) / 2;

                //递归划分数组左序列
                MergeSort(array, temparray, left, middle);

                //递归划分数组右序列
                MergeSort(array, temparray, middle + 1, right);

                //数组合并操作
                Merge(array, temparray, left, middle + 1, right);
            }
        }

        ///<summary>
        /// 数组的两两合并操作
        ///</summary>
        ///<param name="array">待排序数组</param>
        ///<param name="temparray">临时数组</param>
        ///<param name="left">第一个区间段开始位置</param>
        ///<param name="middle">第二个区间的开始位置</param>
        ///<param name="right">第二个区间段结束位置</param>
        static void Merge(int[] array, int[] temparray, int left, int middle, int right)
        {
            //左指针尾
            int leftEnd = middle - 1;

            //右指针头
            int rightStart = middle;

            //临时数组的下标
            int tempIndex = left;

            //数组合并后的length长度
            int tempLength = right - left + 1;

            //先循环两个区间段都没有结束的情况
            while ((left <= leftEnd) && (rightStart <= right))
            {
                //如果发现有序列大，则将此数放入临时数组
                if (array[left] < array[rightStart])
                    temparray[tempIndex++] = array[left++];
                else
                    temparray[tempIndex++] = array[rightStart++];
            }

            //判断左序列是否结束
            while (left <= leftEnd)
                temparray[tempIndex++] = array[left++];

            //判断右序列是否结束
            while (rightStart <= right)
                temparray[tempIndex++] = array[rightStart++];

            //交换数据
            for (int i = 0; i < tempLength; i++)
            {
                array[right] = temparray[right];
                right--;
            }
        }

        #endregion
    }
}
