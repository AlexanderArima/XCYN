#include "Student.h"
#include "stdio.h"
#include "string.h"

namespace CPLUS {
	// 使用namespace关键字定义了一个命名空间，命名空间可以用来避免命名冲突。两个人即使使用的是同一个名称的
	// 变量，只要在不同的命名空间下，编译后也是不会报错的
	class Student
	{
	public:
		// 类包含的变量
		// char *name;
		int age;
		float score;

		// 类包含的函数
		void say() {
			printf("年龄是%d，成绩是%f", age, score);
		}
	};
}