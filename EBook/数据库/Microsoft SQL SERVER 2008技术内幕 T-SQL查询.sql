SELECT DISTINCT Country
FROM T_Customer
ORDER BY CustID

--��ѯ�Ż� 
--���Ȳ�ѯ��̬����ͼ��DMV��dynamic management view��
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

-- ���ҳ������������ĵȴ�
WITH Waits AS
(
  SELECT
    wait_type,
    wait_time_ms / 1000. AS wait_time_s,
    100. * wait_time_ms / SUM(wait_time_ms) OVER() AS pct,
    ROW_NUMBER() OVER(ORDER BY wait_time_ms DESC) AS rn,
    100. * signal_wait_time_ms / wait_time_ms as signal_pct
  FROM sys.dm_os_wait_stats
  WHERE wait_time_ms > 0
    AND wait_type NOT LIKE N'%SLEEP%'
    AND wait_type NOT LIKE N'%IDLE%'
    AND wait_type NOT LIKE N'%QUEUE%'    
    AND wait_type NOT IN(  N'CLR_AUTO_EVENT'
                         , N'REQUEST_FOR_DEADLOCK_SEARCH'
                         , N'SQLTRACE_BUFFER_FLUSH'
                         /* filter out additional irrelevant waits */ )
)
SELECT
  W1.wait_type, 
  CAST(W1.wait_time_s AS NUMERIC(12, 2)) AS wait_time_s,
  CAST(W1.pct AS NUMERIC(5, 2)) AS pct,
  CAST(SUM(W2.pct) AS NUMERIC(5, 2)) AS running_pct,
  CAST(W1.signal_pct AS NUMERIC(5, 2)) AS signal_pct
FROM Waits AS W1
  JOIN Waits AS W2
    ON W2.rn <= W1.rn
GROUP BY W1.rn, W1.wait_type, W1.wait_time_s, W1.pct, W1.signal_pct
HAVING SUM(W2.pct) - W1.pct < 80 -- percentage threshold
    OR W1.rn <= 5
ORDER BY W1.rn;
GO


-- ����̶���ʱ���ռ�����
INSERT INTO Performance.dbo.WaitStats
    (wait_type, waiting_tasks_count, wait_time_ms,
     max_wait_time_ms, signal_wait_time_ms)
  SELECT
    wait_type, waiting_tasks_count, wait_time_ms,
    max_wait_time_ms, signal_wait_time_ms
  FROM sys.dm_os_wait_stats
  WHERE wait_type NOT IN (N'MISCELLANEOUS');

  -- ����������ͼ
IF OBJECT_ID('dbo.IntervalWaitsSample', 'V') IS NOT NULL
  DROP VIEW dbo.IntervalWaitsSample;
GO

CREATE VIEW dbo.IntervalWaitsSample
AS

SELECT wait_type, start_time, interval_wait_s
FROM dbo.IntervalWaits('2018-08-22', '2018-08-30') AS F;
GO

---------------------------------------------------------------------
-- �ȴ�����
---------------------------------------------------------------------

SELECT
  object_name,
  counter_name,
  instance_name,
  cntr_value,
  cntr_type
FROM sys.dm_os_performance_counters;

-- �������ݿ�I/O
WITH DBIO AS
(
  SELECT
    DB_NAME(IVFS.database_id) AS db,
    MF.type_desc,
    SUM(IVFS.num_of_bytes_read + IVFS.num_of_bytes_written) AS io_bytes,
    SUM(IVFS.io_stall) AS io_stall_ms
  FROM sys.dm_io_virtual_file_stats(NULL, NULL) AS IVFS
    JOIN sys.master_files AS MF
      ON IVFS.database_id = MF.database_id
      AND IVFS.file_id = MF.file_id
  GROUP BY DB_NAME(IVFS.database_id), MF.type_desc
)
SELECT db, type_desc, 
  CAST(1. * io_bytes / (1024 * 1024) AS NUMERIC(12, 2)) AS io_mb,
  CAST(io_stall_ms / 1000. AS NUMERIC(12, 2)) AS io_stall_s,
  CAST(100. * io_stall_ms / SUM(io_stall_ms) OVER()
       AS NUMERIC(10, 2)) AS io_stall_pct,
  ROW_NUMBER() OVER(ORDER BY io_stall_ms DESC) AS rn
FROM DBIO
ORDER BY io_stall_ms DESC;


SELECT * FROM Orders
where shipperid = 'A'

SELECT * FROM sys.sysperfinfo
WHERE object_name LIKE '%Buffer Manager%'

SELECT * FROM Orders
WHERE orderdate >= '20080101' AND orderdate < '20080201';

CREATE CLUSTERED INDEX idx_cl_od ON Orders(orderdate)

DROP INDEX idx_cl_od ON Orders

SELECT * FROM sys.dm_exec_plan_attributes(0x0500FF7FFF7AAEE130FD7CD10400000001000000000000000000000000000000000000000000000000000000);

SELECT * FROM sys.dm_exec_sql_text(0x0500FF7FFF7AAEE130FD7CD10400000001000000000000000000000000000000000000000000000000000000);

SELECT * FROM sys.dm_exec_cached_plan_dependent_objects(0x0500FF7FFF7AAEE130FD7CD10400000001000000000000000000000000000000000000000000000000000000);

SELECT * FROM sys.dm_exec_query_plan(0x0500FF7FFF7AAEE130FD7CD10400000001000000000000000000000000000000000000000000000000000000);

--  STATISTICS IO ����IO��ȡ�Ĵ���

-- First clear cache
DBCC DROPCLEANBUFFERS;

SET STATISTICS IO ON;

SELECT orderid, custid, empid, shipperid, orderdate, filler
FROM dbo.Orders
WHERE orderdate >= '20080101'
  AND orderdate < '20080201';
GO

SET STATISTICS IO OFF;
GO


---------------------------------------------------------------------
-- Measuring Runtime of Queries
---------------------------------------------------------------------

-- STATISTICS TIME ����������������йصĴ�CPUʱ���ʵ�ʾ�����ʱ��ʱ����Ϣ.���������ط����ͱ����ʱ��,�����ز�ѯʱ��

-- First clear cache
DBCC DROPCLEANBUFFERS;
DBCC FREEPROCCACHE;

-- Then run
SET STATISTICS TIME ON;

SELECT orderid, custid, empid, shipperid, orderdate, filler
FROM dbo.Orders
WHERE orderdate >= '20080101'
  AND orderdate < '20080201';

SET STATISTICS TIME OFF;
GO


-- First clear cache
DBCC DROPCLEANBUFFERS;
DBCC FREEPROCCACHE;

SET STATISTICS IO ON;
SET STATISTICS TIME ON;

SELECT * FROM ORDERS

SET STATISTICS TIME OFF;
SET STATISTICS IO OFF;
GO