#include <stdio.h>
#include <stdlib.h>
#include <windows.h>

void FileDemo_Open() {
	// printf("FileDemo_Open");

	//char str[3];
	//FILE *fp;
	//// fp = fopen("F:\\�ҵ�Git��Ŀ\\XCYN\\XCYN.C\\OutPut\\1.txt", "r");
	//fp = fopen("E:\\Program Files (x86)\\0830����\\�ù���Ϣ����ϵͳ(ʡ����)\\HotelManage.UI.exe", "r");
	//if (fp == NULL) {
	//	printf("���ļ�ʧ��");
	//	exit(0);
	//}

	//if (fgets(str, 3, fp) != NULL) {
	//	printf("%s", str);
	//}

	//fclose(fp);
	//// fopen("1.txt", "r");

	WinExec("E:\\Program Files (x86)\\0830����\\�ù���Ϣ����ϵͳ(ʡ����)\\HotelManage.UI.exe", SW_SHOW);
}