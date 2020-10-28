
//学生的结构体
//结构体也是一种数据类型，它由程序员自己定义，可以包含多个其他类型的数据。
struct Student {
	char *name;	//	姓名
	int num;	//学号
	int age;	//年龄
	char group;	//所在学习小组
	float score;	//分数

};

//结构体的设置与获取
int Fun35() {
	struct Student stu;
	stu.name = "张三";
	stu.num = 20200001;
	stu.age = 18;
	stu.group = 'S';
	stu.score = 625;
	printf("name：%s，num：%d，age：%d，group：%c，score：%.1f", stu.name, stu.num, stu.age, stu.group, stu.score);
	return 0;
}

//获取结构体成员
int Fun36() {
	struct Student stu;
	stu.name = "张三";
	stu.num = 20200001;
	stu.age = 18;
	stu.group = 'S';
	stu.score = 625;
	struct Student stu2;
	stu.name = "张三";
	stu.num = 20200001;
	stu.age = 18;
	stu.group = 'S';
	stu.score = 625;
	struct Student *banji;
	banji = &stu;
	//printf("%d", sizeof(*banji));
	//通过指针获取结构体的成员使用：pointer->memberName，->是一个新的运算符，习惯称它为“箭头”，有了它，可以通过结构体指针直接取得结构体成员；这也是->在C语言中的唯一用途。
	//或者(*pointer).memberName
	printf("%s\n", banji->name);
	printf("%s\n", (*banji).name);
	printf("%s\n", stu.name);	//直接从结构体对象中取直接用.就行
	return 0;
}