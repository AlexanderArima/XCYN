
typedef struct Link {
	char elem;	//代表数据域
	struct Link * next;	//代表指针域，指向直接的后缀
} link;

link * initLink();

link * insertLink(link * p, int elem, int add);

int selectElem(link *p, int elem);

link * updateLink(link *p, int index, int new_elem);

void displayLink(link *p);