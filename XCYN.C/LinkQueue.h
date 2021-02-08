
// 创建链式队列
typedef struct linkQueue {
	int data;
	struct linkQueue * next;
} linkQueue;

linkQueue * LinkQueueInit();

linkQueue * LinkQueueEntry(linkQueue * rear, int data);

void LinkQueueOut(linkQueue *top, linkQueue *rear);