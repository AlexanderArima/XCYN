#include "Student.h"
#include "stdio.h"
#include "string.h"

namespace CPLUS {
	// ʹ��namespace�ؼ��ֶ�����һ�������ռ䣬�����ռ������������������ͻ�������˼�ʹʹ�õ���ͬһ�����Ƶ�
	// ������ֻҪ�ڲ�ͬ�������ռ��£������Ҳ�ǲ��ᱨ���
	class Student
	{
	public:
		// ������ı���
		// char *name;
		int age;
		float score;

		// ������ĺ���
		void say() {
			printf("������%d���ɼ���%f", age, score);
		}
	};
}