#include <stdio.h>
#include <stdlib.h>
#include <string.h>

//���Ĵ洢�ṹ

// ʹ�ö���˳��洢�ַ���
void CharStringStorageFun01() {
	//�ַ����ĳ���Ϊ11���ټ������һ���ַ�\n��������Ҫ��ǰ���볤��12���ַ�������ڴ�ռ�
	char str[12] = "hello world";	
	printf("%s\n", str);
}

// ʹ�÷Ƕ����Ŀɱ䳤���ַ���
void CharStringStroageFun02() {
	// ��̬������Ҫʹ��relloc�����������Ŀռ�
	// Allocate(����) memory(�ڴ�) block(��) 
	// Reallocate(�ط���) memory block
	// Deallocate(�ͷ�) memory block
	char *a = (char*)malloc(sizeof(char) * 6);	//����һ����̬����a��������5���ַ����ʹ�С�Ŀռ�
	strcpy(a, "hello");
	printf("a��%s\n", a);
	int length_a = strlen(a);	//a���ĳ���

	char *b = (char*)malloc(sizeof(char) * 7);	//����һ����̬����a��������5���ַ����ʹ�С�Ŀռ�
	strcpy(b, "world2");
	printf("b��%s\n", b);
	int length_b = strlen(b);	//b���ĳ���

	if (length_a < length_a + length_b) {
		//�ж������ַ�����Ӻ�ĳ��ȳ���һ���ַ����ĵĳ���
		a = (char*)realloc(a, length_a + length_b + 1);
	}

	//�ϲ������ַ�����a1
	for (int i = length_a; i < length_a + length_b; i++)
	{
		a[i] = b[i - length_a];
	}
	a[length_a + length_b] = '\0';
	printf("ƴ�Ӻ��a��%s\n", a);

	//����������ڴ�ռ��ͷŵ�
	free(a);
	free(b);
}

// �ַ�������ͨģʽƥ���㷨(BF�㷨)
int CharStringStroageFun03(char *b, char *a) {
   	int i = 0;
	int j = 0;
	int length_a = strlen(a);
	int length_b = strlen(b);
	while (i < length_b && j < length_a) {
		if (b[i] == a[j]) {
			//�ַ���ƥ�䣬������ȶ�
			i++;
			j++;
		}
		else {
			//�������Ӵ���ƥ�䣬���Ӵ�ƥ����������㣬����λ�����Ѿ�ƥ��Ĺ����Ӵ�ƫ��j��λ��
			i = i - j + 1;
			j = 0;
		}
	}

	if (j == length_a) {
		// j��ֵ���ӵ����Ӵ��ĳ�����ͬ����ʾ�Ӵ��������ַ�����һ����������ͬ
		return 1;
	}
	return 0;
}