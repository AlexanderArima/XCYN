
typedef struct Link {
	char elem;	//����������
	struct Link * next;	//����ָ����ָ��ֱ�ӵĺ�׺
} link;

link * initLink();

void displayLink(link *p);