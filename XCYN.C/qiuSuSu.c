#include <stdio.h>

int prime(int num) {
	if (num < 0) {
		//����ֻ���Ǵ���0����
		return -1;
	}
	for (int i = 2; i < num; i++)
	{
		if (num % i == 0) {
			//�������������Լ���������
			return 0;
		}
	}
	return 1;
}