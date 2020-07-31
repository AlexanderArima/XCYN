#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>
#include <string.h>

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

//二维数组
int Fun9() {
	int list[2][3]  = { {60, 70, 80}, {70, 80, 90} };
	for (int i = 0; i < 2; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			printf("%d ", list[i][j]);
		}
		printf("\n");
	}
	return 0;
}

int Fun10() {

	//char str[] = "http://c.biancheng.net/c/";
	//long len = strlen(str);
	//printf("The lenth of the string is %ld.\n", len);

	char userName[] = "hello World";	//初始化字符串
	long len;
	len = strlen(userName);
	printf("%s" , userName);

	//char str[30] = { 0 };	//用\0初始化数组
	//char c;
	//int i;
	//for (c = 65, i = 0; c <= 90; c++ , i++)
	//{
	//	//输出A到Z
	//	str[i] = c;
	//}
	//printf("%s\n", str);
	return 0;
}

///字符串拼接
void Fun11() {
	char a[100] = "The URL is ";
	char b[50];
	printf("请输入URL\n");
	gets(b);	//获取字符串的输入
	strcat(a, b);	//将b拼接到a的后面
	puts(a);	//打印字符串
}

///字符串复制
void Fun12() {
	char a[100] = "000000000";
	char b[100] = "奥克斯";
	strcpy(a, b);	//用字符串b覆盖a
	puts(a);
	
}

///字符串比较
void Fun13() {
	char a[] = "0000";
	char b[] = "0";
	int result =strcmp(a, b);	//a大于b，返回1，b大于a返回-1，a等于b返回0
	printf("The result is %d", result);
}

void display_array(int arr[] ,int len) {
	for (int i = 0; i < len; i++)
	{
		printf("%d ", arr[i]);
	}
	printf("\n");
}

//删除指定下标的元素
void Fun14() {
	int arr[10] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
	int arr_new1[9];
	//将第六个元素删除
	for (int i = 0; i < 10; i++)
	{
		if (i < 5) {
			arr_new1[i] = arr[i];
		}
		else if (i > 5) {
			arr_new1[i - 1] = arr[i];	//跳过第6个元素
		}
	}
	display_array(arr_new1, 9);
}
