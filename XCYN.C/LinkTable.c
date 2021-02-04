#include <stdio.h>
#include <stdlib.h>
#include "LinkTable.h"

//初始化链表
link * initLink() {
	//首元节点初始化
	link * temp = (link *)malloc(sizeof(link));	//创建首元节点
	temp->elem = 1;	//给首节点赋值
	temp->next = NULL;	//首节点指向的下一个节点的指针地址初始化

	//首节点初始化完毕后，将头指针指向首元节点
	link * p = NULL;	//创建头指针
	p = temp;	//让头指针指向首元节点

	//从第二个节点开始创建其他节点
	for (int i = 2; i < 10; i++)
	{
		link *a = (link *)malloc(sizeof(link));	//创建其他节点，申请内存空间
		a->elem = i;	//其他节点赋值
		a->next = NULL;		//其他节点指向的接口为空，即初始化

		//建立各个节点之间的联系
		temp->next = a;		//前驱节点指向下一个节点，这里这么写是为了给link对象中的next属性标记后驱节点
		temp = a;					//这里再次给temp赋值a，是为了实际上让temp指针指向a
	}
	//将头指针返回即可，因为拿到了头指针也就能够遍历整个链表
	return p;		
}

// p为链表，elem为插入的值，add为插入的索引
link * insertLink(link * p, int elem, int add) {
	link * temp = p;
	for (int i = 0; i < add; i++)
	{
		// 从第一个节点便利到add的位置
		temp = temp->next;
		if (temp == NULL) {
			// 超过索引的下标限制.
			printf("插入的位置无效");
			return p;
		}
	}

	// 创建插入的节点
	link *new_node = (link *)malloc(sizeof(link));
	new_node->elem = elem;

	//设置新创建的节点指向的下一个节点的指针地址
	new_node->next = temp->next;

	//设置新创建的节点的上一个节点指向的指针为新节点
	temp->next = new_node;
	return p;
}

//查找元素的下标位置
int selectElem(link *p, int elem) {
	//初始化链表
	link *temp = p;	//拿到的是头指针
	int i = 0;
	while (temp != NULL) {
		if (temp->elem == elem) {
			return i;
		}

		//将指向下一个节点的指针赋值给当前的链表元素，这样下次遍历的就是下一个节点了
		temp = temp->next;
		i++;
	}

	//返回-1表示未找到该元素
	return -1;
}

// 修改链表中元素
link * updateLink(link *p, int index, int new_elem) {
	link *temp = p;
	for (int i = 0; i < index; i++) {
		temp = temp->next;
		if (temp == NULL) {
			// 遍历到终结点立即退出方法，避免产生异常.
			return p;
		}
	}
	temp->elem = new_elem;
	return p;
}

//显示链表中的各个元素的值
void displayLink(link *p) {
	link * temp = p;
	while (temp) {
		printf("%d ", temp->elem);
		temp=temp->next;	//指针移动到下一个元素
	}
	printf("\n");
}
