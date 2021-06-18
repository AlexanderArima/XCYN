// XCYN.CPLUS.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//
//#include <stdio.h>
//#include <stdlib.h>
#include <cstdio>
#include <string>
#include <iostream>
#include "Student.cpp"

int main()
{
	// 声明命名空间
	using namespace std;

	bool flag;
	int a;
	int b;
	cout << "请输入两个数字，将会输出最大的" << endl;
	cin >> a >> b;
	flag = a > b;
	if (flag) {
		cout << "最大的数是：" << a << endl;
	}
	else {
		cout << "最大的数是：" << b << endl;
	}

	return 0;
}

// 使用命名空间
// 格式：命名空间名称:变量名称or类名or函数or构造体
// ::称为域解析操作符，在C++中用来指明要使用的命名空间。
void UseNameSpace() {
	class CPLUS::Student stu;
	stu.age = 18;
	stu.score = 90.5f;
	stu.say();

}

void UseNameSpace02() {
	using namespace CPLUS;
	class Student stu;
	stu.age = 18;
	stu.score = 90.5f;
	stu.say();

}

// 使用C++自带库中的std命名空间
void UseNameSpace03() {
	// 声明命名空间
	using namespace std;

	// 定义字符串变量
	string str;
	int age;
	cout << "请输入国家名称" << endl;	// endl表示end of line
	cin >> str;
	cout << "请输入建国多少年了" << endl;
	cin >> age;
	cout << str << "已经成立" << age << "年了" << endl;

}

void BoolFunc() {
	// 声明命名空间
	using namespace std;

	bool flag;
	int a;
	int b;
	cout << "请输入两个数字，将会输出最大的" << endl;
	cin >> a >> b;
	flag = a > b;
	if (flag) {
		cout << "最大的数是：" << a << endl;
	}
	else {
		cout << "最大的数是：" << b << endl;
	}

}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
