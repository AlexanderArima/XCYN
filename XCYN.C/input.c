#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>

//int Fun6() {
int main() {
	int a, b = 0;
	//&称为取地址符，也就是获取变量在内存中的地址
	//scanf()：和 printf() 类似，scanf() 可以输入多种类型的数据。
	//scanf("%d %d", &a, &b);
	//printf("a + b = %d\n", a + b);

	//打印地址
	//char c = ' ';
	//printf("a& = %p b& = %p c& = %p\n", &a, &b, &c);

	//getchar()是scanf()的替代品
	//char d = getchar();
	//printf("d = %c\n", d);

	//_getche()方法和getchar()的区别在于没有缓冲区，不用按Enter键就能输出在控制台上
	//char e = _getche();
	//printf("e = %c\n", e);

	//_getch()方法和_getche()方法的区别在于，它不会回显用户的输入字符，在有些场合需要这种特性，比如输入密码就得防止别人偷窥
	//char f = _getch();
	//printf("f = %c\n", f);

	//gets()方法和scanf()的区别在于gets()只能输入字符串类型并且可以打印空格，scanf()可以输入多种类型，但不能打印空格
	char g[20];
	gets(g);
	//scanf("%s", g);
	printf("g = %s\n", g);
	system("pause");
	return 0;
}