#include <stdio.h>
#include <stdlib.h>

// ��ջ
int push(int *a, int top, int elem) {
	top = top + 1;
	a[top] = elem;
	printf("��ջ��Ԫ��Ϊ%d\n", elem);
	return top;
}

// ��ջ
int pop(int *a, int top) {
	if (top <= -1) {
		printf("��ջ\n");
		return -1;
	}

	top = top - 1;
	printf("��ջ��Ԫ��Ϊ%d\n", a[top + 1]);
	return top;
}
