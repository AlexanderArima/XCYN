
// ��ͷ�ļ��ж���ṹ�壬Ԥ����ָ��
typedef struct Table {
	int *head;		//��̬����
	int length;	//˳���ĳ���
	int size;	//����Ĵ洢����
} table;	//table�ǽṹ��ı���

#define Size 5		//�������󳤶�

table initTable();

void displayTable(table t);

table addTable(table t, int elem, int add);

table delTable(table t, int add);

int selectTable(table t, int elem);

table updateTable(table t, int elem, int newElem);