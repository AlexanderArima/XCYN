#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int Fun39() {
	int t = time(NULL);
	printf("t£º%d\n", t);
	srand((unsigned)time(NULL));
	int num = rand();
	printf("num£º%d\n", num);
	return 0;
}
