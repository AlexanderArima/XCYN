#include <stdio.h>
#include <stdlib.h>

// 进栈
int push(int *a, int top, int elem) {
	top = top + 1;
	a[top] = elem;
	printf("进栈的元素为%d\n", elem);
	return top;
}

// 出栈
int pop(int *a, int top) {
	if (top <= -1) {
		printf("空栈\n");
		return -1;
	}

	top = top - 1;
	printf("出栈的元素为%d\n", a[top + 1]);
	return top;
}
