#include <stdio.h>
#include <stdlib.h>
#include<Windows.h>

int Fun5() {
	//%-9d中的-表示左对齐，9表示至少占用9个字符单位
	printf("%-9d%-9d%-9d%-9d\n", 1, 100, 10000, 1000000);
	printf("%-9d%-9d%-9d%-9d\n", 10, 1000, 100000, 10000000);
	printf("%9d%9d%9d%9d\n", 1, 100, 10000, 1000000);
	printf("%9d%9d%9d%9d\n", 10, 1000, 100000, 10000000);
	//.7表示输出精度，输出小数时只显示7位小数超出部分四舍五入，输出字符串时只取前7位
	Sleep(5000);
	printf("%-15.7lf%-15.7lf%-15.7lf\n", 0.01, 1.01, 0.123456789);
	printf("%-10.7s%-10.7s\n", "abc", "abcdefghijklmn");
	system("pause");
	return 0;
}