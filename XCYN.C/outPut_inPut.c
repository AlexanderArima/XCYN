#include <stdio.h>
#include <stdlib.h>
#include<Windows.h>

int Fun5() {
	//%-9d�е�-��ʾ����룬9��ʾ����ռ��9���ַ���λ
	printf("%-9d%-9d%-9d%-9d\n", 1, 100, 10000, 1000000);
	printf("%-9d%-9d%-9d%-9d\n", 10, 1000, 100000, 10000000);
	printf("%9d%9d%9d%9d\n", 1, 100, 10000, 1000000);
	printf("%9d%9d%9d%9d\n", 10, 1000, 100000, 10000000);
	//.7��ʾ������ȣ����С��ʱֻ��ʾ7λС�����������������룬����ַ���ʱֻȡǰ7λ
	Sleep(5000);
	printf("%-15.7lf%-15.7lf%-15.7lf\n", 0.01, 1.01, 0.123456789);
	printf("%-10.7s%-10.7s\n", "abc", "abcdefghijklmn");
	system("pause");
	return 0;
}