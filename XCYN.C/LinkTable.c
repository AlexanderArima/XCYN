#include <stdio.h>
#include <stdlib.h>
#include "LinkTable.h"

//��ʼ������
link * initLink() {
	//��Ԫ�ڵ��ʼ��
	link * temp = (link *)malloc(sizeof(link));	//������Ԫ�ڵ�
	temp->elem = 1;	//���׽ڵ㸳ֵ
	temp->next = NULL;	//�׽ڵ�ָ�����һ���ڵ��ָ���ַ��ʼ��

	//�׽ڵ��ʼ����Ϻ󣬽�ͷָ��ָ����Ԫ�ڵ�
	link * p = NULL;	//����ͷָ��
	p = temp;	//��ͷָ��ָ����Ԫ�ڵ�

	//�ӵڶ����ڵ㿪ʼ���������ڵ�
	for (int i = 2; i < 10; i++)
	{
		link *a = (link *)malloc(sizeof(link));	//���������ڵ㣬�����ڴ�ռ�
		a->elem = i;	//�����ڵ㸳ֵ
		a->next = NULL;		//�����ڵ�ָ��Ľӿ�Ϊ�գ�����ʼ��

		//���������ڵ�֮�����ϵ
		temp->next = a;		//ǰ���ڵ�ָ����һ���ڵ㣬������ôд��Ϊ�˸�link�����е�next���Ա�Ǻ����ڵ�
		temp = a;					//�����ٴθ�temp��ֵa����Ϊ��ʵ������tempָ��ָ��a
	}
	//��ͷָ�뷵�ؼ��ɣ���Ϊ�õ���ͷָ��Ҳ���ܹ�������������
	return p;		
}

// pΪ����elemΪ�����ֵ��addΪ���������
link * insertLink(link * p, int elem, int add) {
	link * temp = p;
	for (int i = 0; i < add; i++)
	{
		// �ӵ�һ���ڵ������add��λ��
		temp = temp->next;
		if (temp == NULL) {
			// �����������±�����.
			printf("�����λ����Ч");
			return p;
		}
	}

	// ��������Ľڵ�
	link *new_node = (link *)malloc(sizeof(link));
	new_node->elem = elem;

	//�����´����Ľڵ�ָ�����һ���ڵ��ָ���ַ
	new_node->next = temp->next;

	//�����´����Ľڵ����һ���ڵ�ָ���ָ��Ϊ�½ڵ�
	temp->next = new_node;
	return p;
}

//����Ԫ�ص��±�λ��
int selectElem(link *p, int elem) {
	//��ʼ������
	link *temp = p;	//�õ�����ͷָ��
	int i = 0;
	while (temp != NULL) {
		if (temp->elem == elem) {
			return i;
		}

		//��ָ����һ���ڵ��ָ�븳ֵ����ǰ������Ԫ�أ������´α����ľ�����һ���ڵ���
		temp = temp->next;
		i++;
	}

	//����-1��ʾδ�ҵ���Ԫ��
	return -1;
}

// �޸�������Ԫ��
link * updateLink(link *p, int index, int new_elem) {
	link *temp = p;
	for (int i = 0; i < index; i++) {
		temp = temp->next;
		if (temp == NULL) {
			// �������ս�������˳���������������쳣.
			return p;
		}
	}
	temp->elem = new_elem;
	return p;
}

//��ʾ�����еĸ���Ԫ�ص�ֵ
void displayLink(link *p) {
	link * temp = p;
	while (temp) {
		printf("%d ", temp->elem);
		temp=temp->next;	//ָ���ƶ�����һ��Ԫ��
	}
	printf("\n");
}
