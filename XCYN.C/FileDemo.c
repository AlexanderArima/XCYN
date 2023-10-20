#include <stdio.h>
#include <stdlib.h>
#include <windows.h>

void FileDemo_Open() {
	// printf("FileDemo_Open");

	//char str[3];
	//FILE *fp;
	//// fp = fopen("F:\\我的Git项目\\XCYN\\XCYN.C\\OutPut\\1.txt", "r");
	//fp = fopen("E:\\Program Files (x86)\\0830测试\\旅馆信息管理系统(省厅版)\\HotelManage.UI.exe", "r");
	//if (fp == NULL) {
	//	printf("打开文件失败");
	//	exit(0);
	//}

	//if (fgets(str, 3, fp) != NULL) {
	//	printf("%s", str);
	//}

	//fclose(fp);
	//// fopen("1.txt", "r");

	WinExec("E:\\Program Files (x86)\\0830测试\\旅馆信息管理系统(省厅版)\\HotelManage.UI.exe", SW_SHOW);
}