#include <stdio.h>
#include <stdlib.h>
#include<Windows.h>

int main() {
	int a, b = 0;
	//&��Ϊȡ��ַ����Ҳ���ǻ�ȡ�������ڴ��еĵ�ַ
	//scanf()���� printf() ���ƣ�scanf() ��������������͵����ݡ�
	//scanf("%d %d", &a, &b);
	//printf("a + b = %d\n", a + b);

	//��ӡ��ַ
	char c = ' ';
	printf("a& = %p b& = %p c& = %p\n", &a, &b, &c);
	system("pause");
	return 0;
}