#include <stdio.h>
#include <stdlib.h>

int Fun3() {
	int a = 'a';
	int b = 98;
	printf("a = %d %c\n", a, a);
	printf("b = %d %c\n", b, b);

	char c[] = "����һ������";
	char *d = "��ӡһ������";
	printf("c = %s\n", c);
	printf("d = %s\n", d);

	printf("I\'m Jim.\t He say \"Hurry Up\" \n");
	printf("hello");
	system("pause");
	return 0;
}