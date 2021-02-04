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
	insertLink(k, 99, 2);
	displayLink(k);
	int find_elem = 19;
	int index = selectElem(k, find_elem);
	if (index != -1) {
		printf("%d存在于链表中\n", find_elem);
	}
	else {
		printf("%d不存在于链表中\n", find_elem);
	}

	//修改指定位置的元素的值
	k = updateLink(k, 9, 100);
	displayLink(k);
	return 0;
}

//调用顺序表
void MainFunc01() {
	table t = initTable();
	for (int i = 0; i < Size; i++)
	{
		t.head[i] = i + 1;
		t.length++;
	}
	//t = addTable(t, 99, 3);		//在指定位置插入元素
	//t = delTable(t, 4);				//在指定位置删除元素
	int index = selectTable(t, 1);
	if (index != -1) {
		printf("查找到存储的元素 \n");
		updateTable(t, 2, 99);
	}
	else {
		printf("未查找到存储的元素 \n");
	}
	printf("顺序表中存储的元素分别是：\n");
	displayTable(t);
}