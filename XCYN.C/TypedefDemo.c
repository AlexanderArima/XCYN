#include <stdio.h>
#define MAX(a,b) (a>b) ? a : b

int Fun16() 
{
	int x, y, max;
	printf("input two numbers: ");
	scanf("%d %d", &x, &y);
	max = MAX(x, y);
	printf("max=%d\n", max);
   return 0;
}

typedef char* nstring;		//ʹ��typedef������һ���ַ�ָ��
typedef char nstring100[100];		//ʹ��typedef������һ���ַ�����
typedef char (*nstringArray30)[30];		//ʹ��typedef������һ���ַ���ά���飬���ַ����б�

int Fun38() {
	nstring str = "Hello World";
	printf("%s\n", str);
	nstring100 str2 = "Who are you?";
	printf("%s\n", str2);

	char str3[3][30] = {
		"http://c.biancheng.net",
		"C����������",
		"C-Language"
	};
	nstringArray30 strArray3 = str3;
	for (int i = 0; i < 2; i++)
	{
		printf("%s\n", strArray3[i]);
	}
	return 0;
}

//��Ҫǿ�����ǣ�typedef �Ǹ�����������һ���µ����֣������Ǵ����µ����͡�
//Ϊ�ˡ�����֪�⡱���뾡��ʹ�ú�����ȷ�ı�ʶ�������Ҿ�����д��


