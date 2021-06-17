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
#include "CharStringStorage.h"
#include "LinkLists.h"
#include "BiTree.h"
#include "CommonTree.h"
#define MAX(a,b) (a>b) ? a : b

int main()
{
	PTree tree;
	//初始化树
	for (int i = 0; i < 10; i++) {
		tree.tNode[i].data = ' ';
		tree.tNode[i].parent = 0;	
	}
	tree = InitPNode(tree);
	printf("根结点的值为：%c", tree.tNode[0].data);
	return 0;
}

// 二叉树的前序，中序，后序遍历
void MainFunc09() {
	BiTree tree = NULL;	//定义了一个非指针的变量
	CreateBiTree(&tree);	//将该变量的地址传入函数
	//proOrderLists(tree);		//前序遍历
	//middleOrderList(tree);	//中序遍历
	postOrderList(tree);		//后序遍历
}

// 初始化二叉树
void MainFunc08() {
	// 官方例子
	BiTree tree = NULL;	//定义了一个非指针的变量
	CreateBiTree(&tree);	//将该变量的地址传入函数
	printf("%d\n", tree->lchild->data);

	// 我的例子
	BiTNode *tree2 = NULL;
	tree2 = OtherCreateBiTree(tree2);
	printf("%d", tree2->data);
}

//串的BF算法
void MainFunc07() {
	char *a = "wataxiwadannsenndesi";
	char *b = "desi";
	int result = CharStringStroageFun03(a, b);
	if (result == 0) {
		printf("字符串%s与字符串%s不匹配", a, b);
	}
	else {
		printf("字符串%s与字符串%s匹配", a, b);
	}
}

//链表队列测试方法
void MainFunc06() {
	linkQueue * queue, *top, *rear;
	queue = top = rear = LinkQueueInit();	//创建头结点
	rear = LinkQueueEntry(rear, 0);	//开始入队
	rear = LinkQueueEntry(rear, 1);
	rear = LinkQueueEntry(rear, 2);
	rear = LinkQueueEntry(rear, 3);
	rear = LinkQueueEntry(rear, 4);

	LinkQueueOut(top, rear);	//开始出队
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);
	LinkQueueOut(top, rear);	//队列为空
}

// 顺序队列测试方法
void MainFunc05() {
	// 初始化
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

// 链栈测试方法
void MainFunc04() {
	// 初始化
	linkStack *link = NULL;

	// 入栈
	link = LinkPush(link, 0);
	link = LinkPush(link, 1);
	link = LinkPush(link, 2);
	link = LinkPush(link, 3);
	link = LinkPush(link, 4);

	// 出栈
	link = LinkPop(link);	//务必更新局部变量link的值为每一个方法的返回值.
	link = LinkPop(link);
	link = LinkPop(link);
	link = LinkPop(link);
	link = LinkPop(link);
	link = LinkPop(link);
}

// 顺序栈测试方法
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

//链表测试方法
void MainFunc02() {
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
}

//顺序表测试方法
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