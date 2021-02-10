#include <stdio.h>
#include <stdlib.h>
#include <string.h>

//串的存储结构

// 使用定长顺序存储字符串
void CharStringStorageFun01() {
	//字符串的长度为11，再加上最后一个字符\n，所以需要提前申请长度12的字符数组的内存空间
	char str[12] = "hello world";	
	printf("%s\n", str);
}

// 使用非定长的可变长度字符串
void CharStringStroageFun02() {
	// 动态数组需要使用relloc函数申请更多的空间
	// Allocate(分配) memory(内存) block(块) 
	// Reallocate(重分配) memory block
	// Deallocate(释放) memory block
	char *a = (char*)malloc(sizeof(char) * 6);	//创建一个动态数组a，申请了5个字符类型大小的空间
	strcpy(a, "hello");
	printf("a：%s\n", a);
	int length_a = strlen(a);	//a串的长度

	char *b = (char*)malloc(sizeof(char) * 7);	//创建一个动态数组a，申请了5个字符类型大小的空间
	strcpy(b, "world2");
	printf("b：%s\n", b);
	int length_b = strlen(b);	//b串的长度

	if (length_a < length_a + length_b) {
		//判断两个字符串相加后的长度超过一个字符串的的长度
		a = (char*)realloc(a, length_a + length_b + 1);
	}

	//合并两个字符串到a1
	for (int i = length_a; i < length_a + length_b; i++)
	{
		a[i] = b[i - length_a];
	}
	a[length_a + length_b] = '\0';
	printf("拼接后的a：%s\n", a);

	//将申请过的内存空间释放掉
	free(a);
	free(b);
}

// 字符串的普通模式匹配算法(BF算法)
int CharStringStroageFun03(char *b, char *a) {
   	int i = 0;
	int j = 0;
	int length_a = strlen(a);
	int length_b = strlen(b);
	while (i < length_b && j < length_a) {
		if (b[i] == a[j]) {
			//字符串匹配，则继续比对
			i++;
			j++;
		}
		else {
			//主串与子串不匹配，将子串匹配的索引归零，主串位置向已经匹配的过的子串偏移j个位置
			i = i - j + 1;
			j = 0;
		}
	}

	if (j == length_a) {
		// j的值增加到与子串的长度相同，表示子串的所有字符串有一段与主串相同
		return 1;
	}
	return 0;
}