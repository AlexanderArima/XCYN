#include <stdio.h>
#include <stdlib.h>

int Fun17() {
	//预定义宏
	printf("Date : %s\n", __DATE__);
	printf("Time : %s\n", __TIME__);
	printf("File : %s\n", __FILE__);
	printf("Line : %d\n", __LINE__);
	system("pause");
	return 0;
}

int Fun18() {
	//条件编译
#if _WIN32
	printf("This is Windows Platform");
#elif __linux__
	printf("This is Linux Platform");
#else
	printf("This is unknown Platform");
#endif
	//printf("WIN32：%d", _WIN32);	//print : WIN32：1
	//printf("__cplusplus：%d", __cplusplus);	//__cplusplus表示使用C++编译
	//#error 编译错误		//#error 指令用于在编译期间产生错误信息，并阻止程序的编译，
	return 0;
}

int Fun19() {
	//在学习过程中，我们通常使用 Debug 模式，这样便于程序的调试；
	//而最终发布的程序，要使用 Release 模式，这样编译器会进行很多优化，提高程序运行效率，删除冗余信息。
#ifdef _DEBUG
	printf("正在使用 Debug 模式编译程序...\n");
#else
	printf("正在使用 Release 模式编译程序...\n");
#endif
	return 0;
}
