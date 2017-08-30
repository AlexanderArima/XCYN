--顺序+判断语句
/*
declare @i as int,@myVal as int
set @i = 1
set @myVal = 0

if @i > 0
	set @myVal = 100
else 
	begin
		set @myVal = 10
		print Convert(char(10),@myVal);
	end
*/

--顺序+判断+循环
/*
declare @i as int,@myVal1 as int,@myVal2 as int
set @i = 0
set @myVal1 = 0
set @myVal2 = 0

while @i < 100
	begin
		if @i % 2 <> 0
			set @myVal1 = @myVal1 + @i
		else 
			set @myVal2 = @myVal2 + @i
		set @i = @i + 1;
	end

select @i,@myVal1,@myVal2
*/

--判断+循环
/*
declare @i as int,@myVal1 as int,@myVal2 as int
set @i = 0
set @myVal1 = 0
set @myVal2 = 0

table_loop:
	if @i % 2 <> 0
		set @myVal1 = @myVal1 + @i
	else
		set @myVal2 = @myVal2 + @i
	set @i = @i + 1

	if @i < 100
		goto table_loop --跳转到标签处

select @i,@myVal1,@myVal2
*/

--等待执行
/*
waitfor delay '00:00:03'--等待3s
print('3')

waitfor time '09:50:30'--等待时间走到09：50：30
print('09:50:30')
*/

--批处理执行go
/*
create table #a
(
	id int
)
go
insert into #a(id) values(1)
go
insert into #a(id) values(2)
insert into #a(id) valuee(3)
go

--2,3都不会插入到表中
select * from #a
*/

--变量的运算优先级，int在char之前
--select 1.5 + '1'

--变量的作用域
/*
declare @i as int
set @i = 10
select @i
go
--i变量的作用域在批处理之内可见
select @i
*/

--为变量赋值
/*
declare @i as int
set @i = 100

--在列表中引用单行变量
select @i = avg(id) from zcp_post

select @i

--在列表中引用多行变量，则取出结果集中的最后一行中的表达式作为返回值
select @i = id from zcp_post

select @i
*/

--赋值运算符
--使用赋值运算符，定义列标题和列值
--select t_id = 0,s_id = id,* from zcp_post

--逻辑运算符
/*
--any
select * from zcp_post
where id > any(select post_id from zcp_post_collect)

--between
select * from zcp_post
where 1 between 1 and 5

--exists 如果子查询中包含一些行，则返回true
select * from zcp_post
where exists(select * from zcp_post_collect where id = 1)

--in如果操作数中等于表达式中的一个，返回true
select * from zcp_post where id in(1,2,3,4)

--like如果操作数与一个模式匹配，返回true
-- '_'表示一个字符，'%'表示一个字符串
select * from zcp_post where title like '_111'
*/
--not取反，And，Or

/*
--聚合函数
create table #a(num int)
go
insert into #a values(10)
insert into #a values(20)
insert into #a values(20)
insert into #a values(20)
insert into #a values(30)
insert into #a values(50)
insert into #a values(50)

--avg([All|Distinct] expression)
--默认是All，对所有的值进行聚合，Distinct只在唯一的值进行聚合
select avg(num) from #a
select avg(distinct num) from #a
*/

--配置函数

--select @@MAX_CONNECTIONS as 最大连接数,@@VERSION as 版本信息,@@DATEFIRST as 每周第一天,@@LANGUAGE as 语言,@@SERVERNAME as 本地服务器名称,@@SERVICENAME as 命名实例,@@SPID as 会话ID

--游标函数
--select @@CURSOR_ROWS as 游标结果集的行数,@@FETCH_STATUS as Fetch语句的状态

--日期和时间函数
/*
declare @time_now as datetime
set @time_now = '2017-07-04 00:00:00'
--DATEADD函数，操作时间
select DATEADD(YYYY,1,@time_now) as 年,DATEADD(QQ,1,@time_now) as 季度,DATEADD(MM,1,@time_now) as 月,
DATEADD(WEEK,1,@time_now) as 周,DATEADD(DAY,1,@time_now) as 天,DATEADD(HOUR,1,@time_now) as 小时,
DATEADD(MINUTE,1,@time_now) as 分钟,DATEADD(SECOND,1,@time_now) as 秒,DATEADD(MS,1,@time_now) as 毫秒
go
*/

--DATEDIFF函数,返回时间差值

--declare @start_time as datetime,@end_time as datetime
--set @start_time = '2017-07-04 12:00:00'
--set @end_time = '2017-07-04 20:45:00'
--select DATEDIFF(SECOND,@start_time,@end_time)

--select DATEPART(DAY,@start_time) as 一个月的日期,DATEPART(DAYOFYEAR,@start_time) as 一年的日期,
--	   DATEPART(WEEKDAY,@start_time) as 一周的日期,DATEPART(WEEK,@start_time) as 一年的星期,
--	   DATEPART(hour,@start_time) as 小时

--DATENAME函数，返回日期中的指定部分
--select DATENAME(MONTH,@start_time)

--构造时间日期
--select DATEFROMPARTS(2017,7,4)
--select DATETIMEFROMPARTS(2017,7,4,12,00,00,00)
--select SMALLDATETIMEFROMPARTS(2017,7,4,13,0)
--select TIMEFROMPARTS(12,01,50,0,0)

----得到当前的时间
--select GETDATE()
--select GETUTCDATE()

----返回年月日
--select YEAR(GETDATE())
--select DAY(GETDATE())
--select MONTH(GETDATE())


--数学函数

--取出0-1的随机数
--select rand()

----四舍五入
--select round(rand(),2) * 100

----平方根
--select SQRT(2)

----平方
--select SQUARE(2)

----自然对数
--select LOG(2)

--数据类型转换
--CAST(expression)
--select CAST(1111111111 as char(10)) + 'a'

--CONVERT(data_type,expression)
--select CONVERT(char(10),1111111111) + 'a'

--时间转换为字符串
--select CONVERT(varchar(30),GETDATE(),121)

--字符串转浮点型
--select CAST('12.5' as float) + 10


--字符串函数

--ASCII 返回字符的ASCII值
--select ASCII('a') --97
--select ASCII('A') --65
--select ASCII('1') --49

----CHAR(integer_expression) 将数字转成ASCII字符 integer_expression范围在0-255之间
--select CHAR(97) --a
--select CHAR(65) --A
--select CHAR(49) --1

--select CHAR(0) --空格
--select CHAR(254) --NULL

--CHARINDEX(expression1,expression2,[start_location])
--返回字符串中指定表达式的开始位置
--select * from zcp_post
--where CHARINDEX('资料分析',title) > 0

--select * from zcp_post
--where title like '%资料分析%'

--LEFT(char_expression,int_expression)
--返回字符串从左边开始置顶个数的字符

--LEN(string_expression) 字符串长度
--select LEFT(jzrq,4) as 年份,LEN(jzrq) as 长度 from zcp_lottery_data

--LOWER转小写，UPPER转大写
--select LOWER(title) as 小写,UPPER(title) as 大写,* from zcp_post

--select * from zcp_post
--where id = 1

--update zcp_post
--set title = '   [资料分析]116期:【内部资料一头】主打六码，跟上让你赚够一百万!    '
--where id = 1

--LTRIM(expression)
--删除字符串左侧所有空格
--select LTRIM(title),LEN(LTRIM(title)),title,LEN(title),RTRIM(title),LEN(RTRIM(title)) from zcp_post

--PATINDEX('%patten%,expression)
--返回符合模式的字符串的下标
--select PATINDEX('%a%','i am jack')

--REPLACE(expression1,expression2,expression3)
--将expression1中的所有expression2替换为expression3
--select REPLACE('abcdefghijklmnopqrstuvwxyz','a','1')

--REPLICATE(char_expression,int_expression)
--连续输出char_expression重复int_expression次
--select REPLICATE('abc',3)

--REVERSE逆向输出字符串
--select REVERSE('abcdefg')

--RIGTH返回字符串从右开始输出个数的字符
--select * from zcp_lottery_data
--where RIGHT(jzrq,2) = '31'
--order by LEFT(jzrq,4)

--RTRIM删除字符右侧的空格

--SUBSTRING(char_expression,start,length)
--从start开始截取length个字符，从char_expression中
--select SUBSTRING('abcdefg',1,2)

--SELECT * 
--FROM zcp_post
--WHERE LEFT(category_id,2) = 61

--判读数据库是否存在
--select DB_ID('whnk1')

--OBJECT_ID查询表，视图，存储过程，函数是否存在
--select OBJECT_ID('ball_sort')

--获取唯一标识符
--select NEWID()

--DROP TABLE #c
--CREATE TABLE #c
--(
--	id int IDENTITY,
--	p_id int,
--	price money,
--	name varchar(20) 
--)

----添加主键约束
--ALTER TABLE #c ADD CONSTRAINT PK_C_id PRIMARY KEY(id)

----添加唯一约束
--ALTER TABLE #c ADD CONSTRAINT UNI_C_P_ID UNIQUE(p_id)

----添加CHECK约束
--ALTER TABLE #c ADD CONSTRAINT CHK_C_price CHECK(price >= 0)

----增加表的列
--ALTER TABLE #c ADD email VARCHAR(100)

----SP_RENAME [@objname = ] 'object_name',[@newname = ] 'new_name' ,[@objtype = ] 'object_type'
----'object_name'格式为table.column，new_name指定对象的新名称，object_type要重命名的对象的类型
----修改列名
--EXEC SP_RENAME 'link.titles','title','column';

----修改列的数据类型
--ALTER TABLE link ALTER COLUMN title nvarchar(255)

--INSERT INTO #c(p_id,price,name) VALUES (8,0,'A','CC@QQ.COM'),(6,2,'B','DD@QQ.COM')
--SELECT @@IDENTITY

--SELECT * FROM #c

----删除表的约束和列
--ALTER TABLE #c
--DROP CONSTRAINT UNI_C_P_ID

--ALTER TABLE #c
--DROP COLUMN p_id

----列出表中的约束 sp_help
--exec sp_help zcp_post


----表变量
----声明一个表变量
--DECLARE @tableVar TABLE (id int)
----复制表
--insert into @tableVar(id) 
--select id from users

--select * from @tableVar

----临时表
--create table #t1(id int)

--insert into #t1 
--select id from users

--select * from #t1

--exec sp_help zcp_lottery_data

--drop table #c

--CREATE TABLE #c
--(
--	id int NOT NULL,
--	name varchar(50),
--)
--GO

--CREATE UNIQUE NONCLUSTERED INDEX IX_C
--ON #c(id)
--GO

--INSERT INTO #c(id,name) VALUES (3,'c'),(2,'b'),(1,'a')

--select * from #c

--select * from #c WITH (INDEX(IX_C))

--第四章 查询操作
--CREATE TABLE #c
--(
--	ProductID int NOT NULL,
--	MadeFrom CHAR(20),
--	Sales MONEY NOT NULL
--)

--INSERT INTO #c
--VALUES (1,'China',100.00),(1,'China',100.00),(2,'China',80.00),(2,'China',80.00),
--(3,'China',90.00),(4,'USA',200.00)

--delete from #c

--SELECT ProductID,SUM(Sales)
--FROM #c
--WHERE MadeFrom = 'China'
--GROUP BY ProductID
--HAVING SUM(Sales) > 150
--ORDER BY ProductID

----CASE 语句
--SELECT 
--case when point > 1000 then '高级用户'
--     when point > 500 and point <= 1000 then '中级用户'
--	 when point > 100 and point <= 500 then '初级用户'
--	 else '游客'
--	 end as N'用户等级'
--	 ,point as N'积分'
--	 ,user_name
--	 ,user_name + ':' + (CAST(point as varchar(20))) as 用户积分
--	 ,$IDENTITY   --特殊关键字$IDENTITY代表关键字具有IDENTITY的属性列
--	 ,DATEDIFF(ww,reg_time,GETDATE()) as 'sum'
--FROM users
--ORDER BY POINT DESC


--WHERE语句筛选

----BETWEEN关键字检索两个指定值之间的所有值
--select * from users
--where point between 10 and 100

----两者之间等价--
--select * from users
--where point >= 10 and point < 100

----NOT BETWEEN 查找指定范围之外的所有行
--SELECT * FROM users
--WHERE point NOT BETWEEN 10 AND 100

----等价于--
--SELECT * FROM users
--WHERE point < 10 OR point > 100

-- NULL与NOT NULL --
--CREATE TABLE #C
--(
--	ID INT,
--	NAME VARCHAR(20),
--)

--INSERT INTO #C(ID,NAME) VALUES(1,N'BEIJING')
--INSERT INTO #C(ID,NAME) VALUES(2,N'SHANGHAI')
--INSERT INTO #C(ID,NAME) VALUES(3,NULL)
--INSERT INTO #C(ID,NAME) VALUES(4,N'BEIJING')

--SELECT * FROM #C
--WHERE NAME = N'BEIJING'

----加上IS NULL将所有为空的也带上去
--SELECT * FROM #C
--WHERE NAME <> N'BEIJING' OR NAME IS NULL


----GROUP BY--
--DROP TABLE #C
--CREATE TABLE #C
--(
--	ColA int,
--	ColB varchar(20),
--	ColC int,
--)

--INSERT INTO #C 
--VALUES
--(NULL,NULL,NULL),
--(NULL,NULL,NULL),
--(1,'ABC',1),
--(1,'DEF',1),
--(1,'GHI',1),
--(2,'JKL',1),
--(2,'MNO',1);

----需要注意的是使用聚合函数对NULL进行计算时，NULL不会在计算内
--SELECT ColA,count(ColB),SUM(ColC)
--from #C
--Group by ColA
--having ColA is null

--SELECT count(ColB),SUM(ColC)
--from #C

----使用HAVING子句中可以包含聚合函数
--SELECT point,count(point) as 'count'
--FROM users
--GROUP BY point
--HAVING count(point) > 10


----ORDER BY子句中不能包含聚合函数，但是可以包含聚合函数的别名
--SELECT point,count(point) as 'count'
--FROM users
--GROUP BY point
--ORDER BY COUNT 

--SELECT TOP 100 PERCENT * FROM USERS


-------------------------第六章 子查询-----------------------
--create table #order
--(
--	orderID int,
--	orderDate datetime,
--	skipTo char(20),
--)

--create table #orderDetail
--(
--	orderID int,
--	productID int,
--	orderQty int,
--	unitPrice money,
--	lineTotal as isnull(unitPrice * orderQty,0.0),
--)

--insert into #order values
--(1,'2017-07-31','shanghai'),
--(2,'2017-08-01','beijing')

--insert into #orderDetail values
--(1,714,1,500),
--(1,715,2,250),
--(2,716,1,300)

--select * from #order
--select * from #orderDetail

----查询每个订单的总价
--select orderID,orderDate,(select sum(lineTotal) from #orderDetail where #orderDetail.orderID = #order.orderID) as orderTotal
--from #order

----等价于
--select #order.orderID,max(#order.orderDate),sum(#orderDetail.lineTotal)
--from #order left join #orderDetail
--on #order.orderID = #orderDetail.orderID
--group by #order.orderID

--create table #customer
--(
--	custID int,
--	custName char(50),
--	city char(50),
--)

--create table #orderHeader
--(
--	custID int,
--	orderID int,
--	orderDate datetime,
--	shipTo char(20),
--)

--insert into #customer values
--(1,'ZhangHongYu','beijing'),
--(2,'LiMing','shanghai'),
--(3,'SunLiHuan','beijing'),
--(4,'WangGang','beijing')

--insert into #orderHeader values
--(1,110,'2017-07-01','fangshan,beijing'),
--(1,111,'2017-07-01','handan,beijing'),
--(2,113,'2017-07-02','pudong,shanghai'),
--(3,114,'2017-07-03','chongwen,beijing'),
--(null,115,'2017-07-04','langfang,beijing')

----in关键字(含有in关键字的可以是独立子查询也可以是相关子查询)
--select * from #orderHeader
--where custID in(select #customer.custID from #customer where city = 'beijing')

----exists关键字(含有exists的是相关子查询)
--select * from #orderHeader
--where exists
--(
--select * 
--from #customer 
--where #customer.custID = #orderHeader.custID and #customer.city = 'beijing'
--)

----含有not in和not exists的子查询
--select * from #customer
--where not exists(
--select * from #orderHeader 
--where custID = #customer.custID
--)

----等价于
--select * from #customer
--where custID not in(
--select custID from #orderHeader
--where custID is not null
--)

----三值逻辑 null和任何比较运算符运算时始终返回Unknown，除了与is null谓词计算时才返回true


----课程表
--create table #lessons
--(
--	studentName char(20),
--	lessonNbr int,
--	lessonStat char(4) check(lessonStat in('DONE','WAIT')),
--)

--insert into #lessons values
--('ken',1,'DONE'),
--('ken',2,'WAIT'),
--('ken',3,'WAIT'),
--('Nan',1,'WAIT'),
--('Nan',2,'WAIT'),
--('Tom',1,'DONE'),
--('Tom',2,'DONE'),
--('Tom',3,'DONE'),
--('Bob',1,'DONE'),
--('Bob',2,'WAIT')

--select * from #lessons

----找出课程1已经修完，但是其他课程没有修完的学生，并且选修的课程等于3

--select studentName
--from #lessons as t1
--where lessonNbr = 1 and lessonStat = 'DONE' and 'WAIT' = ALL(
--select lessonStat from #lessons as t2
--where lessonNbr <> 1 and t1.studentName = t2.studentName
--) and t1.studentName in (
--select t3.studentName from #lessons as t3
--group by t3.studentName
--having count(t3.studentName) = 3
--)

----等价于

--select studentName
--from #lessons as t1
--where lessonNbr = 1 and lessonStat = 'DONE' and 'WAIT' = ALL(
--select lessonStat from #lessons as t2
--where lessonNbr <> 1 and t1.studentName = t2.studentName
--) and 3 = (
--select count(t3.studentName) from #lessons as t3
--where t1.studentName = t3.studentName
--)

----等价于
--select studentName
--from #lessons as t1
--where lessonNbr = 1 and lessonStat = 'DONE' and 'WAIT' = ALL(
--select lessonStat from #lessons as t2
--where lessonNbr <> 1 and t1.studentName = t2.studentName
--) and exists(
--select * from #lessons as t3
--where t1.studentName = t3.studentName
--group by t3.studentName
--having count(t3.studentName) = 3
--)

----心得:子查询一定要注意去除笛卡尔积，也就是说和主表要有关联!

--通过比较运算符引入的子查询选择列表只能包括一个表达式或列名称
--外部查询中where子句的列必须与子查询列表中的列具有数据类型的兼容性

--declare @t table(id int,name varchar(50))
--insert into @t values(10,'a')
--insert into @t values(11,'b')
--insert into @t values(12,'c')
--insert into @t values(4,'d')

--select * from zcp_formula
--where category_id = any(select top 99999 id from @t order by name)

----Order By语句不能用于子查询，但是指定了top关键字后，就可以使用了

--select * from zcp_formula as t1
--where category_id in (select t2.id from zcp_formula_category as t2 where state = 1)



-----------------------------第七章 联接查询---------------------------

--select * from zcp_formula as t1
--left join zcp_formula_category as t2 
--on CAST(t1.category_id as varchar(50)) = CAST(t2.id as varchar(50))


--select * from zcp_formula as t1
--join zcp_formula_category as t2 
--on Convert(varchar(50),t1.category_id) = Convert(varchar(50),t2.id)

-----交叉联接
--select * from zcp_formula cross join zcp_formula_category

----等价于
--select * from zcp_formula,zcp_formula_category

----加上=符号等同于内连接
--select * from zcp_formula,zcp_formula_category
--where zcp_formula.category_id = zcp_formula_category.id

--create table #employees
--(
--	empid int not null,
--	empname char(10) not null
--);

--create table #orders
--(
--	empid int not null,
--	seasonNbr char(10) not null,
--	sales money default 0.00 not null,
--)

--insert into #employees values
--(1,'grace'),
--(2,'ken'),
--(3,'tom')

--insert into #orders values
--(1,'season 1',100),
--(1,'season 2',100),
--(2,'season 3',120),
--(2,'season 4',130)

----获取每个雇员，每个季度的销售额
--select t1.empname,t2.seasonNbr,t2.sales from #employees as t1
--left join #orders as t2 on t1.empid = t2.empid

----这样查询是得不到想要的结果的，需要建立第三张辅助表(season)
--create table #season
--(
--	seasonNbr char(10) not null,
--);

--insert into #season values
--('season 1'),
--('season 2'),
--('season 3'),
--('season 4')

----用交叉联接得到
--select t1.empname,t2.seasonNbr,
--case 
--	when t3.sales is null then 0
--	else t3.sales
--end as sales
--from #employees as t1
--cross join #season as t2 
--left join #orders as t3 on t1.empid = t3.empid and t2.seasonNbr = t3.seasonNbr

----优化查询性能
----得到每季度销售的占比和与平均销售额的差值
--select seasonNbr,sales,
--CAST(sales/(select sum(sales) from #orders) * 100 as decimal(5,2)) as per,
--(sales - (select avg(sales) from #orders)) as 'avg'
--from #orders

----等价于，但是下面这个少了一次表扫描，提高了查询效率
--select t1.seasonNbr,t1.sales,t1.sales/t2.sumsales * 100 as per,t1.sales - t2.avgsales as 'avg' 
--from #orders as t1
--cross join (select sum(sales) as sumsales,avg(sales) as avgsales from #orders) as t2


----为cross join加上where相当于内连接
--select * from #employees as t1
--cross join #orders as t2
--where t1.empid = t2.empid
----等价于
--select * from #employees as t1
--inner join #orders as t2
--on t1.empid = t2.empid


----内部连接(inner join)
--create table #softEmployees
--(
--	EmpName char(10) not null,
--	SkillName char(20) not null,
--)

--create table #softSkill
--(
--	SkillName char(20) not null
--)

--insert into #softSkill values
--('SQLServer'),('C#'),('XML')

--insert into #softEmployees values
--('Jones','SQLServer'),
--('Jones','C#'),
--('Jones','XML'),
--('Grace','VB'),
--('Grace','C#'),
--('Eddie','VB'),
--('Eddie','J#'),
--('Celko','SQLServer'),
--('Celko','C#'),
--('Celko','XML'),
--('Celko','J#')

----得到同时会#softSkill的所有技能的员工姓名
--select t2.EmpName from #softSkill as t1
--inner join #softEmployees as t2 on t1.SkillName = t2.SkillName
--group by t2.EmpName
--having count(t2.skillName) > 2

----不等值连接，用于筛选
--select t2.EmpName from #softSkill as t1
--inner join #softEmployees as t2 on t1.SkillName = t2.SkillName and t2.EmpName <> 'Jones'
--group by t2.EmpName
--having count(t2.skillName) > 2


--create table #employees
--(
--	empid int not null,
--	empname char(10) not null
--);

--create table #orders
--(
--	empid int not null,
--	seasonNbr char(10) not null,
--	sales money default 0.00 not null,
--)

--insert into #employees values
--(1,'grace'),
--(2,'ken'),
--(3,'tom')

--insert into #orders values
--(1,'season 1',100),
--(1,'season 2',100),
--(1,'season 3',120),
--(1,'season 4',130),
--(2,'season 1',200),
--(2,'season 2',300),
--(2,'season 3',150)

----左连接查询
--select * from #employees as t1
--left join #orders as t2 on t1.empid = t2.empid

----行转列
--select t1.empname,s1.sales as 'season 1',s2.sales as 'season 2',s3.sales as 'season 3',s4.sales as 'season 4'
--from #employees as t1
--left join #orders as s1 on t1.empid = s1.empid and s1.seasonNbr = 'season 1'
--left join #orders as s2 on t1.empid = s2.empid and s2.seasonNbr = 'season 2'
--left join #orders as s3 on t1.empid = s3.empid and s3.seasonNbr = 'season 3'
--left join #orders as s4 on t1.empid = s4.empid and s4.seasonNbr = 'season 4'
----上诉方法进行了4次连接操作，执行效率似乎有一点问题，
----可以使用case语句优化，只进行一次连接操作
--select e1.empid,e1.empname,
--max(case when o1.seasonNbr = 'season 1' then o1.sales else 0 end),
--max(case when o1.seasonNbr = 'season 2' then o1.sales else 0 end),
--max(case when o1.seasonNbr = 'season 3' then o1.sales else 0 end),
--max(case when o1.seasonNbr = 'season 4' then o1.sales else 0 end)
--from #employees as e1
--left join #orders as o1
--on e1.empid = o1.empid
--group by e1.empid,e1.empname

----右连接 right join
----外连接 outer join，最直接的作用是，通过where语句返回两表之间没有匹配的行	
--select * from #employees as t1
--full outer join #orders as t2 on t1.empid = t2.empid
--where t1.empname is null or t2.sales is null


-----7.5自连接
-----7.5.1使用不同列的自连接
--drop table #employees
--create table #employees
--(
--	empid int not null,
--	empname varchar(50) not null,
--	mgrid int,
--)

--insert into #employees values
--(1,'Nancy',2),
--(2,'Andrew',NULL),
--(3,'Janet',2),
--(4,'Margaret',2),
--(5,'Steven',2),
--(6,'Michael',5),
--(7,'Robert',5),
--(8,'Laura',2),
--(9,'Anne',5)

----查出所有员工的上级领导
--select t1.empid,t1.empname,t1.mgrid,t2.empname as mgrname from #employees as t1
--left join #employees as t2 on t1.mgrid = t2.empid

--select * from users
--left join zcp_users on users.id = zcp_users.user_id
--where users.user_name like '%11%'


------第八章 操作结果集
--create table #table1
--(
--	a int,
--	b char(4),
--	c char(4),
--)

--create table #table2
--(
--	a char(4),
--	b decimal(5,4),
--)

--insert into #table1 values
--(1,'abc','jkl'),
--(2,'def','mno'),
--(3,'ghi','pqr')

--insert into #table2 values
--('jkl',1.000),
--('def',2.000),
--('mno',5.000)

--select a as 'count',b from #table1
--union 
--select CAST(b as int),a from #table2
--order by a desc


--交(intersect)并(union union all)差(except) 运算符

--create table #tableA(col int)
--create table #tableB(col int)
--create table #tableC(col int)

--insert into #tableA values(null),(null),(null),(1),(2),(2),(2),(3),(4),(4)
--insert into #tableB values(null),(1),(3),(4),(4),(5)
--insert into #tableC values(null),(2),(2),(4),(4)

----union 连接两张表，但是会去重
--select * from #tableA union select * from #tableB
----union 连接两张表，但是不会去重
--select * from #tableA union all select * from #tableB

----except找出tableA表中有的，但是tableB表中没有的数据
--select * from #tableA except 
--select * from #tableB

----except找出tableA表中有的，但是tableC表中没有的数据
--select * from #tableA except 
--select * from #tableC

----intersect找出tableA中有，且tableB中也有的
--select * from #tableA intersect select * from #tableB


--插入操作(insert)
create table #A
(
	id int,
	user_name varchar(50),
)

--使用insert 和 select子查询插入行
insert into #A
select id,user_name from users

select * from #A

drop proc GetTop
create proc GetTop
	@n as int,
	@rcc as int output
as
select top(@n) id,user_name
from users
set @rcc = @@ROWCOUNT

declare @rcc as int
exec GetTop @n = 500,@rcc = @rcc output
select @rcc

create table #B
(
	id int,
	user_name varchar(50),
)

--使用insert 和 exec插入行
declare @rcc2 as int
insert into #B
exec GetTop @n = 100,@rcc = @rcc2 output

select @rcc2
select * from #B


--使用select into 插入行
select t1.id,t1.user_name,ISNULL(t2.gold,0) as gold
into #C
from users as t1 left join zcp_users as t2
on t1.id = t2.user_id

select * from #C

--只插入表结构，添加数据
select t1.id,t1.user_name,ISNULL(t2.gold,0) as gold
into #D
from users as t1 left join zcp_users as t2
on t1.id = t2.user_id
where 1=2

select * from #D

--Update 更新操作
--1.使用set和where子句更新数据

--2.使用from子句更新数据
update users
set exp = t2.gold
select * from zcp_users as t1
inner join (
select max(zcp_users.gold) as gold,user_id 
from zcp_users
group by user_id) as t2
on t1.user_id = t2.user_id

select * from users


--使用OUTPUT输出受影响的信息
--1.INSERT和OUTPUT
create table #student
(
	id int identity(1,1),
	name varchar(50),
)

insert into #student output inserted.id,inserted.name
values('xc')
select * from #student

--也可以将返回结果输出到一张表中
--声明表变量
declare @myTable table
(
	id int,
	name varchar(50)
);

insert into #student output inserted.* into @myTable
values('xc')
select * from @myTable	

--2.DELETE与OUTPUT
delete from #student
output deleted.*
where id in (select min(id) from #student)

--3.UPDATE与OUTPUT
--UPDATE实际上是通过删除旧行和插入新行来完成更新操作的。
update #student
set name = 'fy'
output deleted.* ,inserted.*
where id = 9


---------------第11章 视图-------------------

---------------第12章 游标-------------------

--12.1 游标主要应用于存储过程和触发器
--步骤1 声明变量，用于包含游标的变量
--步骤2 使用Declare Cursor语句将游标与Select语句相关联，另外它还定义了游标名称和只读还是只进
--步骤3 使用Open语句，执行Select语句并填充游标
--步骤4 使用Fetch into语句提取单个行，并将每列数据移至指定变量中。然后其他变量可以引用变量来访问提取的数据值
--步骤5 使用Close语句结束游标的使用。关闭游标可以释放某些资源。Deallocate语句可以完全释放分配给游标的资源。

alter procedure GetPosts
as 
declare @id int;
declare @title varchar(50);
declare @add_time datetime;

--声明游标
declare myFirstCursor cursor local for
select id,title,add_time 
from zcp_post
order by id desc
for read only

--打开游标
open myFirstCursor;
while 0 = 0
begin
	fetch next --读取行
		from myFirstCursor
		into @id,@title,@add_time
	if @@FETCH_STATUS <> 0	--非0表示执行失败
	begin
		break;--跳出循环
	end
	print cast(@id as varchar(10)) + '-' + @title + '-' + cast(@add_time as varchar(20))
end

close myFirstCursor		--关闭游标
deallocate myFirstCursor	--释放游标资源
go

exec GetPosts

--建议在声明游标的局部游标(local)，这样名字不会冲突

-- 12.2快速只进游标和可滚动游标
-- 默认情况下，创建的游标是只进游标，不支持滚动，只能按照从头到尾的顺序提取行。
-- 对于Fetch语句来说，也就是只能进行Fetch Next，而不能向前提取。
-- 为了提高只进游标的性能，可以在Cursor语句之后添加Fast_Forward关键字，在打开游标时，查询优化器可以对游标中的Select语句进行优化
-- 但是如果指定了Scroll(创建滚动游标)或For Update(可更新游标)，则不能指定Fast_Forward

--例子
select * from zcp_post
order by id desc
go

--声明存储过程
alter procedure p_post
as 
--声明游标
declare c_post scroll cursor for
	select * from zcp_post

--打开游标
open c_post;
--读取最后一行
fetch last from c_post

--读取当前行的前一行
fetch prior from c_post

--读取游标中的第二行
fetch absolute 2 from c_post

--读取当前行的第三行
fetch relative 3 from c_post

--读取当前行的第三行
fetch relative -2 from c_post

--关闭游标
close c_post
deallocate c_post
go

exec p_post
go
---------------第13章 存储过程---------------

--创建存储过程
alter procedure myProcedure
as
	select * from users
go

--修改存储过程
alter proc myProcedure
as 
	select top 1 * from users
--调用存储过程
execute myProcedure
go

--try和catch块
--创建存储过程检索错误信息
alter proc usp_GetErrorInfo
as
select ERROR_NUMBER() as ERROR_NUMBER,
	   ERROR_SEVERITY() as ERROR_SEVERITY,
	   ERROR_STATE() as ERROR_STATE,
	   ERROR_PROCEDURE() as ERROR_PROCEDURE,
	   ERROR_LINE() as ERROR_LINE,
	   ERROR_MESSAGE() as ERROR_MESSAGE;


begin try
select 1/0
end try

begin catch
exec usp_GeterrorInfo
end catch;

--嵌套存储过程
create proc usp_InnerProc as
	select @@NESTLEVEL as NESTLEVEL
create proc usp_OuterProc as 
	select @@NESTLEVEL as NESTLEVEL
	exec usp_InnerProc

exec usp_OuterProc

--下面展示了select,exec,exec sp_executesql三种的嵌套层级
select @@NESTLEVEL as NESTLEVEL
exec('select @@NESTLEVEL as NESTLEVEL')
exec sp_executesql  N'select @@NESTLEVEL as NESTLEVEL'



---------------第14章 触发器---------------

--14.1 DML触发器
--触发器分为After触发器，Instead Of触发器和CLR触发器
--14.1.1 AFTER触发器

--例子
--创建主表
create table PriTable
(OrderID int identity(1,1),OrderTotal money)
--创建详情表
create table DetailTable
(OrderID int,ProductID int,ProductCount int not null,Price money);
go

--向主表中插入数据 
insert into PriTable values(2100)
insert into PriTable values(1000)
--向明细表中插入订单的产品信息
insert into DetailTable values(1,1,10,110)
insert into DetailTable values(1,2,10,100)
insert into DetailTable values(2,2,10,100)
go

--为PriTable表添加触发器
create trigger PriTrigger
on PriTable
for Delete
as
delete from DetailTable
where OrderID in(select OrderID from Deleted);
Print N'已经删除了DetailTable表中的相关数据'

select * from PriTable
select * from DetailTable

--接下来让我们删除PriTable表的第一行
Delete from PriTable 
where OrderID = 2

--14.1.2 进行事务提交和回滚操作
--在自动事务处理模式下，当语句遇到错误时，会有隐含的BeginTransaction语句来回滚该语句所影响的修改。
--但是，该回滚对批处理中的其他语句没有影响，不会回滚前面操作正常的语句。
--因为语句完成时，该事务要么提交要么回滚，事务处理已经结束。
--需要注意的是，这个回滚操作也会终止批处理中对该语句的后面语句的执行。

--例子

--创建表
create table DetailTable
(OrderID int,ProductID int,ProductCount int not null,Price money);
create table DetailTable1
(OrderID int,ProductID int,ProductCount int not null,Price money);
go
--创建触发器，将插入表DetailTable的数据复制到DetailTable1中
alter trigger DetailTrigger
on DetailTable
after Insert
as 
	begin transaction
	insert into DetailTable1 
		select * from inserted;
	
	if(@@ERROR <> 0 or Exists(select * from inserted where ProductID = 0 or ProductCount = 0 or Price = 0))
	begin
		rollback transaction
	end
	else 
	begin
		commit transaction
	end
	--rollback transaction

insert into DetailTable values(2,1,1,88)
go

select * from DetailTable
select * from DetailTable1