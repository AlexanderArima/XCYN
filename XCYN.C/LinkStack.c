#include <stdio.h>
#include <stdlib.h>
#include "LinkStack.h"

// 入栈操作
// 参数 stack 是栈的头结点
// 参数 data 是要插入的元素
linkStack * LinkPush(linkStack * stack, int num) {
	// 创建一个新元素的对象
	linkStack * s = (linkStack *)malloc(sizeof(linkStack));
	s->data = num;

	//新节点与头节点建立逻辑关系
	s->next = stack;

	// 更新头结点的指针
	stack = s;
	printf("入栈元素：%d \n", num);
	return stack;
}

// 出栈操作
// 参数 stack 是栈的头结点
linkStack * LinkPop(linkStack * stack) {
	if (stack) {
		linkStack *s = stack;	//保存原来头结点的数据，为了后续显示出栈的元素
		stack = stack->next ;	//更新节点指向的内存空间
		printf("出栈元素：%d ，", s->data);
		if (stack) {
			printf("新的栈顶元素：%d \n", stack->data);
		}
		else {
			printf("新的栈顶没有其他元素 \n");
		}
		free(s);
	}
	else {
		printf("栈内没有元素 \n");
		return stack;
	}
	return stack;
}