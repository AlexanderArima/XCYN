--˳��+�ж����
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

--˳��+�ж�+ѭ��
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

--�ж�+ѭ��
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
		goto table_loop --��ת����ǩ��

select @i,@myVal1,@myVal2
*/

--�ȴ�ִ��
/*
waitfor delay '00:00:03'--�ȴ�3s
print('3')

waitfor time '09:50:30'--�ȴ�ʱ���ߵ�09��50��30
print('09:50:30')
*/

--������ִ��go
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

--2,3��������뵽����
select * from #a
*/

--�������������ȼ���int��char֮ǰ
--select 1.5 + '1'

--������������
/*
declare @i as int
set @i = 10
select @i
go
--i��������������������֮�ڿɼ�
select @i
*/

--Ϊ������ֵ
/*
declare @i as int
set @i = 100

--���б������õ��б���
select @i = avg(id) from zcp_post

select @i

--���б������ö��б�������ȡ��������е����һ���еı��ʽ��Ϊ����ֵ
select @i = id from zcp_post

select @i
*/

--��ֵ�����
--ʹ�ø�ֵ������������б������ֵ
--select t_id = 0,s_id = id,* from zcp_post

--�߼������
/*
--any
select * from zcp_post
where id > any(select post_id from zcp_post_collect)

--between
select * from zcp_post
where 1 between 1 and 5

--exists ����Ӳ�ѯ�а���һЩ�У��򷵻�true
select * from zcp_post
where exists(select * from zcp_post_collect where id = 1)

--in����������е��ڱ��ʽ�е�һ��������true
select * from zcp_post where id in(1,2,3,4)

--like�����������һ��ģʽƥ�䣬����true
-- '_'��ʾһ���ַ���'%'��ʾһ���ַ���
select * from zcp_post where title like '_111'
*/
--notȡ����And��Or

/*
--�ۺϺ���
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
--Ĭ����All�������е�ֵ���оۺϣ�Distinctֻ��Ψһ��ֵ���оۺ�
select avg(num) from #a
select avg(distinct num) from #a
*/

--���ú���

--select @@MAX_CONNECTIONS as ���������,@@VERSION as �汾��Ϣ,@@DATEFIRST as ÿ�ܵ�һ��,@@LANGUAGE as ����,@@SERVERNAME as ���ط���������,@@SERVICENAME as ����ʵ��,@@SPID as �ỰID

--�α꺯��
--select @@CURSOR_ROWS as �α�����������,@@FETCH_STATUS as Fetch����״̬

--���ں�ʱ�亯��
/*
declare @time_now as datetime
set @time_now = '2017-07-04 00:00:00'
--DATEADD����������ʱ��
select DATEADD(YYYY,1,@time_now) as ��,DATEADD(QQ,1,@time_now) as ����,DATEADD(MM,1,@time_now) as ��,
DATEADD(WEEK,1,@time_now) as ��,DATEADD(DAY,1,@time_now) as ��,DATEADD(HOUR,1,@time_now) as Сʱ,
DATEADD(MINUTE,1,@time_now) as ����,DATEADD(SECOND,1,@time_now) as ��,DATEADD(MS,1,@time_now) as ����
go
*/

--DATEDIFF����,����ʱ���ֵ

--declare @start_time as datetime,@end_time as datetime
--set @start_time = '2017-07-04 12:00:00'
--set @end_time = '2017-07-04 20:45:00'
--select DATEDIFF(SECOND,@start_time,@end_time)

--select DATEPART(DAY,@start_time) as һ���µ�����,DATEPART(DAYOFYEAR,@start_time) as һ�������,
--	   DATEPART(WEEKDAY,@start_time) as һ�ܵ�����,DATEPART(WEEK,@start_time) as һ�������,
--	   DATEPART(hour,@start_time) as Сʱ

--DATENAME���������������е�ָ������
--select DATENAME(MONTH,@start_time)

--����ʱ������
--select DATEFROMPARTS(2017,7,4)
--select DATETIMEFROMPARTS(2017,7,4,12,00,00,00)
--select SMALLDATETIMEFROMPARTS(2017,7,4,13,0)
--select TIMEFROMPARTS(12,01,50,0,0)

----�õ���ǰ��ʱ��
--select GETDATE()
--select GETUTCDATE()

----����������
--select YEAR(GETDATE())
--select DAY(GETDATE())
--select MONTH(GETDATE())


--��ѧ����

--ȡ��0-1�������
--select rand()

----��������
--select round(rand(),2) * 100

----ƽ����
--select SQRT(2)

----ƽ��
--select SQUARE(2)

----��Ȼ����
--select LOG(2)

--��������ת��
--CAST(expression)
--select CAST(1111111111 as char(10)) + 'a'

--CONVERT(data_type,expression)
--select CONVERT(char(10),1111111111) + 'a'

--ʱ��ת��Ϊ�ַ���
--select CONVERT(varchar(30),GETDATE(),121)

--�ַ���ת������
--select CAST('12.5' as float) + 10


--�ַ�������

--ASCII �����ַ���ASCIIֵ
--select ASCII('a') --97
--select ASCII('A') --65
--select ASCII('1') --49

----CHAR(integer_expression) ������ת��ASCII�ַ� integer_expression��Χ��0-255֮��
--select CHAR(97) --a
--select CHAR(65) --A
--select CHAR(49) --1

--select CHAR(0) --�ո�
--select CHAR(254) --NULL

--CHARINDEX(expression1,expression2,[start_location])
--�����ַ�����ָ�����ʽ�Ŀ�ʼλ��
--select * from zcp_post
--where CHARINDEX('���Ϸ���',title) > 0

--select * from zcp_post
--where title like '%���Ϸ���%'

--LEFT(char_expression,int_expression)
--�����ַ�������߿�ʼ�ö��������ַ�

--LEN(string_expression) �ַ�������
--select LEFT(jzrq,4) as ���,LEN(jzrq) as ���� from zcp_lottery_data

--LOWERתСд��UPPERת��д
--select LOWER(title) as Сд,UPPER(title) as ��д,* from zcp_post

--select * from zcp_post
--where id = 1

--update zcp_post
--set title = '   [���Ϸ���]116��:���ڲ�����һͷ���������룬��������׬��һ����!    '
--where id = 1

--LTRIM(expression)
--ɾ���ַ���������пո�
--select LTRIM(title),LEN(LTRIM(title)),title,LEN(title),RTRIM(title),LEN(RTRIM(title)) from zcp_post

--PATINDEX('%patten%,expression)
--���ط���ģʽ���ַ������±�
--select PATINDEX('%a%','i am jack')

--REPLACE(expression1,expression2,expression3)
--��expression1�е�����expression2�滻Ϊexpression3
--select REPLACE('abcdefghijklmnopqrstuvwxyz','a','1')

--REPLICATE(char_expression,int_expression)
--�������char_expression�ظ�int_expression��
--select REPLICATE('abc',3)

--REVERSE��������ַ���
--select REVERSE('abcdefg')

--RIGTH�����ַ������ҿ�ʼ����������ַ�
--select * from zcp_lottery_data
--where RIGHT(jzrq,2) = '31'
--order by LEFT(jzrq,4)

--RTRIMɾ���ַ��Ҳ�Ŀո�

--SUBSTRING(char_expression,start,length)
--��start��ʼ��ȡlength���ַ�����char_expression��
--select SUBSTRING('abcdefg',1,2)

--SELECT * 
--FROM zcp_post
--WHERE LEFT(category_id,2) = 61

--�ж����ݿ��Ƿ����
--select DB_ID('whnk1')

--OBJECT_ID��ѯ����ͼ���洢���̣������Ƿ����
--select OBJECT_ID('ball_sort')

--��ȡΨһ��ʶ��
--select NEWID()

--DROP TABLE #c
--CREATE TABLE #c
--(
--	id int IDENTITY,
--	p_id int,
--	price money,
--	name varchar(20) 
--)

----�������Լ��
--ALTER TABLE #c ADD CONSTRAINT PK_C_id PRIMARY KEY(id)

----���ΨһԼ��
--ALTER TABLE #c ADD CONSTRAINT UNI_C_P_ID UNIQUE(p_id)

----���CHECKԼ��
--ALTER TABLE #c ADD CONSTRAINT CHK_C_price CHECK(price >= 0)

----���ӱ����
--ALTER TABLE #c ADD email VARCHAR(100)

----SP_RENAME [@objname = ] 'object_name',[@newname = ] 'new_name' ,[@objtype = ] 'object_type'
----'object_name'��ʽΪtable.column��new_nameָ������������ƣ�object_typeҪ�������Ķ��������
----�޸�����
--EXEC SP_RENAME 'link.titles','title','column';

----�޸��е���������
--ALTER TABLE link ALTER COLUMN title nvarchar(255)

--INSERT INTO #c(p_id,price,name) VALUES (8,0,'A','CC@QQ.COM'),(6,2,'B','DD@QQ.COM')
--SELECT @@IDENTITY

--SELECT * FROM #c

----ɾ�����Լ������
--ALTER TABLE #c
--DROP CONSTRAINT UNI_C_P_ID

--ALTER TABLE #c
--DROP COLUMN p_id

----�г����е�Լ�� sp_help
--exec sp_help zcp_post


----�����
----����һ�������
--DECLARE @tableVar TABLE (id int)
----���Ʊ�
--insert into @tableVar(id) 
--select id from users

--select * from @tableVar

----��ʱ��
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

--������ ��ѯ����
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

----CASE ���
--SELECT 
--case when point > 1000 then '�߼��û�'
--     when point > 500 and point <= 1000 then '�м��û�'
--	 when point > 100 and point <= 500 then '�����û�'
--	 else '�ο�'
--	 end as N'�û��ȼ�'
--	 ,point as N'����'
--	 ,user_name
--	 ,user_name + ':' + (CAST(point as varchar(20))) as �û�����
--	 ,$IDENTITY   --����ؼ���$IDENTITY����ؼ��־���IDENTITY��������
--	 ,DATEDIFF(ww,reg_time,GETDATE()) as 'sum'
--FROM users
--ORDER BY POINT DESC


--WHERE���ɸѡ

----BETWEEN�ؼ��ּ�������ָ��ֵ֮�������ֵ
--select * from users
--where point between 10 and 100

----����֮��ȼ�--
--select * from users
--where point >= 10 and point < 100

----NOT BETWEEN ����ָ����Χ֮���������
--SELECT * FROM users
--WHERE point NOT BETWEEN 10 AND 100

----�ȼ���--
--SELECT * FROM users
--WHERE point < 10 OR point > 100

-- NULL��NOT NULL --
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

----����IS NULL������Ϊ�յ�Ҳ����ȥ
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

----��Ҫע�����ʹ�þۺϺ�����NULL���м���ʱ��NULL�����ڼ�����
--SELECT ColA,count(ColB),SUM(ColC)
--from #C
--Group by ColA
--having ColA is null

--SELECT count(ColB),SUM(ColC)
--from #C

----ʹ��HAVING�Ӿ��п��԰����ۺϺ���
--SELECT point,count(point) as 'count'
--FROM users
--GROUP BY point
--HAVING count(point) > 10


----ORDER BY�Ӿ��в��ܰ����ۺϺ��������ǿ��԰����ۺϺ����ı���
--SELECT point,count(point) as 'count'
--FROM users
--GROUP BY point
--ORDER BY COUNT 

--SELECT TOP 100 PERCENT * FROM USERS


-------------------------������ �Ӳ�ѯ-----------------------
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

----��ѯÿ���������ܼ�
--select orderID,orderDate,(select sum(lineTotal) from #orderDetail where #orderDetail.orderID = #order.orderID) as orderTotal
--from #order

----�ȼ���
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

----in�ؼ���(����in�ؼ��ֵĿ����Ƕ����Ӳ�ѯҲ����������Ӳ�ѯ)
--select * from #orderHeader
--where custID in(select #customer.custID from #customer where city = 'beijing')

----exists�ؼ���(����exists��������Ӳ�ѯ)
--select * from #orderHeader
--where exists
--(
--select * 
--from #customer 
--where #customer.custID = #orderHeader.custID and #customer.city = 'beijing'
--)

----����not in��not exists���Ӳ�ѯ
--select * from #customer
--where not exists(
--select * from #orderHeader 
--where custID = #customer.custID
--)

----�ȼ���
--select * from #customer
--where custID not in(
--select custID from #orderHeader
--where custID is not null
--)

----��ֵ�߼� null���καȽ����������ʱʼ�շ���Unknown��������is nullν�ʼ���ʱ�ŷ���true


----�γ̱�
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

----�ҳ��γ�1�Ѿ����꣬���������γ�û�������ѧ��������ѡ�޵Ŀγ̵���3

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

----�ȼ���

--select studentName
--from #lessons as t1
--where lessonNbr = 1 and lessonStat = 'DONE' and 'WAIT' = ALL(
--select lessonStat from #lessons as t2
--where lessonNbr <> 1 and t1.studentName = t2.studentName
--) and 3 = (
--select count(t3.studentName) from #lessons as t3
--where t1.studentName = t3.studentName
--)

----�ȼ���
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

----�ĵ�:�Ӳ�ѯһ��Ҫע��ȥ���ѿ�������Ҳ����˵������Ҫ�й���!

--ͨ���Ƚ������������Ӳ�ѯѡ���б�ֻ�ܰ���һ�����ʽ��������
--�ⲿ��ѯ��where�Ӿ���б������Ӳ�ѯ�б��е��о����������͵ļ�����

--declare @t table(id int,name varchar(50))
--insert into @t values(10,'a')
--insert into @t values(11,'b')
--insert into @t values(12,'c')
--insert into @t values(4,'d')

--select * from zcp_formula
--where category_id = any(select top 99999 id from @t order by name)

----Order By��䲻�������Ӳ�ѯ������ָ����top�ؼ��ֺ󣬾Ϳ���ʹ����

--select * from zcp_formula as t1
--where category_id in (select t2.id from zcp_formula_category as t2 where state = 1)



-----------------------------������ ���Ӳ�ѯ---------------------------

--select * from zcp_formula as t1
--left join zcp_formula_category as t2 
--on CAST(t1.category_id as varchar(50)) = CAST(t2.id as varchar(50))


--select * from zcp_formula as t1
--join zcp_formula_category as t2 
--on Convert(varchar(50),t1.category_id) = Convert(varchar(50),t2.id)

-----��������
--select * from zcp_formula cross join zcp_formula_category

----�ȼ���
--select * from zcp_formula,zcp_formula_category

----����=���ŵ�ͬ��������
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

----��ȡÿ����Ա��ÿ�����ȵ����۶�
--select t1.empname,t2.seasonNbr,t2.sales from #employees as t1
--left join #orders as t2 on t1.empid = t2.empid

----������ѯ�ǵò�����Ҫ�Ľ���ģ���Ҫ���������Ÿ�����(season)
--create table #season
--(
--	seasonNbr char(10) not null,
--);

--insert into #season values
--('season 1'),
--('season 2'),
--('season 3'),
--('season 4')

----�ý������ӵõ�
--select t1.empname,t2.seasonNbr,
--case 
--	when t3.sales is null then 0
--	else t3.sales
--end as sales
--from #employees as t1
--cross join #season as t2 
--left join #orders as t3 on t1.empid = t3.empid and t2.seasonNbr = t3.seasonNbr

----�Ż���ѯ����
----�õ�ÿ�������۵�ռ�Ⱥ���ƽ�����۶�Ĳ�ֵ
--select seasonNbr,sales,
--CAST(sales/(select sum(sales) from #orders) * 100 as decimal(5,2)) as per,
--(sales - (select avg(sales) from #orders)) as 'avg'
--from #orders

----�ȼ��ڣ����������������һ�α�ɨ�裬����˲�ѯЧ��
--select t1.seasonNbr,t1.sales,t1.sales/t2.sumsales * 100 as per,t1.sales - t2.avgsales as 'avg' 
--from #orders as t1
--cross join (select sum(sales) as sumsales,avg(sales) as avgsales from #orders) as t2


----Ϊcross join����where�൱��������
--select * from #employees as t1
--cross join #orders as t2
--where t1.empid = t2.empid
----�ȼ���
--select * from #employees as t1
--inner join #orders as t2
--on t1.empid = t2.empid


----�ڲ�����(inner join)
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

----�õ�ͬʱ��#softSkill�����м��ܵ�Ա������
--select t2.EmpName from #softSkill as t1
--inner join #softEmployees as t2 on t1.SkillName = t2.SkillName
--group by t2.EmpName
--having count(t2.skillName) > 2

----����ֵ���ӣ�����ɸѡ
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

----�����Ӳ�ѯ
--select * from #employees as t1
--left join #orders as t2 on t1.empid = t2.empid

----��ת��
--select t1.empname,s1.sales as 'season 1',s2.sales as 'season 2',s3.sales as 'season 3',s4.sales as 'season 4'
--from #employees as t1
--left join #orders as s1 on t1.empid = s1.empid and s1.seasonNbr = 'season 1'
--left join #orders as s2 on t1.empid = s2.empid and s2.seasonNbr = 'season 2'
--left join #orders as s3 on t1.empid = s3.empid and s3.seasonNbr = 'season 3'
--left join #orders as s4 on t1.empid = s4.empid and s4.seasonNbr = 'season 4'
----���߷���������4�����Ӳ�����ִ��Ч���ƺ���һ�����⣬
----����ʹ��case����Ż���ֻ����һ�����Ӳ���
--select e1.empid,e1.empname,
--max(case when o1.seasonNbr = 'season 1' then o1.sales else 0 end),
--max(case when o1.seasonNbr = 'season 2' then o1.sales else 0 end),
--max(case when o1.seasonNbr = 'season 3' then o1.sales else 0 end),
--max(case when o1.seasonNbr = 'season 4' then o1.sales else 0 end)
--from #employees as e1
--left join #orders as o1
--on e1.empid = o1.empid
--group by e1.empid,e1.empname

----������ right join
----������ outer join����ֱ�ӵ������ǣ�ͨ��where��䷵������֮��û��ƥ�����	
--select * from #employees as t1
--full outer join #orders as t2 on t1.empid = t2.empid
--where t1.empname is null or t2.sales is null


-----7.5������
-----7.5.1ʹ�ò�ͬ�е�������
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

----�������Ա�����ϼ��쵼
--select t1.empid,t1.empname,t1.mgrid,t2.empname as mgrname from #employees as t1
--left join #employees as t2 on t1.mgrid = t2.empid

--select * from users
--left join zcp_users on users.id = zcp_users.user_id
--where users.user_name like '%11%'


------�ڰ��� ���������
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


--��(intersect)��(union union all)��(except) �����

--create table #tableA(col int)
--create table #tableB(col int)
--create table #tableC(col int)

--insert into #tableA values(null),(null),(null),(1),(2),(2),(2),(3),(4),(4)
--insert into #tableB values(null),(1),(3),(4),(4),(5)
--insert into #tableC values(null),(2),(2),(4),(4)

----union �������ű����ǻ�ȥ��
--select * from #tableA union select * from #tableB
----union �������ű����ǲ���ȥ��
--select * from #tableA union all select * from #tableB

----except�ҳ�tableA�����еģ�����tableB����û�е�����
--select * from #tableA except 
--select * from #tableB

----except�ҳ�tableA�����еģ�����tableC����û�е�����
--select * from #tableA except 
--select * from #tableC

----intersect�ҳ�tableA���У���tableB��Ҳ�е�
--select * from #tableA intersect select * from #tableB


--�������(insert)
create table #A
(
	id int,
	user_name varchar(50),
)

--ʹ��insert �� select�Ӳ�ѯ������
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

--ʹ��insert �� exec������
declare @rcc2 as int
insert into #B
exec GetTop @n = 100,@rcc = @rcc2 output

select @rcc2
select * from #B


--ʹ��select into ������
select t1.id,t1.user_name,ISNULL(t2.gold,0) as gold
into #C
from users as t1 left join zcp_users as t2
on t1.id = t2.user_id

select * from #C

--ֻ�����ṹ���������
select t1.id,t1.user_name,ISNULL(t2.gold,0) as gold
into #D
from users as t1 left join zcp_users as t2
on t1.id = t2.user_id
where 1=2

select * from #D

--Update ���²���
--1.ʹ��set��where�Ӿ��������

--2.ʹ��from�Ӿ��������
update users
set exp = t2.gold
select * from zcp_users as t1
inner join (
select max(zcp_users.gold) as gold,user_id 
from zcp_users
group by user_id) as t2
on t1.user_id = t2.user_id

select * from users


--ʹ��OUTPUT�����Ӱ�����Ϣ
--1.INSERT��OUTPUT
create table #student
(
	id int identity(1,1),
	name varchar(50),
)

insert into #student output inserted.id,inserted.name
values('xc')
select * from #student

--Ҳ���Խ����ؽ�������һ�ű���
--���������
declare @myTable table
(
	id int,
	name varchar(50)
);

insert into #student output inserted.* into @myTable
values('xc')
select * from @myTable	

--2.DELETE��OUTPUT
delete from #student
output deleted.*
where id in (select min(id) from #student)

--3.UPDATE��OUTPUT
--UPDATEʵ������ͨ��ɾ�����кͲ�����������ɸ��²����ġ�
update #student
set name = 'fy'
output deleted.* ,inserted.*
where id = 9


---------------��11�� ��ͼ-------------------

---------------��12�� �α�-------------------

--12.1 �α���ҪӦ���ڴ洢���̺ʹ�����
--����1 �������������ڰ����α�ı���
--����2 ʹ��Declare Cursor��佫�α���Select�������������������������α����ƺ�ֻ������ֻ��
--����3 ʹ��Open��䣬ִ��Select��䲢����α�
--����4 ʹ��Fetch into�����ȡ�����У�����ÿ����������ָ�������С�Ȼ�����������������ñ�����������ȡ������ֵ
--����5 ʹ��Close�������α��ʹ�á��ر��α�����ͷ�ĳЩ��Դ��Deallocate��������ȫ�ͷŷ�����α����Դ��

alter procedure GetPosts
as 
declare @id int;
declare @title varchar(50);
declare @add_time datetime;

--�����α�
declare myFirstCursor cursor local for
select id,title,add_time 
from zcp_post
order by id desc
for read only

--���α�
open myFirstCursor;
while 0 = 0
begin
	fetch next --��ȡ��
		from myFirstCursor
		into @id,@title,@add_time
	if @@FETCH_STATUS <> 0	--��0��ʾִ��ʧ��
	begin
		break;--����ѭ��
	end
	print cast(@id as varchar(10)) + '-' + @title + '-' + cast(@add_time as varchar(20))
end

close myFirstCursor		--�ر��α�
deallocate myFirstCursor	--�ͷ��α���Դ
go

exec GetPosts

--�����������α�ľֲ��α�(local)���������ֲ����ͻ

-- 12.2����ֻ���α�Ϳɹ����α�
-- Ĭ������£��������α���ֻ���α꣬��֧�ֹ�����ֻ�ܰ��մ�ͷ��β��˳����ȡ�С�
-- ����Fetch�����˵��Ҳ����ֻ�ܽ���Fetch Next����������ǰ��ȡ��
-- Ϊ�����ֻ���α�����ܣ�������Cursor���֮�����Fast_Forward�ؼ��֣��ڴ��α�ʱ����ѯ�Ż������Զ��α��е�Select�������Ż�
-- �������ָ����Scroll(���������α�)��For Update(�ɸ����α�)������ָ��Fast_Forward

--����
select * from zcp_post
order by id desc
go

--�����洢����
alter procedure p_post
as 
--�����α�
declare c_post scroll cursor for
	select * from zcp_post

--���α�
open c_post;
--��ȡ���һ��
fetch last from c_post

--��ȡ��ǰ�е�ǰһ��
fetch prior from c_post

--��ȡ�α��еĵڶ���
fetch absolute 2 from c_post

--��ȡ��ǰ�еĵ�����
fetch relative 3 from c_post

--��ȡ��ǰ�еĵ�����
fetch relative -2 from c_post

--�ر��α�
close c_post
deallocate c_post
go

exec p_post
go
---------------��13�� �洢����---------------

--�����洢����
alter procedure myProcedure
as
	select * from users
go

--�޸Ĵ洢����
alter proc myProcedure
as 
	select top 1 * from users
--���ô洢����
execute myProcedure
go

--try��catch��
--�����洢���̼���������Ϣ
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

--Ƕ�״洢����
create proc usp_InnerProc as
	select @@NESTLEVEL as NESTLEVEL
create proc usp_OuterProc as 
	select @@NESTLEVEL as NESTLEVEL
	exec usp_InnerProc

exec usp_OuterProc

--����չʾ��select,exec,exec sp_executesql���ֵ�Ƕ�ײ㼶
select @@NESTLEVEL as NESTLEVEL
exec('select @@NESTLEVEL as NESTLEVEL')
exec sp_executesql  N'select @@NESTLEVEL as NESTLEVEL'



---------------��14�� ������---------------

--14.1 DML������
--��������ΪAfter��������Instead Of��������CLR������
--14.1.1 AFTER������

--����
--��������
create table PriTable
(OrderID int identity(1,1),OrderTotal money)
--���������
create table DetailTable
(OrderID int,ProductID int,ProductCount int not null,Price money);
go

--�������в������� 
insert into PriTable values(2100)
insert into PriTable values(1000)
--����ϸ���в��붩���Ĳ�Ʒ��Ϣ
insert into DetailTable values(1,1,10,110)
insert into DetailTable values(1,2,10,100)
insert into DetailTable values(2,2,10,100)
go

--ΪPriTable����Ӵ�����
create trigger PriTrigger
on PriTable
for Delete
as
delete from DetailTable
where OrderID in(select OrderID from Deleted);
Print N'�Ѿ�ɾ����DetailTable���е��������'

select * from PriTable
select * from DetailTable

--������������ɾ��PriTable��ĵ�һ��
Delete from PriTable 
where OrderID = 2

--14.1.2 ���������ύ�ͻع�����
--���Զ�������ģʽ�£��������������ʱ������������BeginTransaction������ع��������Ӱ����޸ġ�
--���ǣ��ûع����������е��������û��Ӱ�죬����ع�ǰ�������������䡣
--��Ϊ������ʱ��������Ҫô�ύҪô�ع����������Ѿ�������
--��Ҫע����ǣ�����ع�����Ҳ����ֹ�������жԸ����ĺ�������ִ�С�

--����

--������
create table DetailTable
(OrderID int,ProductID int,ProductCount int not null,Price money);
create table DetailTable1
(OrderID int,ProductID int,ProductCount int not null,Price money);
go
--�������������������DetailTable�����ݸ��Ƶ�DetailTable1��
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