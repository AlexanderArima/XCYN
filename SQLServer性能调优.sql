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