#include <stdio.h>
#include <stdlib.h>
#include "SequenceTable.h"

//��ʼ��˳���
table initTable() {
	table t;
	t.head = (int *)malloc(Size * sizeof(int));	//����һ���µĿ�˳�����̬����洢�ռ�.
	if (!t.head) {
		printf("��ʼ��ʧ��");
		exit(0);
	}
	t.length = 0;
	t.size = Size;
	return t;
}

//���һ��Ԫ�أ�elem�ǲ����Ԫ�أ�add�ǲ��������λ��
table addTable(table t,int elem,int add) {
	//��֤����Ĳ���������λ���Ƿ񳬹�����ķ�Χ���Ƿ�С��0
	if (add < 0 || add >= t.length)
	{
		printf("����Ĳ�������ȷ");
		return t;
	}
	
	//�鿴����ĳ����Ƿ��Ѿ��ﵽ���ֵ������Ѿ��ﵽ���������ݣ�������ĳ���+1
	if (t.length == t.size) {
		t.head = (int *)realloc(t.head, (t.size + 1) * sizeof(int));
		if (!t.head) {
			printf("�洢����ʧ��");
			return t;
		}
		t.size = t.size + 1;
	}

	//����������������еĺ���Ԫ�����������ƶ�һ��Ȼ��������λ�ø������ֵ
	for (int i = t.length; i > add; i--)
	{
		t.head[i] = t.head[i - 1];
	}
	t.head[add] = elem;
	t.length = t.length + 1;
	return t;
}

//˳���ɾ��Ԫ��
table delTable(table t, int add) {
	if (add > t.length || add < 0) {
		printf("��ɾ����Ԫ��λ������");
		return t;
	}
	// ɾ������
	for (int i = add; i < t.length; i++)
	{
		t.head[i] = t.head[i + 1];
	}
	t.length--;
	return t;
}

//����Ԫ��
int selectTable(table t, int elem) {
	for (int i = 0; i < t.length; i++)
	{
		if (elem == t.head[i]) {
			return i;
		}
	}
	return -1;
}

//�޸�Ԫ��
table updateTable(table t, int elem, int newElem) {
	int index = selectTable(t, elem);
	if (index == -1) {
		return t;
	}
	t.head[index] = newElem;
	return t;
}

//���˳����е�Ԫ��
void displayTable(table t) {
	for (int i = 0; i < t.length; i++) {
		printf("%d ", t.head[i]);
	}
	printf("\n");
}

