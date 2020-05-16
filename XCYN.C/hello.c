#include <stdio.h>
#include <stdlib.h>

int main() {
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

	//类型占的字节数
	short a = 100;
	int b = 0;
	
	int short_length = sizeof(a);
	int int_length = sizeof b;
	int char_length = sizeof(char);
	int long_length = sizeof(long);
	printf("short=%d，int=%d，char=%d，long=%d\n", short_length, int_length, char_length, long_length);
	//32位环境及64位Windows环境下short占2个字节，int，long占4个字节，char占1个字节
	//在64位Linux和Mac os环境下short占2个字节，int占4个字节，long占8个字节，char占1个字节

	//%hd用来输出short decimal类型（短整型）
	//%ld用来输出long deicimal类型（长整型）
	long l = 333;
	printf("%hd %ld\n", a, l);

	//二进制由 0 和 1 两个数字组成，使用时必须以0b或0B（不区分大小写）开头
	int m = 0B10;
	printf("%d", m);
	//八进制由 0~7 八个数字组成，使用时必须以0开头（注意是数字 0，不是字母 o）
	int n = 070;
	printf("%d\n", n);
	//十六进制由数字 0~9、字母 A~F 或 a~f（不区分大小写）组成，使用时必须以0x或0X（不区分大小写）开头
	int g = 0XA0;
	printf("%d\n", g);

	int h = 48;
	short i= 48;
	long k = 44;
	printf("%#ho %#hd %#hx\n", i, i, i);
	printf("%#o %#d %#x\n", h, h, h);
	printf("%#lo %#ld %#lX\n", k, k, k);
	system("pause");
	return 0;
}