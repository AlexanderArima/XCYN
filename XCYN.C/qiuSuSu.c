#include <stdio.h>

int prime(int num) {
	if (num < 0) {
		//素数只能是大于0的数
		return -1;
	}
	for (int i = 2; i < num; i++)
	{
		if (num % i == 0) {
			//素数不被除了自己以外整除
			return 0;
		}
	}
	return 1;
}