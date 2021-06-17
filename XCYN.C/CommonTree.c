#include <stdio.h>
#include <stdlib.h>
#include "CommonTree.h"

PTree InitPNode(PTree tree) {
	tree.tNode[0].data = 'A';
	tree.tNode[0].parent = -1;
	tree.tNode[1].data = 'B';
	tree.tNode[1].parent = 0;
	tree.tNode[2].data = 'C';
	tree.tNode[2].parent = 0;
	return tree;
}