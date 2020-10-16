#include <stdio.h>
#include <stdlib.h>

int Fun20() {
	int a = 100;
	char str[20] = "c.biancheng.net";
	printf("%#X, %#X\n", &a, str);
	return 0;
}

int a;

int Fun21() {
	int *p = &a;
	printf("%#X, %d\n", p, sizeof(int*));
	system("pause");
	return 0;
}