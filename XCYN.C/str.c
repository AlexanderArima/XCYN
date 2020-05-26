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

	char c[] = "生成一段文字";
	char *d = "打印一段文字";
	printf("c = %s\n", c);
	printf("d = %s\n", d);

	printf("I\'m Jim.\t He say \"Hurry Up\" \n");
	
	char e = '一';	//打印的是乱码，因为char类型的窄字符是用ASCII编码
	printf("%c \n", e);
	//想要输出中文首先要定义一个wchar_t类型，这个类型可以输出宽字符，并在前面加L
	wchar_t f = L'一';
	//并设置本地语言环境为中文
	setlocale(LC_ALL, "zh-CN");
	//输出得使用wprintf函数，并在前面加L，宽字符对应的占位符为%lc
	wprintf(L"%lc \n", f);
	system("pause");
	return 0;
}