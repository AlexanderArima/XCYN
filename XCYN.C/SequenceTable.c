#include <stdio.h>
#include <stdlib.h>
#include "SequenceTable.h"

//初始化顺序表
table initTable() {
	table t;
	t.head = (int *)malloc(Size * sizeof(int));	//构造一个新的空顺序表，动态申请存储空间.
	if (!t.head) {
		printf("初始化失败");
		exit(0);
	}
	t.length = 0;
	t.size = Size;
	return t;
}

//输出顺序表中的元素
void displayTable(table t) {
	for (int i = 0; i < t.length; i++) {
		printf("%d ", t.head[i]);
	}
	printf("\n");
}

