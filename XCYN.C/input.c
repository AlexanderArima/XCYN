#include <stdio.h>
#include <stdlib.h>
#include<Windows.h>

int main() {
	int a, b = 0;
	//&称为取地址符，也就是获取变量在内存中的地址
	//scanf()：和 printf() 类似，scanf() 可以输入多种类型的数据。
	//scanf("%d %d", &a, &b);
	//printf("a + b = %d\n", a + b);

	//打印地址
	char c = ' ';
	printf("a& = %p b& = %p c& = %p\n", &a, &b, &c);
	system("pause");
	return 0;
}