#include <stdio.h>

int Fun22() {
	//定义不同变量
	float a = 99.5, b = 10.6;
	char c = '@', d = 'a';
	//定义指针变量
	float *p1 = &a;	//这里的 "*" 是一个特殊符号，表明一个变量是指针变量；&这里用作取地址符（作用为取变量的地址）
	char *p2 = &c;
	//修改指针变量
	p1 = &b;
	p2 = &d;
	//注意点：定义指针变量时必须带*，给指针变量赋值时不能带*

	//----------------通过指针变量取得数据------------------//
	printf("p1：%f", *p1);	//这里的*称为指针运算符，用来取得某个地址上的数据
	printf("p2：%c", *p2);
	//性能比较：使用指针是间接获取数据，使用变量名是直接获取数据，前者比后者的代价要高。

	return 0;
}

int Fun23() {
	int a = 309, b = 903, temp = 0;
	int *pa = &a;
	int *pb = &b;
	//交换值
	temp = *pa;
	*pa = *pb;
	*pb = temp;
	//打印
	printf("a：%d；b：%d", *pa, *pb);
	return 0;
}

int Fun24() {
	int a = 1;
	int *pa = &a;
	pa = &a;
	printf("pa：%d", *pa);
	return 0;
}