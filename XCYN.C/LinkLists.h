typedef struct GLNode {
	int tag;		//ͨ��ԭ�ӵ� tag ֵΪ 0���ӱ�� tag ֵΪ 1
	union {
		char value;		//	�洢��ֵ
		struct GLNode *hp;		//hp ָ���������ӱ��ӱ��д洢��ԭ�ӻ��ӱ�
	};
	struct GLNode *tp;		//tp ָ���������ӹ��������һ��ԭ�ӻ��ӱ�
} *Glist;

Glist CreateLists(Glist c);