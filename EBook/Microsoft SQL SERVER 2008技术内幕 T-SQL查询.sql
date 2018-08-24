SELECT DISTINCT Country
FROM T_Customer
ORDER BY CustID

--��ѯ�Ż� 
--DMV
SELECT
  wait_type,	--�ȴ�����
  waiting_tasks_count,	--�����͵ȴ�������
  wait_time_ms,	--�������ܵĵȴ�ʱ��
  max_wait_time_ms,	--���ȴ�ʱ��
  signal_wait_time_ms	--���յ��źŵ���ʼ����֮���ʱ���
FROM sys.dm_os_wait_stats
where wait_type like '%LCK%'
ORDER BY wait_time_ms desc,wait_type;

--�ֶ�����
DBCC SQLPERF('sys.dm_os_wait_stats',CLEAR);