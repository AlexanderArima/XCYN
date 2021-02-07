
typedef struct linkStack {
	int data;
	struct linkStack * next;
} linkStack;

linkStack * LinkPush(linkStack * stack, int data);

linkStack * LinkPop(linkStack * stack);