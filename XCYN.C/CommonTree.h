
typedef char ElemType;	//������������

typedef struct PTNode {
	char data;
	int parent;	//�洢���ڵ���±�λ��
} PTNode;

typedef struct {
	PTNode tNode[10];	//������е����н��ļ���
	int n;	//�����±�λ��
} PTree;

PTree InitPNode(PTree tree);