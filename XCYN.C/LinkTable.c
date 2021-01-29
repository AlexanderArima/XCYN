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

//��ʾ�����еĸ���Ԫ�ص�ֵ
void displayLink(link *p) {
	link * temp = p;
	while (temp) {
		printf("%d ", temp->elem);
		temp=temp->next;	//ָ���ƶ�����һ��Ԫ��
	}
	printf("\n");
}
