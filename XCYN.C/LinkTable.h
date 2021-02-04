
typedef struct Link {
	char elem;	//����������
	struct Link * next;	//����ָ����ָ��ֱ�ӵĺ�׺
} link;

link * initLink();

link * insertLink(link * p, int elem, int add);

int selectElem(link *p, int elem);

link * updateLink(link *p, int index, int new_elem);

void displayLink(link *p);