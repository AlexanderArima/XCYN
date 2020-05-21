#include <stdio.h>
#include <stdlib.h>

int Fun1() {
	//赋值是指把数据放到内存的过程
	//数据类型用来说明数据的类型，确定了数据的解释方式，让计算机和程序员不会产生歧义
	//int a, b, c;
	//int d = 1, e = 2;
	//int f, g = 3;
	//puts("Hello world");

	//printf不仅可以输出字符串，还能输出数字，小数
	//printf("%s是%d月%d号","明天", 5, 16);
	
	//如何输出超长的字符串
	printf("C语言中文网，一个学习C语言和C++的网站，他们坚持用工匠的精神来打磨每一套教程。"
		"坚持做好一件事情，做到极致，让自己感动，让用户心动，这就是足以传世的作品！"
		"C语言中文网的网址是：http://c.biancheng.net \n");

	//int 是基本的整数类型，short 和 long 是在 int 的基础上进行的扩展，short 可以节省内存，long 可以容纳更大的值。

	//system("pause");
	return 0;
}