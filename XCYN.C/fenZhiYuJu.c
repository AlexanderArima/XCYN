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
	printf("������һ������ \n");
	scanf("%d", &a);
	printf("��������һ������ \n");
	scanf("%d", &b);
	if (a > b)	max = a;
	else	max = b;
	printf("%d��%d������ֵ��%d", a, b, max);
}
