#include <stdio.h>

//ð������
void BubbleSort() {
	int arr[10] = {0, 2, 4, 6, 8, 1, 3, 5, 7, 9};
	int temp = 0;
	int compareCount = 0;
	int isSort = 1;	//��������Ƿ���������˵�
	for (int i = 0; i < 10 - 1; i++)	
	{
		isSort = 1;
		//���鹲��10��Ԫ�أ��õ�һ�����Ƚϵڶ��������ڶ������Ƚϵ�������....��n - 1�����Ƚϵ�n�������ȽϵĴ���Ϊn - 1
		for (int j = 1; j < 10 - 1 - i; j++)
		{
			//�ڶ���ѭ���ǿ��Ʊ��Ƚϵ���������Զ�ȱȱȽϵ����±��1
			int before = arr[j - 1];
			int after = arr[j];	//after���±��before��1
			if (before > after) {
				//����λ��
				temp = before;
				arr[j - 1] = arr[j];
				arr[j] = temp;
				isSort = 0;
			}
			compareCount++;
		}
		if (isSort == 1) {
			//���������õģ���ô�Ͳ�����ȥ�����ˣ�ֱ����������
			break;
		}
	}
	for (int i = 0; i < 10; i++)
	{
		printf("%d", arr[i]);
	}
	printf("\n");
	printf("�Ƚϴ�����%d", compareCount);
}