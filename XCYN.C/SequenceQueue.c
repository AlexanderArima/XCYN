#include <stdio.h>
#include <stdlib.h>
#include "SequenceQueue.h"

// ��Ӳ���
int SequenceQueue_Entry(int *queue, int rear, int data) {
	queue[rear] = data;
	rear++;	//��Ӳ�����Ҫ��βָ�����һλ
	printf("���Ԫ��Ϊ��%d\n", data);
	return rear;
}

// ���Ӳ���
int SequenceQueue_Out(int *queue, int front, int rear) {
	if (front == rear) {
		// ��ͷָ���βָ��λ����ͬʱ����Ϊ��
		printf("����Ϊ��\n");
		return front;
	}
	printf("����Ԫ��Ϊ��%d\n", queue[front]);
	front++;	//���Ӳ�����Ҫ��ͷָ�����һλ
	return front;
}