#include <stdio.h>
#include <stdlib.h>

int main() {
	//��ֵ��ָ�����ݷŵ��ڴ�Ĺ���
	//������������˵�����ݵ����ͣ�ȷ�������ݵĽ��ͷ�ʽ���ü�����ͳ���Ա�����������
	//int a, b, c;
	//int d = 1, e = 2;
	//int f, g = 3;
	//puts("Hello world");

	//printf������������ַ���������������֣�С��
	//printf("%s��%d��%d��","����", 5, 16);
	
	//�������������ַ���
	printf("C������������һ��ѧϰC���Ժ�C++����վ�����Ǽ���ù����ľ�������ĥÿһ�׽̡̳�"
		"�������һ�����飬�������£����Լ��ж������û��Ķ�����������Դ�������Ʒ��"
		"C��������������ַ�ǣ�http://c.biancheng.net \n");

	//int �ǻ������������ͣ�short �� long ���� int �Ļ����Ͻ��е���չ��short ���Խ�ʡ�ڴ棬long �������ɸ����ֵ��

	//����ռ���ֽ���
	short a = 100;
	int b = 0;
	
	int short_length = sizeof(a);
	int int_length = sizeof b;
	int char_length = sizeof(char);
	int long_length = sizeof(long);
	printf("short=%d��int=%d��char=%d��long=%d\n", short_length, int_length, char_length, long_length);
	//32λ������64λWindows������shortռ2���ֽڣ�int��longռ4���ֽڣ�charռ1���ֽ�
	//��64λLinux��Mac os������shortռ2���ֽڣ�intռ4���ֽڣ�longռ8���ֽڣ�charռ1���ֽ�

	//%hd�������short decimal���ͣ������ͣ�
	//%ld�������long deicimal���ͣ������ͣ�
	long l = 333;
	printf("%hd %ld\n", a, l);

	//�������� 0 �� 1 ����������ɣ�ʹ��ʱ������0b��0B�������ִ�Сд����ͷ
	int m = 0B10;
	printf("%d", m);
	//�˽����� 0~7 �˸�������ɣ�ʹ��ʱ������0��ͷ��ע�������� 0��������ĸ o��
	int n = 070;
	printf("%d\n", n);
	//ʮ������������ 0~9����ĸ A~F �� a~f�������ִ�Сд����ɣ�ʹ��ʱ������0x��0X�������ִ�Сд����ͷ
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