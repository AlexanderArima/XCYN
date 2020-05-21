#include <stdio.h>
#include <stdlib.h>

int Fun2() {

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
	short i = 48;
	long k = 44;
	printf("%#ho %#hd %#hx\n", i, i, i);
	printf("%#o %#d %#x\n", h, h, h);
	printf("%#lo %#ld %#lX\n", k, k, k);

	float o = 1.22334455f;
	float p = 0.000005f;
	float q = 300000000;
	int r = 3.61;
	printf("%g\n%g\n%g\n%d\n", o, p, q, r);
	//system("pause");
	return 0;
}