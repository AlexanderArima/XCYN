
// ��ͷ�ļ��ж���ṹ�壬Ԥ����ָ��
typedef struct Table {
	int *head;		//��̬����
	int length;	//˳���ĳ���
	int size;	//����Ĵ洢����
} table;	//table�ǽṹ��ı���

#define Size 5		//�������󳤶�

table initTable();

void displayTable(table t);
