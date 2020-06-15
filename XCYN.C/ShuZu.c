#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>

//数组是一个整体，它的内存是连续的；也就是说，数组元素之间是相互挨着的，彼此之间没有一点点缝隙。

int Fun8() {
	int i = 0;
	int list[10];
	printf("请输入10个数字\n");
	for (; i < 10; i++)
	{
		scanf("%d", &list[i]);	//scanf读取输入数据时需要的是一个地址，所以在list[i]前面添加&符号，&会将一个具体的值转成地址 
	}
	i = 0;
	printf("输入完毕，开始输出...\n");
	for (;	 i < 10; i++)
	{
		printf("%d \n", list[i]);
	}
	printf("输出完毕\n");
	system("pause");
	return 0;
}
