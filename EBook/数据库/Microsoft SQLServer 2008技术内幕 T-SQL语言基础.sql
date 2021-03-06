--查询1-10的数据
SELECT * FROM 
(
SELECT sum(CODE_ACT_LEN) over(partition by REC_CREATOR) AS TotalCode_Len
      ,ROW_NUMBER() OVER(ORDER BY REC_ID) AS RN
      ,RANK() OVER(ORDER BY REC_ID) AS R
      ,DENSE_RANK() OVER(ORDER BY REC_ID) AS DR
      ,NTILE(10) OVER(ORDER BY REC_ID) AS N
  FROM [BGMES].[dbo].[TTS0091]
) N
WHERE N.RN > 0 AND N.RN <= 10
  
--删除用户id重复的行 只保留最小的值
DELETE FROM TTS0091
WHERE REC_ID NOT IN
(
  SELECT MIN(REC_ID) AS MIN_REC_ID
  FROM TTS0091
  GROUP BY REC_CREATOR
)

--case表达式
SELECT width, case
when width > 100 then '大于100'
else '小于100'
end AS 长度
FROM TRWCU02

SELECT CU02.WIDTH,CU02.*
FROM TRWCU02 AS CU02
WHERE CASE
WHEN CU02.WIDTH > 100 THEN '1'
ELSE '0'
END = '1'

--日期格式和转换，比较

SELECT CASE 
WHEN CAST('2018-01-23T00:00:00' AS DATETIME) = '20180123' THEN 'TRUE'
ELSE 'FALSE'
END 只比较日期是否相等

SELECT CAST('20181001' AS SMALLDATETIME)

SELECT CAST('20000901 07:30:00' AS DATETIME2)

SELECT CAST('19:45:00' AS TIME)

--第二章练习题
--1.求出6月份所有订单
SELECT orderid,orderdate,custid,empid FROM Sales.Orders
WHERE MONTH(orderdate) = 6 and YEAR(orderdate) = 2008
--这种方式可以避免索引失效
SELECT orderid,orderdate,custid,empid FROM Sales.Orders
WHERE orderdate >= '2008-06-01' and orderdate < '2008-07-01'

--3.求出所有名字的字母包含两个以上的a
SELECT * FROM PERSON
WHERE FIRSTNAME LIKE '%a%a%'

--4.求总价格大于10000的所有订单,并按照总价排序
SELECT ORDERID,(NUM * PRICE) AS TOTALVALUE           --错误
FROM SALES.ORDERDETAILS
WHERE NUM * PRICE > 10000
ORDER TOTALVALUE

SELECT ORDERID,SUM(PRICE * NUM) AS TOTALVALUE        --正确
FROM SALES.ORDERDETAILS
GROUP BY ORDERID
HAVING SUM(PRICE * NUM) > 10000
ORDER BY SUM(PRICE * NUM)

--5.返回2007年平均运费最高的三个国家
SELECT TOP 3 SHIPCOUNTRY,AVG(AVGFREIGHT)
FROM SALES.ORDERS
WHERE ORDERDATE >= '2007-01-01' AND ORDERDATE < '2008-01-01'
GROUP BY SHIPCOUNTRY
ORDER BY AVG(AVGFREIGHT) DESC

--6.为每个顾客单独根据订单日期的顺序，来计算其订单的行号

SELECT CUSTID,ORDERDATE,ORDERID,
ROW_NUMBER() OVER(PARTITION BY CUSTID ORDER BY ORDERDATE,ORDERID) AS ROWNUM
FROM SALES.ORDERS
ORDER BY CUSTID,ROWNUM

--7.构建一个SELECT语句，让它根据每个雇员的友好称谓来返回不同的性别，如：对于'Ms.'和'Mrs.'返回'female'；
--对于'Mr.'，返回'male'，对于其他情况，返回'UnKnown'

SELECT EMPID,FIRSTNAME,LASTNAME,TITLEOFCOURTESY,
CASE TITLEOFCOURTESY WHEN 'MS.' THEN 'FEMALE'
WHEN 'MSR.' THEN 'FEMALE'
WHEN 'MR.' THEN 'MALE'
ELSE 'UNKNOWN' END AS GENDER
FROM HR.EMPLOYEES.

--8.返回每个客户ID和所在区域。对输出中的行按照区域排序，NULL值排在最后面

SELECT CUSTID,REGION                 --错误，因为ISNULL()函数有两个参数，并且它的作用是，当行为空时用另一个参数代替之
FROM SALES.CUSTOMERS
WHERE !ISNULL(REGION)
ORDER BY CUSTID
UNION
SELECT CUSTID,REGION
FROM SALES.CUSTOMERS
WHERE ISNULL(REGION)
ORDER BY CUSTID

SELECT CUSTID,REGION				--正确
FROM SALES.CUSTOMERS
ORDER BY CASE WHEN REGION IS NULL THEN 0 ELSE 1 END DESC

CREATE TABLE #T1
(
	digit int
)
--插入十条数据依次为0-9
insert into #T1
values (0),(1),(2),(3),(4),(5),(6),(7),(8),(9)
--利用交叉连接，算出1000个不同的数字
SELECT (T3.digit * 100) + (T2.digit * 10) + T1.digit + 1 AS D2 FROM #T1 AS T1
CROSS JOIN #T1 AS T2
CROSS JOIN #T1 AS T3
ORDER BY D2 

FROM TABLE1 AS T1
JOIN TABLE2 AS T2
ON T1.col1 = T2.col1 AND T1.col2 = T2.col2

--日期函数
--DATEADD(part,n,dt_val)
--part:日期部分，有效值有时，分，秒，天，月，年，季度
--n:增加的数值
--dt_val:原值
SELECT DATEADD(DAY,1,'20180712')
SELECT DATEADD(MONTH,1,'20180712')
SELECT DATEADD(YEAR,1,'20180712')


--求日期的差值
--DATEDIFF(part,dt_val1,dt_val2)
--part:日期部分，有效值有时，分，秒，天，月，年，季度
--公式：dt_val2 - dt_val1
SELECT DATEDIFF(DAY,'20180101','20180712')
SELECT DATEDIFF(MONTH,'20180101','20180712')
SELECT DATEDIFF(YEAR,'20180101','20180712')


CREATE TABLE #NUM
(n int)

DECLARE @i int;
SET @i = 0;
WHILE @i < 100000
BEGIN
INSERT INTO #NUM VALUES(@i);
SET @i = @i + 1;
END

--算出2000年-2018年每天
SELECT DATEADD(DAY,n,'2000-01-01') AS everyday,T91.*
FROM #NUM
LEFT JOIN [BGMES].[dbo].[TTS0091] AS T91
ON DATEADD(DAY,n,'2000-01-01') =  SUBSTRING(T91.REC_CREATE_TIME + '',0,9)
WHERE DATEDIFF(DAY,'2000-01-01','2018-01-01') >= n


--第三章 联接查询 练习题
--创建一个辅助表Nums
CREATE TABLE Nums
(n int)

DECLARE @i int;
SET @i = 0;
WHILE @i < 100000
BEGIN
INSERT INTO Nums VALUES(@i);
SET @i = @i + 1;
END

--1.写一条SQL语句，把所有雇员记录复制5次

SELECT Empid,FirstName,LastName,n
FROM T_Employee AS emp CROSS JOIN Nums
WHERE NUMS.n <= 5 AND Nums.n > 0

--2.写一个查询，为每个雇员和从2009-06-12至2009-06-16日范围内每天返回一行。
SELECT T_Employee.Empid,T1.dt
FROM T_Employee Cross Join 
(
SELECT DATEADD(DAY,n,'2009-06-12') AS dt
FROM Nums
WHERE n <= DATEDIFF(DAY,'2009-06-12','2009-06-16')
) AS T1
Order by T_Employee.Empid

--3.返回来自美国的客户，并为每一个客户返回其订单总数和交易总额
SELECT c.CustID,COUNT(o.OrderID) AS NumOrders,SUM(od.qty) AS TotalQty
FROM T_Customer AS c
LEFT JOIN T_Order AS o On c.CustID = o.CustID
LEFT JOIN T_OrderDetail AS od ON o.OrderID = od.OrderID
WHERE c.Country = 'USA'
GROUP BY c.CustID

--4.返回客户及其订单信息，包括没有下过任何订单的客户
SELECT c.CustID,o.OrderID
FROM T_Customer AS c LEFT JOIN T_Order AS o
ON c.CustID = o.CustID

--5.返回没有下过订单的用户
SELECT c.CustID
FROM T_Customer AS c LEFT JOIN T_Order AS o
ON c.CustID = o.CustID
WHERE o.OrderID is null

SELECT * 
FROM T_Order AS o1
WHERE OrderID = 
(
	select MAX(OrderID)
	from T_Order AS o2
	where o1.CustID = o2.CustID
)

--获取所有下过订单的美国用户
SELECT CustID
FROM T_Customer AS c
WHERE Country = 'USA' AND
EXISTS(
SELECT * 
FROM T_Order AS o
WHERE c.CustID = o.CustID
)

--返回ORDER表最后一天所有订单
SELECT * FROM T_Order
WHERE OrderDate = 
(
	SELECT MAX(OrderDate) FROM T_Order
)

--返回订单数量最多的客户的所有订单
SELECT * FROM T_Order
WHERE CustID = 
(
	SELECT top 1 CustID FROM T_Order
	GROUP BY CustID
	Order by Count(CustID) DESC
)

--返回2012年2月15日后没有处理过订单的雇员
SELECT * FROM T_Employee
WHERE Empid NOT IN
(
	SELECT CustID FROM T_Order
	WHERE OrderDate > '2017-02-15'
)

--返回在客户表出现过，但没有在雇员表中出现过的国家

SELECT C.Country FROM T_Customer AS C
WHERE C.Country NOT IN
(
	SELECT  E.Country FROM T_Employee AS E
)

--返回每一个客户最后一条订单
SELECT * FROM T_Order AS O1
WHERE O1.OrderDate IN
(
SELECT MAX(O2.OrderDate) AS OrderID FROM T_Order AS O2
WHERE O1.CustID = O2.CustID
) 
--此处的MAX(OrderDate)表示的是，每个客户的最大订单

--生成一个由10个数字组成的虚拟辅助表，要求不能使用循环
SELECT 1 AS n
UNION ALL
SELECT 2
UNION ALL
SELECT 3
UNION ALL
SELECT 4
UNION ALL
SELECT 5
UNION ALL
SELECT 6
UNION ALL 
SELECT 7 
UNION ALL
SELECT 8 
UNION ALL
SELECT 9
UNION ALL
SELECT 10

--也可以使用这种方式来解决该问题
SELECT n 
FROM (VALUES(1),(2),(3)) AS #Nums(n)

--求出2017年1月有订单且2月份没有订单的客户
SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-01-01' and OrderDate < '2017-02-01'

EXCEPT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-02-01' and OrderDate < '2017-03-01'

--求出在2017年1月和2月都有订单的客户
SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-01-01' AND OrderDate < '2017-02-01'

INTERSECT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-02-01' AND OrderDate < '2017-03-01' 

--返回在2017年1月和2月都有订单，但在2018年没有订单的客户
SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-01-01' AND OrderDate < '2017-02-01'

INTERSECT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2017-02-01' AND OrderDate < '2017-03-01'

EXCEPT

SELECT CustID
FROM T_Order
WHERE OrderDate >= '2018-01-01' AND OrderDate < '2019-01-01' 

--查询T_Customer和表T_Employee，让T_Customer表里的数据都要在T_Employee表前面，并让他们按照国家排序

SELECT 1 AS SortID, Country
FROM T_Customer AS c

UNION ALL

SELECT 2 AS SortID,Country
FROM T_Employee AS e

ORDER BY SortID,Country

SELECT CURRENT_TIMESTAMP

--VALUES子句，创建虚拟表

SELECT * FROM 
(VALUES(1,'USA'),(2,'CHI'),(3,'FRA'))
AS O(ID,COUNTRY)

--SELECT VALUES多个行，可以进行原子性操作

INSERT INTO T_Order
VALUES
(1,1,'冰箱','2017-01-01'),
(2,1,'洗衣机','2017-01-01'),
(3,1,'电视','2017-01-01')

INSERT INTO #T_Order(CustID,Name,OrderDate)
SELECT 2,'电视','2018-01-01' 
UNION ALL
SELECT 2,'电视','2018-01-02'
go

--也可以套用之前的VALUES子句，创建虚拟表

INSERT INTO #T_Order(CustID,Name,OrderDate)
SELECT * FROM 
(VALUES(3,'电视','2018-01-01'),(3,'电视','2018-01-02'))
AS O(CustID,Name,OrderDate)

use Test
if OBJECT_ID('P_InsertOrder','p') is not null
drop proc P_InsertOrder
go
create PROCEDURE P_InsertOrder
as
begin
SELECT * FROM (VALUES
(1,'冰箱','2017-01-01'),
(1,'洗衣机','2017-01-01'),
(1,'电视','2017-01-01'))
AS O(OrderID,Name,OrderDate)
end
GO

--执行INSERT EXEC
INSERT INTO T_Order(CustID,Name,OrderDate)
EXEC P_InsertOrder

--执行SELECT INTO语句复制表
SELECT * 
INTO #T_Order
FROM T_Order WHERE T_Order.OrderID > 5

SELECT * FROM #T_Order

INSERT INTO T_Order
VALUES
(1,'冰箱','2017-01-01')
SELECT @@IDENTITY
SELECT SCOPE_IDENTITY()

--DELETE语句带上JOIN连接
DELETE FROM O
FROM T_Order AS O
JOIN T_Customer AS C 
ON O.CustID = C.CustID
WHERE C.Country = 'China'

--也可以使用EXISTS+子查询解决问题
DELETE FROM T_Order
WHERE EXISTS(
SELECT * FROM T_Customer AS C
WHERE C.CustID = T_Order.CustID AND C.Country = 'China'
)


SELECT * FROM T_Customer

declare @i AS int = 123;
update T_Customer
set @i = CustID = CustID + 1
select @i

SELECT * INTO T_Employee2
FROM T_Employee

--MERGE语句
MERGE T_Employee AS e1
USING T_Employee2 AS e2
ON e1.Empid = e2.Empid
WHEN Matched THEN		--以USING的表为准，修改MERGE的表
UPDATE SET 
e1.FirstName = e2.FirstName,e1.LastName = e2.LastName,e1.Country = e2.Country
WHEN NOT Matched THEN   --以USING的表为准，新增MERGE的表
INSERT(FirstName,LastName,Country) VALUES
(e2.FirstName,e2.LastName,e2.Country)
WHEN NOT MATCHED BY SOURCE THEN  --以USING的表为准，删除MERGE的表
UPDATE SET
e1.IsDelete = 1;

---------------------------检测阻塞---------------------------

--返回 SQL Server 2017 中有关当前活动的锁管理器资源的信息。 
--向锁管理器发出的已授予锁或正等待授予锁的每个当前活动请求分别对应一行。

SELECT 
request_session_id AS spid,   --当前拥有该请求的会话 ID。 对于分布式事务和绑定事务，拥有请求的会话 ID 可能不同。 该值为 -2 时，指示该请求属于孤立的分布式事务。 该值为 -3 时，指示请求属于延迟的恢复事务，例如因其回滚未能成功完成而延迟恢复该回滚的事务。
resource_type AS restype,     --表示资源类型。 该值可以是下列值之一：DATABASE、FILE、OBJECT、PAGE、KEY、EXTENT、RID、APPLICATION、METADATA、HOBT 或 ALLOCATION_UNIT。
resource_database_id AS dbid, --此资源位于其范围之内的数据库的 ID。 由锁管理器处理的所有资源均按该数据库 ID 划分范围。
DB_NAME(resource_database_id) AS dbname,
resource_description AS res,
resource_associated_entity_id AS resid,
request_mode AS mode,		  --锁的性质
request_status AS sta      --锁的状态
FROM SYS.dm_tran_locks    

--根据Session ID查询正在阻塞的SQL语句

SELECT 
session_id AS spid,text
FROM SYS.dm_exec_connections
cross apply sys.dm_exec_sql_text(most_recent_sql_handle) AS ST
WHERE session_id in(61,51)

--正在执行的会话
SELECT 
session_id AS spid,
login_time,           --建立会话的时间
host_name,			  --客户端工作站的名称
program_name,         
login_name,           --会话所使用SQL Server登录名
nt_user_name,         --客户端Windows用户名
last_request_start_time, --最后一次会话请求开始时间
last_request_end_time    --最后一次会话请求结束时间
FROM SYS.dm_exec_sessions

--这个视图每一行表示一个活动的请求，当blocking_session_id大于0时，就能查到阻塞的请求。
SELECT 
session_id AS spid,
blocking_session_id,
command,
sql_handle,
database_id,
wait_type,
wait_time,
wait_resource
FROM SYS.dm_exec_requests
WHERE blocking_session_id > 0
--这样就能比较容易地识别阻塞链所涉及到的会话，命令，等待时间，资源等信息。

KILL 55   --关闭阻塞请求

--获取事务隔离级别
DBCC USEROPTIONS 

UPDATE T_OrderDetail
SET qty = 100
WHERE OrderID = 1

KILL 56

DBCC USEROPTIONS 

SELECT * FROM T_OrderDetail

SELECT * FROM T_Customer

UPDATE T_OrderDetail
SET qty = 100
WHERE OrderID = 1

DELETE FROM T_OrderDetail
WHERE OrderID = 10 AND qty = 100

-----------------------------可编程对象-----------------------------

DECLARE @A AS VARCHAR(50);
SET @A = 1

SET @A = (SELECT Name FROM T_Order WHERE OrderID = 1)

--SELECT @A AS A

DECLARE @B AS INT;

SET @B = (SELECT OrderID FROM T_Order WHERE OrderID = 1)

SELECT @A = Name,@B = OrderID
FROM T_Order
WHERE OrderID = 1

SELECT @A AS A,@B AS B

DECLARE @C AS INT = 10;

PRINT '11'

GO
-------------批处理-------------
DECLARE @A AS INT = 10;
PRINT @A;
GO
PRINT @A;
GO

IF OBJECT_ID('V_Shop','C') IS NOT NULL DROP TABLE V_Shop
GO
CREATE VIEW V_Shop AS
SELECT * FROM T_Shop
GO

CREATE TABLE #T(ID INT IDENTITY)

SET NOCOUNT ON; --防止DML输出影响行数

INSERT INTO #T DEFAULT VALUES
GO 100

SELECT * FROM #T

SELECT CURRENT_TIMESTAMP,YEAR(CURRENT_TIMESTAMP),DATEADD(DAY,1,CURRENT_TIMESTAMP)

--判断今天是不是今年的最后一天
IF YEAR(CURRENT_TIMESTAMP) = YEAR(DATEADD(DAY,1,CURRENT_TIMESTAMP))
BEGIN
	IF MONTH(CURRENT_TIMESTAMP) = 12
		PRINT('这个月是今年的最后一个月');
	ELSE
		PRINT('这个月不是今年的最后一个月');
END
ELSE 
PRINT('今天是今年的最后一天');

SELECT * FROM SYS.objects

GO

SET NOCOUNT OFF;

DECLARE @i AS int = 0;
WHILE @i < 10
BEGIN
IF @i = 5
BEGIN
	SET @i = @i + 1;
	CONTINUE;
END
PRINT(@i);
SET @i = @i + 1;
END



-------------局部临时表和全局临时表-------------

SELECT * 
INTO #T_Order
FROM T_Order

SELECT * 
FROM #T_Order

SELECT * 
INTO ##T_Order
FROM T_Order

-------------表变量-------------

DECLARE @T_Order table(
	ID int,
	Name varchar(50)
)

INSERT INTO @T_Order(ID,Name)
SELECT OrderID,Name
FROM T_Order

INSERT INTO @T_Order(ID,Name)
VALUES(100,'微波炉')

SELECT * FROM @T_Order

GO

-----------动态SQL-----------

DECLARE @Msg AS varchar(100);

SET @Msg = 'PRINT ''HELLO WORLD''';

EXEC(@Msg)

print @msg

--------sp_executesql----------

DECLARE @sql AS nvarchar(100) = N'SELECT * FROM T_Order Where OrderID > @OrderID And CustID = @CustID'
-- And CustID = @CustID
exec sp_executesql
@stmt = @sql,
@params = N'@OrderID AS INT,@CustID AS INT',
@OrderID = 1,
@CustID = 1

SELECT NEWID()

SELECT CAST(RAND() * 31 AS INT)

-----------创建一个标量函数-------------

--计算两个事件的年份差值
IF OBJECT_ID('GetAge') is not null drop Function GetAge
GO

CREATE FUNCTION GetAge
(
	@eventday as datetime,
	@birthday as datetime
)
returns int
as
begin
	return datediff(YEAR,@birthday,@eventday) - 
	       case when (100 * month(@eventday) + day(@eventday)) < (100 * month(@birthday) + day(@birthday))
	       then 1 else 0 End
end
Go

---------------结束--------------

SELECT *,dbo.GetAge(GETDATE(),OrderDate) as Age FROM T_Order

update T_Order
set OrderDate = DATEADD(month,-3,OrderDate)
where OrderID = 5

---------------异常处理--------------

begin try
set nocount on
select 10/CAST(RAND() * 2 AS INT)
print 'yes'
end try

begin catch
EXEC P_ErrorMessage
end catch

--消息对照表
SELECT * FROM SYS.messages
WHERE message_id = 8134

SELECT COALESCE(ORDERDATE,NULL,'2018-01-05')
FROM T_Order
WHERE OrderID = 22

SELECT COALESCE(EXPRESSION1,EXPRESSION2,...)

SELECT CASE WHEN EXPRESSION1 IS NULL THEN EXPRESSION1,
       CASE WHEN EXPRESSION2 IS NULL THEN EXPRESSION2,
       ...