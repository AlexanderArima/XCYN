#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>

//int Fun6() {
int main() {
	int a, b = 0;
	//&��Ϊȡ��ַ����Ҳ���ǻ�ȡ�������ڴ��еĵ�ַ
	//scanf()���� printf() ���ƣ�scanf() ��������������͵����ݡ�
	//scanf("%d %d", &a, &b);
	//printf("a + b = %d\n", a + b);

	//��ӡ��ַ
	//char c = ' ';
	//printf("a& = %p b& = %p c& = %p\n", &a, &b, &c);

	//getchar()��scanf()�����Ʒ
	//char d = getchar();
	//printf("d = %c\n", d);

	//_getche()������getchar()����������û�л����������ð�Enter����������ڿ���̨��
	//char e = _getche();
	//printf("e = %c\n", e);

	//_getch()������_getche()�������������ڣ�����������û��������ַ�������Щ������Ҫ�������ԣ�������������͵÷�ֹ����͵��
	//char f = _getch();
	//printf("f = %c\n", f);

	//gets()������scanf()����������gets()ֻ�������ַ������Ͳ��ҿ��Դ�ӡ�ո�scanf()��������������ͣ������ܴ�ӡ�ո�
	char g[20];
	gets(g);
	//scanf("%s", g);
	printf("g = %s\n", g);
	system("pause");
	return 0;
}