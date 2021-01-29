
// 在头文件中定义结构体，预处理指令
typedef struct Table {
	int *head;		//动态数组
	int length;	//顺序表的长度
	int size;	//数组的存储容量
} table;	//table是结构体的变量

#define Size 5		//数组的最大长度

table initTable();

void displayTable(table t);

table addTable(table t, int elem, int add);

table delTable(table t, int add);

int selectTable(table t, int elem);

table updateTable(table t, int elem, int newElem);