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
	//取余数
	printf("100 %% 12 = %d\n", 100 % 12);
	printf("100 %% -12 = %d\n", 100 % -12);
	printf("-100 %% 12 = %d\n", -100 % 12);
	printf("-100 %% -12 = %d\n", -100 % -12);
	//在 printf 中，% 是格式控制符的开头，是一个特殊的字符，不能直接输出；要想输出 %，必须在它的前面再加一个 %

	c = 6.2;
	int d = c;
	printf("d = %d\n", d);

	//强制类型转换，由于()的优先级高于/所以先进行类型转换，再做除法运算
	printf("b / a = %lf\n", (double)b / a);
	system("pause");
	return 0;
}