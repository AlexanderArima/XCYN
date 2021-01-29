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

//添加一个元素，elem是插入的元素，add是插入的索引位置
table addTable(table t,int elem,int add) {
	//验证输入的参数，索引位置是否超过数组的范围，是否小于0
	if (add < 0 || add >= t.length)
	{
		printf("输入的参数不正确");
		return t;
	}
	
	//查看数组的长度是否已经达到最大值，如果已经达到就申请扩容，让数组的长度+1
	if (t.length == t.size) {
		t.head = (int *)realloc(t.head, (t.size + 1) * sizeof(int));
		if (!t.head) {
			printf("存储分配失败");
			return t;
		}
		t.size = t.size + 1;
	}

	//插入操作，将数组中的后续元素依次往后移动一格，然后往插入位置赋插入的值
	for (int i = t.length; i > add; i--)
	{
		t.head[i] = t.head[i - 1];
	}
	t.head[add] = elem;
	t.length = t.length + 1;
	return t;
}

//顺序表删除元素
table delTable(table t, int add) {
	if (add > t.length || add < 0) {
		printf("被删除的元素位置有误");
		return t;
	}
	// 删除操作
	for (int i = add; i < t.length; i++)
	{
		t.head[i] = t.head[i + 1];
	}
	t.length--;
	return t;
}

//查找元素
int selectTable(table t, int elem) {
	for (int i = 0; i < t.length; i++)
	{
		if (elem == t.head[i]) {
			return i;
		}
	}
	return -1;
}

//修改元素
table updateTable(table t, int elem, int newElem) {
	int index = selectTable(t, elem);
	if (index == -1) {
		return t;
	}
	t.head[index] = newElem;
	return t;
}

//输出顺序表中的元素
void displayTable(table t) {
	for (int i = 0; i < t.length; i++) {
		printf("%d ", t.head[i]);
	}
	printf("\n");
}

