
typedef struct BiTNode {
	int data;	//������
	struct BiTNode *lchild, *rchild;	//���Һ��ӵ�ָ��
	struct BiTNode *parent;		// ������ָ��
} BiTNode, *BiTree;		//BiTNode��ʾ�ṹ��ı�����*BiTree��ʾ�ṹ��ָ��ı���

//  ����һ��������
void CreateBiTree(BiTree *t);

//  ����һ��������
BiTNode * OtherCreateBiTree(BiTNode *t);

void displayElem(BiTNode* elem);

void displayElem(BiTree elem);

void proOrderLists(BiTree tree);

void middleOrderList(BiTree tree);

void postOrderList(BiTree tree);