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

//���˳����е�Ԫ��
void displayTable(table t) {
	for (int i = 0; i < t.length; i++) {
		printf("%d ", t.head[i]);
	}
	printf("\n");
}

