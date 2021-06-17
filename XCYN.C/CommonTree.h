
typedef char ElemType;	//定义数据类型

typedef struct PTNode {
	char data;
	int parent;	//存储父节点的下标位置
} PTNode;

typedef struct {
	PTNode tNode[10];	//存放树中的所有结点的集合
	int n;	//根的下标位置
} PTree;

PTree InitPNode(PTree tree);