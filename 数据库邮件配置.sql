--�ʼ������б�
select * from msdb.dbo.sysmail_allitems

--�ʼ�������־
select * from msdb.dbo.sysmail_event_log

--�ʼ��������ļ�
select * from msdb.dbo.sysmail_profile

--�洢���̷����ʼ�
exec msdb.dbo.sp_send_dbmail
@profile_name = 'MyConfig',
@recipients = 'xiecheng900424@qq.com',
@body = '����ѧԺSQLServer�γ�',
@subject = '����ѧԺ'

