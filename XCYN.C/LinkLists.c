#include <stdio.h>
#include <stdlib.h>
#include "LinkLists.h"

// ����һ�������
Glist CreateLists(Glist c) {
	c = (Glist)malloc(sizeof(Glist));
	c->tag = 1;
	c->hp = (Glist)malloc(sizeof(Glist));
	c->tp = NULL;

	// ��ͷԭ��
	c->hp->tag = 0;
	c->hp->value = 'a';
	c->hp->tp = (Glist)malloc(sizeof(Glist));
	c->hp->tp->tag = 1;
	c->hp->tp->hp = (Glist)malloc(sizeof(Glist));
	c->hp->tp->tp = NULL;

	//ԭ��b
	c->hp->tp->hp->tag = 0;
	c->hp->tp->hp->value = 'b';
	c->hp->tp->hp->tp = (Glist)malloc(sizeof(Glist));

	//ԭ��c
	c->hp->tp->hp->tp->tag = 0;
	c->hp->tp->hp->tp->value = 'c';
	c->hp->tp->hp->tp->tp = (Glist)malloc(sizeof(Glist));

	//ԭ��d
	c->hp->tp->hp->tp->tp->tag = 0;
	c->hp->tp->hp->tp->tp->value = 'd';
	c->hp->tp->hp->tp->tp->tp = NULL;

	return c;
}