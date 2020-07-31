#include <stdio.h>

//冒泡排序
void BubbleSort() {
	int arr[10] = {0, 2, 4, 6, 8, 1, 3, 5, 7, 9};
	int temp = 0;
	int compareCount = 0;
	int isSort = 1;	//标记数组是否是排序好了的
	for (int i = 0; i < 10 - 1; i++)	
	{
		isSort = 1;
		//数组共有10个元素，拿第一个数比较第二个数，第二个数比较第三个数....第n - 1个数比较第n个数，比较的次数为n - 1
		for (int j = 1; j < 10 - 1 - i; j++)
		{
			//第二层循环是控制被比较的数，它永远比比比较的数下标大1
			int before = arr[j - 1];
			int after = arr[j];	//after的下标比before大1
			if (before > after) {
				//交换位置
				temp = before;
				arr[j - 1] = arr[j];
				arr[j] = temp;
				isSort = 0;
			}
			compareCount++;
		}
		if (isSort == 1) {
			//如果是排序好的，那么就不用再去排序了，直接跳出即可
			break;
		}
	}
	for (int i = 0; i < 10; i++)
	{
		printf("%d", arr[i]);
	}
	printf("\n");
	printf("比较次数：%d", compareCount);
}