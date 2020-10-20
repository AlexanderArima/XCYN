#include <stdio.h>
#include <string.h>

int Fun25() {
	int arr[] = {10, 20, 30, 40, 50};
	int length = sizeof(arr) / sizeof(int);
	printf("正常地打印数组中的元素\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//正常地打印数组中的元素
		printf("%d\n", arr[i]);
	}
	printf("使用数组指针打印数组中的元素\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//使用数组指针打印数组中的元素
		printf("%d\n", *(arr + i));	//arr 本身就是一个指针，可以直接赋值给指针变量 p。arr 是数组第 0 个元素的地址
	}
	int *p = &arr[0];
	printf("使用数组指针（数组第一个元素的下标即数组下标）打印数组中的元素\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//使用数组指针（数组第一个元素的下标即数组下标）打印数组中的元素
		printf("%d\n", *(p + i));
	}
	printf("数组名（arr）与数组指针（p）的区别：arr是常量，p是变量，p可以自增1");
	for (int i = 0; i <= length - 1; i++)
	{
		//数组名 (arr) 是常量，它的值不能改变，而数组指针 (p) 是变量（除非特别指明它是常量），
		//它的值可以任意改变。也就是说，数组名只能指向数组的开头，
		//而数组指针可以先指向数组开头，再指向其他元素。
		printf("%d\n", *(p++));
	}
	return 0;
}

int Fun26() {
	//C语言有两种表示字符串的方法：使用字符数组和使用字符串常量，前者可以读取和修改，后者只能读取
	//（原因：字符数组存储在全局数据区或栈区，第二种形式的字符串存储在常量区。全局数据区和栈区的字符串（也包括其他数据）有读取和写入的权限，而常量区的字符串（也包括其他数据）只有读取权限，没有写入权限。）
	char str[] = "http://c.biancheng.net";
	char *str2 = "http://c.biancheng.net";
	printf("str：%s", str);
	printf("\n");
	printf("str2：%s", str2);
	printf("\n");
	int length = strlen(str);
	printf("使用字符串数组输出：");
	for (int i = 0; i < length; i++) {
		printf("%c", str[i]);
	}
	return 0;
}

int Fun27() {
	char str[] = "http://c.biancheng.net";
	char *str2 = "http://c.biancheng.net";
	str[4] = 's';
	printf("str：%s", str);	//将:改成s
	printf("\n");
	//str2[4] = 's';
	//printf("str2：%s", str2);	//将:改成s，由于str2是一个常量，所以执行到这里会报错（引发了异常: 写入访问权限冲突。）
	str2 = "http://www.baidu.com";	//将另一个常量赋值给str2，这样它就指向了另一个内存的指针
	printf("str2：%s", str2);
	return 0;
}

//交换两个数字的值
int Fun28(int *num1,int *num2) {
	printf("*num1：%d", *num1);	//这里的num1和num2是一个指针变量
	int temp;
	temp = *num1;	//通过指针变量获取内存上的数据
	*num1 = *num2;	//通过指针变量获取，修改内存上的数据
	*num2 = temp;	//通过指针变量修改内存上的数据
	return 0;
}

//取出数组中的最大值
int Fun29(int arr[], int len) {
	int max = arr[0];
	for (int i = 1; i < len; i++)
	{
		if (arr[i] > max) 
		{
			max = arr[i];
		}
	}
	return max;
}

//返回字符串的指针
char *Fun30(char *str1, char *str2) {
	if (strlen(str1) >= strlen(str2)) {
		return str1;
	}
	else {
		return str2;
	}
}

//指向指针的指针
int Fun31() {
	int a = 100;
	int *p1 = &a;
	int **p2 = &p1;
	int ***p3 = &p2;
	printf("%d, %d, %d, %d\n", a, *p1, **p2, ***p3);
	//&a得到a的地址，p1=&a，*p2指向p1，p1又等于&a，所以它们的地址是同一个
	printf("&a：%#X，p1：%#X，*p2：%#X，**p3：%#X\n", &a, p1, *p2, **p3);
	//&p1得到的是p1的地址，p2存放的值为p1的地址，*p3指向p2的值，也就是p1的地址
	printf("&p1：%#X，p2：%#X，*p3：%#X\n", &p1, p2, *p3);
	printf("&p2：%#X，p3：%#X", &p2, p3);
	return 0;
}

int Fun32() {
	char *str = NULL;		//强烈建议对没有初始化的指针赋值为 NULL
	char *str_arr[10] = { '\0' };
	str = &str_arr;
	if (str == NULL) {
		//这样能够从很大程度上增加程序的健壮性，防止对空指针进行无意义的操作。
		printf("请初始化字符串!");
		return 0;
	}
	gets(str);
	printf("%s\n", str);
	return 0;
}