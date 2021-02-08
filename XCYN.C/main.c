#include <stdio.h>
#include <stdlib.h>
#include "input.h"
#include "ShuZu.h"
#include "qiuSuSu.h"
#include "TypedefDemo.h"
#include "zhiZhen01.h"
#include "zhiZhen02.h"
#include "zhiZhen03.h"
#include "SequenceTable.h"
#include "LinkTable.h"
#include "SequenceStack.h"
#include "LinkStack.h"
#include "SequenceQueue.h"
#include "LinkQueue.h"
#define MAX(a,b) (a>b) ? a : b

int main()
{
	linkQueue * queue, *top, *rear;
	queue = top = rear = LinkQueueInit();	//����ͷ���
	rear = LinkQueueEntry(rear, 0);	//��ʼ���
	rear = LinkQueueEntry(rear, 1);
	rear = LinkQueueEntry(rear, 2);
	rear = LinkQueueEntry(rear, 3);
	rear = LinkQueueEntry(rear, 4);

	LinkQueueOut(top, rear);	//��ʼ����
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);	//����Ϊ��
	return 0;
}

// ˳����в��Է���
void MainFunc05() {
	// ��ʼ��
	int a[100];
	int rear = 0;
	int front = 0;
	rear = SequenceQueue_Entry(a, rear, 0);
	rear = SequenceQueue_Entry(a, rear, 1);
	rear = SequenceQueue_Entry(a, rear, 2);

	front = SequenceQueue_Out(a, front, rear);
	front = SequenceQueue_Out(a, front, rear);
	front = SequenceQueue_Out(a, front, rear);
	front = SequenceQueue_Out(a, front, rear);

	rear = SequenceQueue_Entry(a, rear, 3);
	rear = SequenceQueue_Entry(a, rear, 4);

	front = SequenceQueue_Out(a, front, rear);
	front = SequenceQueue_Out(a, front, rear);
}

// ��ջ���Է���
void MainFunc04() {
	// ��ʼ��
	linkStack *link = NULL;

	// ��ջ
	link = LinkPush(link, 0);
	link = LinkPush(link, 1);
	link = LinkPush(link, 2);
	link = LinkPush(link, 3);
	link = LinkPush(link, 4);

	// ��ջ
	link = LinkPop(link);	//��ظ��¾ֲ�����link��ֵΪÿһ�������ķ���ֵ.
	link = LinkPop(link);
	link = LinkPop(link);
	link = LinkPop(link);
	link = LinkPop(link);
	link = LinkPop(link);
}

// ˳��ջ���Է���
void  MainFunc03() {
	int a[100];
	int top = -1;
	top = push(a, top, 1);
	top = push(a, top, 2);
	top = push(a, top, 3);
	top = push(a, top, 4);
	top = push(a, top, 5);
	top = pop(a, top);
	top = pop(a, top);
	top = pop(a, top);
	top = pop(a, top);
	top = pop(a, top);
	top = pop(a, top);
}

//������Է���
void MainFunc02() {
	link * k = initLink();
	insertLink(k, 99, 2);
	displayLink(k);
	int find_elem = 19;
	int index = selectElem(k, find_elem);
	if (index != -1) {
		printf("%d������������\n", find_elem);
	}
	else {
		printf("%d��������������\n", find_elem);
	}

	//�޸�ָ��λ�õ�Ԫ�ص�ֵ
	k = updateLink(k, 9, 100);
	displayLink(k);
}

//˳�����Է���
void MainFunc01() {
	table t = initTable();
	for (int i = 0; i < Size; i++)
	{
		t.head[i] = i + 1;
		t.length++;
	}
	//t = addTable(t, 99, 3);		//��ָ��λ�ò���Ԫ��
	//t = delTable(t, 4);				//��ָ��λ��ɾ��Ԫ��
	int index = selectTable(t, 1);
	if (index != -1) {
		printf("���ҵ��洢��Ԫ�� \n");
		updateTable(t, 2, 99);
	}
	else {
		printf("δ���ҵ��洢��Ԫ�� \n");
	}
	printf("˳����д洢��Ԫ�طֱ��ǣ�\n");
	displayTable(t);
}