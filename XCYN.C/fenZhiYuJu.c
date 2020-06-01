#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>

int Fun7() {
	QiuZuiDaZhi();
	return 0;
}

int QiuZuiDaZhi() {
	int a;
	int b;
	int max;
	printf("请输入一个数字 \n");
	scanf("%d", &a);
	printf("请再输入一个数字 \n");
	scanf("%d", &b);
	if (a > b)	max = a;
	else	max = b;
	printf("%d和%d中最大的值是%d", a, b, max);
}
