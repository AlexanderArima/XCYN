#include <stdio.h>
#include <stdlib.h>

int Fun4() {
	int a = 12;
	int b = 100;
	double c = 6.0;
	printf("b / a = %d\n", b / a);
	printf("b / c = %lf\n", b / c);
	printf("a / c = %lf\n", a / c);
	printf("a * b = %d\n", a * b);
	printf("a * c = %lf\n", a * c);
	printf("0 / c = %lf\n", 0 / c);
	//ȡ����
	printf("100 %% 12 = %d\n", 100 % 12);
	printf("100 %% -12 = %d\n", 100 % -12);
	printf("-100 %% 12 = %d\n", -100 % 12);
	printf("-100 %% -12 = %d\n", -100 % -12);
	//�� printf �У�% �Ǹ�ʽ���Ʒ��Ŀ�ͷ����һ��������ַ�������ֱ�������Ҫ����� %������������ǰ���ټ�һ�� %

	c = 6.2;
	int d = c;
	printf("d = %d\n", d);

	//ǿ������ת��������()�����ȼ�����/�����Ƚ�������ת����������������
	printf("b / a = %lf\n", (double)b / a);
	system("pause");
	return 0;
}