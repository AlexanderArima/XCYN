#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>

//������һ�����壬�����ڴ��������ģ�Ҳ����˵������Ԫ��֮�����໥���ŵģ��˴�֮��û��һ����϶��

int Fun8() {
	int i = 0;
	int list[10];
	printf("������10������\n");
	for (; i < 10; i++)
	{
		scanf("%d", &list[i]);	//scanf��ȡ��������ʱ��Ҫ����һ����ַ��������list[i]ǰ�����&���ţ�&�Ὣһ�������ֵת�ɵ�ַ 
	}
	i = 0;
	printf("������ϣ���ʼ���...\n");
	for (;	 i < 10; i++)
	{
		printf("%d \n", list[i]);
	}
	printf("������\n");
	system("pause");
	return 0;
}
