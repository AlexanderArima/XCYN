#include <stdio.h>
#include <stdlib.h>
#include "LinkQueue.h"

// ��ʼ��ͷ���
linkQueue * LinkQueueInit() {
	//����һ��ͷ���
	linkQueue *q = (linkQueue *)malloc(sizeof(linkQueue));

	//ͷ����ʼ��
	q->next = NULL;
	printf("���г�ʼ�����\n");
	return q;
}

// ��Ӳ���
// ����q��ԭ����
// ����data������
linkQueue * LinkQueueEntry(linkQueue * rear, int data) {
	// ����һ���½ڵ㣬���Ὣ���ŵ�β��
	linkQueue *temp = (linkQueue *)malloc(sizeof(linkQueue));
	temp->next = NULL;
	temp->data = data;

	//�½ڵ���ԭ���н����߼���ϵ��ԭ���е�nextָ��NULL�����ڸĳ�ָ���½ڵ�temp��
	rear->next = temp;

	//��ԭ�ڵ�ָ���½ڵ㣬��������nextָ�뻹��ָ��NULL����Ȼ��β�ڵ���
	rear = temp;
	printf("���Ԫ�أ�%d\n", data);
	return rear;
}

void LinkQueueOut(linkQueue *top, linkQueue *rear) {
	if (top->next == NULL) {
		printf("����Ϊ��\n");
		return;
	}
	linkQueue *p = top->next;
	printf("����Ԫ�أ�%d\n", p->data);
	top->next = p->next;		//��ͷָ��ָ����һ���ڵ�
	if (rear == p) {
		//��βָ�����ͷָ�����һ��ָ��ʱ�����Ӻ�ͷָ���βָ����ȣ���ͷָ�븳ֵ��βָ�룬����βָ���ֵ��
		rear = top;
	}
	free(p);
}