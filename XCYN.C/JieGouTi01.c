
//ѧ���Ľṹ��
//�ṹ��Ҳ��һ���������ͣ����ɳ���Ա�Լ����壬���԰�������������͵����ݡ�
struct Student {
	char *name;	//	����
	int num;	//ѧ��
	int age;	//����
	char group;	//����ѧϰС��
	float score;	//����

};

//�ṹ����������ȡ
int Fun35() {
	struct Student stu;
	stu.name = "����";
	stu.num = 20200001;
	stu.age = 18;
	stu.group = 'S';
	stu.score = 625;
	printf("name��%s��num��%d��age��%d��group��%c��score��%.1f", stu.name, stu.num, stu.age, stu.group, stu.score);
	return 0;
}

//��ȡ�ṹ���Ա
int Fun36() {
	struct Student stu;
	stu.name = "����";
	stu.num = 20200001;
	stu.age = 18;
	stu.group = 'S';
	stu.score = 625;
	struct Student stu2;
	stu.name = "����";
	stu.num = 20200001;
	stu.age = 18;
	stu.group = 'S';
	stu.score = 625;
	struct Student *banji;
	banji = &stu;
	//printf("%d", sizeof(*banji));
	//ͨ��ָ���ȡ�ṹ��ĳ�Աʹ�ã�pointer->memberName��->��һ���µ��������ϰ�߳���Ϊ����ͷ����������������ͨ���ṹ��ָ��ֱ��ȡ�ýṹ���Ա����Ҳ��->��C�����е�Ψһ��;��
	//����(*pointer).memberName
	printf("%s\n", banji->name);
	printf("%s\n", (*banji).name);
	printf("%s\n", stu.name);	//ֱ�Ӵӽṹ�������ȡֱ����.����
	return 0;
}

//ʹ��typedefΪ����(Teacher) ��һ������(TeacherViewModel) 
//һ���÷���typedef  oldName  newName;
typedef struct Teacher {
	char *name;	//	����
	int age;	//����
	char sex;	//�Ա�
} TeacherViewModel;

//ʹ��typedef����ı����ṹ��
int Fun37() {
	TeacherViewModel teacher1;
	teacher1.name = "����";
	teacher1.age = 24;
	teacher1.sex = 'M';
	printf("name��%s��age��%d��sex��%c", teacher1.name, teacher1.age, teacher1.sex);
	return 0;
}