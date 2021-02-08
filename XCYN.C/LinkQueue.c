#include <stdio.h>
#include <stdlib.h>
#include "LinkQueue.h"

// 初始化头结点
linkQueue * LinkQueueInit() {
	//创建一个头结点
	linkQueue *q = (linkQueue *)malloc(sizeof(linkQueue));

	//头结点初始化
	q->next = NULL;
	printf("队列初始化完毕\n");
	return q;
}

// 入队操作
// 参数q是原队列
// 参数data是数据
linkQueue * LinkQueueEntry(linkQueue * rear, int data) {
	// 声明一个新节点，将会将它放到尾部
	linkQueue *temp = (linkQueue *)malloc(sizeof(linkQueue));
	temp->next = NULL;
	temp->data = data;

	//新节点与原队列建立逻辑关系，原队列的next指向NULL，现在改成指向新节点temp了
	rear->next = temp;

	//让原节点指向新节点，这样他的next指针还是指向NULL，仍然是尾节点了
	rear = temp;
	printf("入队元素：%d\n", data);
	return rear;
}

void LinkQueueOut(linkQueue *top, linkQueue *rear) {
	if (top->next == NULL) {
		printf("队列为空\n");
		return;
	}
	linkQueue *p = top->next;
	printf("出队元素：%d\n", p->data);
	top->next = p->next;		//让头指针指向下一个节点
	if (rear == p) {
		//当尾指针等于头指针的下一个指针时，出队后头指针和尾指针相等，将头指针赋值给尾指针，更新尾指针的值。
		rear = top;
	}
	free(p);
}