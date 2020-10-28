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

typedef char* nstring;		//使用typedef定义了一个字符指针
typedef char nstring100[100];		//使用typedef定义了一个字符数组
typedef char (*nstringArray30)[30];		//使用typedef定义了一个字符二维数组，即字符串列表

int Fun38() {
	nstring str = "Hello World";
	printf("%s\n", str);
	nstring100 str2 = "Who are you?";
	printf("%s\n", str2);

	char str3[3][30] = {
		"http://c.biancheng.net",
		"C语言中文网",
		"C-Language"
	};
	nstringArray30 strArray3 = str3;
	for (int i = 0; i < 2; i++)
	{
		printf("%s\n", strArray3[i]);
	}
	return 0;
}

//需要强调的是，typedef 是赋予现有类型一个新的名字，而不是创建新的类型。
//为了“见名知意”，请尽量使用含义明确的标识符，并且尽量大写。


