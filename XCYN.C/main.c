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
#define MAX(a,b) (a>b) ? a : b

int main()
{
	link * k = initLink();
	displayLink(k);
	return 0;
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