#include <stdio.h>
#include <stdlib.h>
#include "BiTree.h"

// ����һ��������
void CreateBiTree(BiTree *T) {
	*T = (BiTNode*)malloc(sizeof(BiTNode));
	(*T)->data = 1;
	(*T)->lchild = (BiTNode*)malloc(sizeof(BiTNode));
	(*T)->rchild = (BiTNode*)malloc(sizeof(BiTNode));

	(*T)->lchild->data = 2;
	(*T)->lchild->lchild = (BiTNode*)malloc(sizeof(BiTNode));
	(*T)->lchild->rchild = (BiTNode*)malloc(sizeof(BiTNode));
	(*T)->lchild->rchild->data = 5;
	(*T)->lchild->rchild->lchild = NULL;
	(*T)->lchild->rchild->rchild = NULL;
	(*T)->rchild->data = 3;
	(*T)->rchild->lchild = (BiTNode*)malloc(sizeof(BiTNode));
	(*T)->rchild->lchild->data = 6;
	(*T)->rchild->lchild->lchild = NULL;
	(*T)->rchild->lchild->rchild = NULL;
	(*T)->rchild->rchild = (BiTNode*)malloc(sizeof(BiTNode));
	(*T)->rchild->rchild->data = 7;
	(*T)->rchild->rchild->lchild = NULL;
	(*T)->rchild->rchild->rchild = NULL;
	(*T)->lchild->lchild->data = 4;
	(*T)->lchild->lchild->lchild = NULL;
	(*T)->lchild->lchild->rchild = NULL;
}

// ����һ��������
BiTNode * OtherCreateBiTree(BiTNode *t) {
	t = (BiTNode *)malloc(sizeof(BiTNode));
	t->data = 1;
	t->lchild = (BiTNode *)malloc(sizeof(BiTNode));
	t->lchild->data = 2;
	t->lchild->rchild = NULL;
	t->lchild->lchild = (BiTNode *)malloc(sizeof(BiTNode));
	t->lchild->lchild->data = 4;
	t->lchild->lchild->lchild = NULL;
	t->lchild->lchild->rchild = NULL;

	t->rchild = (BiTNode *)malloc(sizeof(BiTNode));
	t->rchild->data = 3;
	t->rchild->lchild = NULL;
	t->rchild->rchild = NULL;
	return t;
}

// ��ӡ����ֵ
void displayElem(BiTree elem) {
	printf("%d ", elem->data);
}

// ǰ�����
void proOrderLists(BiTree tree) {
	if (tree) {
		displayElem(tree);
		proOrderLists(tree->lchild);
		proOrderLists(tree->rchild);
	}
	// ���Ϊ�շ�����һ��
	return;
}

// �������
void middleOrderList(BiTree tree) {
	if (tree) {
		middleOrderList(tree->lchild);
		displayElem(tree);
		middleOrderList(tree->rchild);
	}
	return;
}

// �������
void postOrderList(BiTree tree) {
	if (tree) {
		postOrderList(tree->lchild);
		postOrderList(tree->rchild);
		displayElem(tree);
	}
	return;
}