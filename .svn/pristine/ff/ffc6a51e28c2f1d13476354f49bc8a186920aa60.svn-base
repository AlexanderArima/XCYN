--邮件发送列表
select * from msdb.dbo.sysmail_allitems

--邮件发送日志
select * from msdb.dbo.sysmail_event_log

--邮件的配置文件
select * from msdb.dbo.sysmail_profile

--存储过程发送邮件
exec msdb.dbo.sp_send_dbmail
@profile_name = 'MyConfig',
@recipients = 'xiecheng900424@qq.com',
@body = '极客学院SQLServer课程',
@subject = '极客学院'

