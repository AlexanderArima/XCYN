
typedef struct Link {
	char elem;	//代表数据域
	struct Link * next;	//代表指针域，指向直接的后缀
} link;

link * initLink();

void displayLink(link *p);