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
#define MAX(a,b) (a>b) ? a : b

int main()
{
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
	return 0;
}

// ˳��ջ
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

//��������
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

//����˳���
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