typedef struct GLNode {
	int tag;		//通常原子的 tag 值为 0，子表的 tag 值为 1
	union {
		char value;		//	存储的值
		struct GLNode *hp;		//hp 指针用于连接本子表中存储的原子或子表
	};
	struct GLNode *tp;		//tp 指针用于连接广义表中下一个原子或子表。
} *Glist;

Glist CreateLists(Glist c);