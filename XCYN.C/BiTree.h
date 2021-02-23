
typedef struct BiTNode {
	int data;	//数据域
	struct BiTNode *lchild, *rchild;	//左右孩子的指针
	struct BiTNode *parent;		// 父结点的指针
} BiTNode, *BiTree;		//BiTNode表示结构体的别名，*BiTree表示结构体指针的别名

//  创建一个二叉树
void CreateBiTree(BiTree *t);

//  创建一个二叉树
BiTNode * OtherCreateBiTree(BiTNode *t);

void displayElem(BiTNode* elem);

void displayElem(BiTree elem);

void proOrderLists(BiTree tree);

void middleOrderList(BiTree tree);

void postOrderList(BiTree tree);