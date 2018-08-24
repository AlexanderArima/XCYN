SELECT DISTINCT Country
FROM T_Customer
ORDER BY CustID

--查询优化 
--DMV
SELECT
  wait_type,	--等待类型
  waiting_tasks_count,	--该类型等待的数量
  wait_time_ms,	--该类型总的等待时间
  max_wait_time_ms,	--最大等待时间
  signal_wait_time_ms	--从收到信号到开始运行之间的时间差
FROM sys.dm_os_wait_stats
where wait_type like '%LCK%'
ORDER BY wait_time_ms desc,wait_type;

--手动重置
DBCC SQLPERF('sys.dm_os_wait_stats',CLEAR);