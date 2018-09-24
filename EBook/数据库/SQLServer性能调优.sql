DECLARE @Num int
SET @Num = 0

WHILE @Num < 10000
BEGIN
INSERT INTO T_User(num) VALUES(@Num * 100000000000000000000000000)
Set @Num = @Num + 1
END

--ƴ��SQL���
DECLARE @Total VARCHAR(MAX) = ''

SELECT @Total += T.num
FROM T_User AS T

SELECT @Total

--ʹ��XMLƴ��
DECLARE @Total VARCHAR(MAX) = ''
SET @Total = (
SELECT T.num + ''
FROM T_User AS T
FOR XML PATH('')
)

SELECT @Total

truncate table T_User

--������������
SELECT * INTO #T_User
FROM T_User
ALTER TABLE #T_User ADD PRIMARY KEY(id) 

SELECT * FROM #T_User

INSERT INTO #T_User
SELECT * FROM T_User

INSERT INTO T_User
OUTPUT inserted.id
VALUES('3')

DELETE FROM T_User
OUTPUT DELETED.ID
WHERE NUM = '3'

UPDATE T_User
SET NUM = '3'
OUTPUT inserted.id,deleted.id
WHERE ID = 4

SELECT * FROM SYS.dm_exec_requests
SELECT * FROM SYS.dm_exec_connections
SELECT * FROM SYS.dm_exec_query_stats

SELECT name,sum(pages_kb) FROM SYS.dm_os_memory_cache_counters
WHERE NAME IN ('SQL Plans','Object Plans','Bound Trees')
GROUP BY name
order BY SUM(pages_kb) desc

--���ܼ��������ʵ��
--1.���Lazy writes/secֵԶԶ����Checkpoint pages/sec��Ϊһ����С��ֵ,�ɴ����ж��ڴ�ѹ��������
--2.ʹ��Page reads/sec��Page writes/secֵ�������ж����ݿ��д��
SELECT TOP 312 * FROM  sys.dm_os_performance_counters
where object_name ='SQLServer:Buffer Manager' order by counter_name

SELECT T.dbid,T.objectid,P.objtype,P.cacheobjtype,T.text 
FROM SYS.dm_exec_cached_plans AS P
CROSS APPLY SYS.dm_exec_sql_text(P.plan_handle) AS T
--WHERE P.usecounts > 5
