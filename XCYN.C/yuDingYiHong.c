#include <stdio.h>
#include <stdlib.h>

int Fun17() {
	//Ԥ�����
	printf("Date : %s\n", __DATE__);
	printf("Time : %s\n", __TIME__);
	printf("File : %s\n", __FILE__);
	printf("Line : %d\n", __LINE__);
	system("pause");
	return 0;
}

int Fun18() {
	//��������
#if _WIN32
	printf("This is Windows Platform");
#elif __linux__
	printf("This is Linux Platform");
#else
	printf("This is unknown Platform");
#endif
	//printf("WIN32��%d", _WIN32);	//print : WIN32��1
	//printf("__cplusplus��%d", __cplusplus);	//__cplusplus��ʾʹ��C++����
	//#error �������		//#error ָ�������ڱ����ڼ����������Ϣ������ֹ����ı��룬
	return 0;
}

int Fun19() {
	//��ѧϰ�����У�����ͨ��ʹ�� Debug ģʽ���������ڳ���ĵ��ԣ�
	//�����շ����ĳ���Ҫʹ�� Release ģʽ����������������кܶ��Ż�����߳�������Ч�ʣ�ɾ��������Ϣ��
#ifdef _DEBUG
	printf("����ʹ�� Debug ģʽ�������...\n");
#else
	printf("����ʹ�� Release ģʽ�������...\n");
#endif
	return 0;
}
