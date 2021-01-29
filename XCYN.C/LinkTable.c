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

//显示链表中的各个元素的值
void displayLink(link *p) {
	link * temp = p;
	while (temp) {
		printf("%d ", temp->elem);
		temp=temp->next;	//指针移动到下一个元素
	}
	printf("\n");
}
