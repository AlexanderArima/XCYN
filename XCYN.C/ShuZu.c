#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>
#include <string.h>

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

//��ά����
int Fun9() {
	int list[2][3]  = { {60, 70, 80}, {70, 80, 90} };
	for (int i = 0; i < 2; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			printf("%d ", list[i][j]);
		}
		printf("\n");
	}
	return 0;
}

int Fun10() {

	//char str[] = "http://c.biancheng.net/c/";
	//long len = strlen(str);
	//printf("The lenth of the string is %ld.\n", len);

	char userName[] = "hello World";	//��ʼ���ַ���
	long len;
	len = strlen(userName);
	printf("%s" , userName);

	//char str[30] = { 0 };	//��\0��ʼ������
	//char c;
	//int i;
	//for (c = 65, i = 0; c <= 90; c++ , i++)
	//{
	//	//���A��Z
	//	str[i] = c;
	//}
	//printf("%s\n", str);
	return 0;
}

///�ַ���ƴ��
void Fun11() {
	char a[100] = "The URL is ";
	char b[50];
	printf("������URL\n");
	gets(b);	//��ȡ�ַ���������
	strcat(a, b);	//��bƴ�ӵ�a�ĺ���
	puts(a);	//��ӡ�ַ���
}

///�ַ�������
void Fun12() {
	char a[100] = "000000000";
	char b[100] = "�¿�˹";
	strcpy(a, b);	//���ַ���b����a
	puts(a);
	
}

///�ַ����Ƚ�
void Fun13() {
	char a[] = "0000";
	char b[] = "0";
	int result =strcmp(a, b);	//a����b������1��b����a����-1��a����b����0
	printf("The result is %d", result);
}

void display_array(int arr[] ,int len) {
	for (int i = 0; i < len; i++)
	{
		printf("%d ", arr[i]);
	}
	printf("\n");
}

//ɾ��ָ���±��Ԫ��
void Fun14() {
	int arr[10] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
	int arr_new1[9];
	//��������Ԫ��ɾ��
	for (int i = 0; i < 10; i++)
	{
		if (i < 5) {
			arr_new1[i] = arr[i];
		}
		else if (i > 5) {
			arr_new1[i - 1] = arr[i];	//������6��Ԫ��
		}
	}
	display_array(arr_new1, 9);
}
