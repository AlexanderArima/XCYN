#include <stdio.h>
#include <stdlib.h>
#include "LinkStack.h"

// ��ջ����
// ���� stack ��ջ��ͷ���
// ���� data ��Ҫ�����Ԫ��
linkStack * LinkPush(linkStack * stack, int num) {
	// ����һ����Ԫ�صĶ���
	linkStack * s = (linkStack *)malloc(sizeof(linkStack));
	s->data = num;

	//�½ڵ���ͷ�ڵ㽨���߼���ϵ
	s->next = stack;

	// ����ͷ����ָ��
	stack = s;
	printf("��ջԪ�أ�%d \n", num);
	return stack;
}

// ��ջ����
// ���� stack ��ջ��ͷ���
linkStack * LinkPop(linkStack * stack) {
	if (stack) {
		linkStack *s = stack;	//����ԭ��ͷ�������ݣ�Ϊ�˺�����ʾ��ջ��Ԫ��
		stack = stack->next ;	//���½ڵ�ָ����ڴ�ռ�
		printf("��ջԪ�أ�%d ��", s->data);
		if (stack) {
			printf("�µ�ջ��Ԫ�أ�%d \n", stack->data);
		}
		else {
			printf("�µ�ջ��û������Ԫ�� \n");
		}
		free(s);
	}
	else {
		printf("ջ��û��Ԫ�� \n");
		return stack;
	}
	return stack;
}