#include <stdio.h>
#include <stdlib.h>
#include <wchar.h>
#include <locale.h>

int Fun3() {
//int main()	{
	int a = 'a';
	int b = 98;
	printf("a = %d %c\n", a, a);
	printf("b = %d %c\n", b, b);

	char c[] = "����һ������";
	char *d = "��ӡһ������";
	printf("c = %s\n", c);
	printf("d = %s\n", d);

	printf("I\'m Jim.\t He say \"Hurry Up\" \n");
	
	char e = 'һ';	//��ӡ�������룬��Ϊchar���͵�խ�ַ�����ASCII����
	printf("%c \n", e);
	//��Ҫ�����������Ҫ����һ��wchar_t���ͣ�������Ϳ���������ַ�������ǰ���L
	wchar_t f = L'һ';
	//�����ñ������Ի���Ϊ����
	setlocale(LC_ALL, "zh-CN");
	//�����ʹ��wprintf����������ǰ���L�����ַ���Ӧ��ռλ��Ϊ%lc
	wprintf(L"%lc \n", f);
	system("pause");
	return 0;
}