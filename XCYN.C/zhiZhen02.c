#include <stdio.h>

int Fun22() {
	//���岻ͬ����
	float a = 99.5, b = 10.6;
	char c = '@', d = 'a';
	//����ָ�����
	float *p1 = &a;	//����� "*" ��һ��������ţ�����һ��������ָ�������&��������ȡ��ַ��������Ϊȡ�����ĵ�ַ��
	char *p2 = &c;
	//�޸�ָ�����
	p1 = &b;
	p2 = &d;
	//ע��㣺����ָ�����ʱ�����*����ָ�������ֵʱ���ܴ�*

	//----------------ͨ��ָ�����ȡ������------------------//
	printf("p1��%f", *p1);	//�����*��Ϊָ�������������ȡ��ĳ����ַ�ϵ�����
	printf("p2��%c", *p2);
	//���ܱȽϣ�ʹ��ָ���Ǽ�ӻ�ȡ���ݣ�ʹ�ñ�������ֱ�ӻ�ȡ���ݣ�ǰ�߱Ⱥ��ߵĴ���Ҫ�ߡ�

	return 0;
}

int Fun23() {
	int a = 309, b = 903, temp = 0;
	int *pa = &a;
	int *pb = &b;
	//����ֵ
	temp = *pa;
	*pa = *pb;
	*pb = temp;
	//��ӡ
	printf("a��%d��b��%d", *pa, *pb);
	return 0;
}

int Fun24() {
	int a = 1;
	int *pa = &a;
	pa = &a;
	printf("pa��%d", *pa);
	return 0;
}