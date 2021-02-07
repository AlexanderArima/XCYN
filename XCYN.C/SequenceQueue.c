#include <stdio.h>
#include <stdlib.h>
#include "SequenceQueue.h"

// 入队操作
int SequenceQueue_Entry(int *queue, int rear, int data) {
	queue[rear] = data;
	rear++;	//入队操作需要将尾指针后移一位
	printf("入队元素为：%d\n", data);
	return rear;
}

// 出队操作
int SequenceQueue_Out(int *queue, int front, int rear) {
	if (front == rear) {
		// 当头指针和尾指针位置相同时队列为空
		printf("队列为空\n");
		return front;
	}
	printf("出队元素为：%d\n", queue[front]);
	front++;	//出队操作需要将头指针后移一位
	return front;
}