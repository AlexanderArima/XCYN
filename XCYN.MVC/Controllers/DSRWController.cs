using Quartz;
using Quartz.Collection;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using XCYN.MVC.ViewModel;

namespace XCYN.MVC.Controllers
{
    /// <summary>
    /// 定时任务Web框架
    /// </summary>
    public class DSRWController : Controller
    {

        IScheduler scheduler;

        public DSRWController()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
        }

        // GET: DSRW
        public ActionResult Index()
        {
            //schedule
            var meta = scheduler.GetMetaData();
            var name = scheduler.SchedulerName;
            var id = scheduler.SchedulerInstanceId;
            var isStarted = scheduler.IsStarted;
            var isShutdown = scheduler.IsShutdown;
            ScheduleObject obj = new ScheduleObject()
            {
                Name = name,
                ID = id,
                IsStarted = isStarted == true ? "1" : "0",
                IsShutdown = isShutdown == true ? "1" : "0",
                Version = meta.Version,
                JobNumbers = meta.NumberOfJobsExecuted.ToString(),
            };
            ViewData["scheObj"] = obj;
            //Job
            var list = scheduler.GetJobGroupNames();
            var jobs = scheduler.GetJobKeys(Quartz.Impl.Matchers.GroupMatcher<JobKey>.GroupContains(""));
            List<JobObj> list_job = new List<JobObj>();
            foreach (var item in jobs)
            {
                var detail = scheduler.GetJobDetail(item);
                JobObj job = new JobObj
                {
                    Name = item.Name,
                    GroupName = item.Group,
                    Description = detail.Description,
                    IsDurable = detail.Durable,
                    Type = detail.JobType.FullName
                };
                var list_trig = scheduler.GetTriggersOfJob(new JobKey(item.Name, item.Group));
                List<string> list_temp1 = new List<string>();
                foreach (var item2 in list_trig)
                {
                    list_temp1.Add(item2.Key.Group + "." + item2.Key.Name);
                }
                job.Trigger = string.Join(",", list_temp1);
                list_job.Add(job);
            }
            ViewData["jobObj"] = list_job;
            //触发器(Trigger)
            var triggers = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupContains(""));
            List<TriggerObj> list_trigger = new List<TriggerObj>();
            foreach (var item in triggers)
            {
                var detail = scheduler.GetTrigger(item);
                list_trigger.Add(new TriggerObj() {
                    Name = item.Name,
                    GroupName = item.Group,
                    Description = detail.Description,
                    StartTime = detail.StartTimeUtc.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = detail.EndTimeUtc.HasValue? detail.EndTimeUtc.Value.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    NextTime = detail.GetNextFireTimeUtc().HasValue ? detail.GetNextFireTimeUtc().Value.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    PreviousTime = detail.GetPreviousFireTimeUtc().HasValue ? detail.GetPreviousFireTimeUtc().Value.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    State = scheduler.GetTriggerState(new TriggerKey(item.Name, item.Group)).ToString(),
                });
                
            }
            ViewData["triggerObj"] = list_trigger;
            //排除日历(Calendar)
            List<CalendarObj> list_calendar = new List<CalendarObj>();
            var calendars = scheduler.GetCalendarNames();
            for (int i = 0; i < calendars.Count; i++)
            {
                var item = scheduler.GetCalendar(calendars[i]) as DailyCalendar;
                var calendarName = calendars[i];
                var startTime = item.GetTimeRangeStartingTimeUtc(DateTimeOffset.Now).ToString("yyyy-MM-dd HH:mm:ss");
                var endTime = item.GetTimeRangeEndingTimeUtc(DateTimeOffset.Now).ToString("yyyy-MM-dd HH:mm:ss");
                CalendarObj detail = new CalendarObj()
                {
                    calendarName = calendarName,
                    startTime = startTime,
                    endTime = endTime,
                    calendarType = "DailyCalendar",
                };
                list_calendar.Add(detail);
            }
            ViewData["calendarObj"] = list_calendar;
            return View();
        }

        /// <summary>
        /// 添加Job
        /// </summary>
        /// <returns></returns>
        public JsonResult AddJob(string jobName,string groupName,string className,string namespaceName,string description)
        {
            try
            {
                //Load(命名空间).GetType(命名空间.类名)
                var type = Assembly.Load(namespaceName).GetType(namespaceName + "." + className);
                if (scheduler.CheckExists(new JobKey(jobName, groupName)) == true)
                {
                    return Json(new
                    {
                        code = "999999",
                        msg = "该Job已经存在"
                    });
                }
                var job = JobBuilder.Create(type).StoreDurably(true)
                                                  .WithDescription(description)
                                                  .WithIdentity(jobName, groupName)
                                                  .Build();

                scheduler.AddJob(job, true);

            }
            catch(Exception ex)
            {
                return Json(new {
                    code = "999999",
                    msg = ex.Message
                });
            }
            return Json(new {
                code = "1",
                msg = ""
            });
        }

        /// <summary>
        /// 编辑Job
        /// </summary>
        /// <returns></returns>
        public JsonResult EditJob(string jobName, string groupName, string className, string namespaceName, string description)
        {
            try
            {
                //Load(命名空间).GetType(命名空间.类名)
                var type = Assembly.Load(namespaceName).GetType(namespaceName + "." + className);
           
                var job = JobBuilder.Create(type).StoreDurably(true)
                                                  .WithDescription(description)
                                                  .WithIdentity(jobName, groupName)
                                                  .Build();
                scheduler.AddJob(job, true);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
            return Json(new
            {
                code = "1",
                msg = ""
            });
        }

        /// <summary>
        /// 删除Job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public JsonResult DeleteJob(string jobName,string groupName)
        {
            try
            {
                var flag = scheduler.DeleteJob(new JobKey(jobName, groupName));
                if(flag == true)
                {
                    return Json(new
                    {
                        code = "1",
                        msg = ""
                    });
                }
                else
                {
                    return Json(new
                    {
                        code = "0",
                        msg = "删除失败"
                    });
                }
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public JsonResult PauseJob(string jobName, string groupName)
        {
            try
            {
                scheduler.PauseJob(new JobKey(jobName, groupName));
                return Json(new
                {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public JsonResult ResumeJob(string jobName, string groupName)
        {
            try
            {
                scheduler.ResumeJob(new JobKey(jobName, groupName));
                return Json(new
                {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 添加触发器
        /// </summary>
        /// <returns></returns>
        public JsonResult AddTrigger(DSRWControlViewModel_AddTrigger model)
        {
            try
            {
                if(scheduler.CheckExists(new TriggerKey(model.triggerName,model.triggerGroupName)) == true)
                {
                    return Json(new {
                        code = "999999",
                        msg = "该触发器已存在"
                    });
                }
                var trigger = TriggerBuilder.Create()
                                                       .StartNow()
                                                       .ForJob(model.jobName, model.jobGroupName)
                                                       .WithIdentity(model.triggerName, model.triggerGroupName)
                                                       .WithCronSchedule(model.cornExpression)
                                                       .WithDescription(model.description)
                                                       .Build();
                scheduler.ScheduleJob(trigger);
                return Json(new {
                    code = "1",
                    msg = ""
                });
            }
            catch(Exception ex)
            {
                return Json(new {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 编辑触发器
        /// </summary>
        /// <returns></returns>
        public JsonResult EditTrigger(DSRWControlViewModel_AddTrigger model)
        {
            try
            {
                var jobDetail = scheduler.GetJobDetail(new JobKey(model.jobName, model.jobGroupName));
                var trigger = TriggerBuilder.Create()
                                                       .StartNow()
                                                       .ForJob(model.jobName, model.jobGroupName)
                                                       .WithIdentity(model.triggerName, model.triggerGroupName)
                                                       .WithCronSchedule(model.cornExpression)
                                                       .WithDescription(model.description)
                                                       .Build();
                var list_trigger = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
                var hash = new Quartz.Collection.HashSet<ITrigger>();
                foreach (var item in list_trigger)
                {
                    if(item.Name == model.triggerName && item.Group == model.triggerGroupName)
                    {
                        var tri = TriggerBuilder.Create()
                                                       .StartNow()
                                                       .ForJob(model.jobName, model.jobGroupName)
                                                       .WithIdentity(model.triggerName, model.triggerGroupName)
                                                       .WithCronSchedule(model.cornExpression)
                                                       .WithDescription(model.description)
                                                       .Build();
                        //var tri =  scheduler.GetTrigger(new TriggerKey(model.triggerName, model.triggerGroupName));
                        hash.Add(tri);
                    }
                }
               
                scheduler.ScheduleJob(jobDetail, hash, true);
                
                return Json(new
                {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 暂停Trigger
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public JsonResult PauseTrigger(string triggerName, string groupName)
        {
            try
            {
                scheduler.PauseTrigger(new TriggerKey(triggerName, groupName));
                return Json(new
                {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 恢复Trigger
        /// </summary>
        /// <param name="triggerName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public JsonResult ResumeTrigger(string triggerName,string groupName)
        {
            try
            {
                scheduler.ResumeTrigger(new TriggerKey(triggerName, groupName));
                return Json(new
                {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 暂停Schedule
        /// </summary>
        /// <returns></returns>
        public JsonResult PauseSchedule()
        {
            try
            {
                scheduler.Standby();
                scheduler.PauseAll();
                return Json(new {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 恢复Schedule
        /// </summary>
        /// <returns></returns>
        public JsonResult ResumeSchedule()
        {
            try
            {
                if(scheduler.IsStarted == false)
                {
                    scheduler.Start();
                }
                scheduler.ResumeAll();
                return Json(new
                {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 关闭Schedule
        /// </summary>
        /// <returns></returns>
        public JsonResult CloseSchedule()
        {
            try
            {
                if(scheduler.IsShutdown == false)
                {
                    scheduler.Shutdown(false);
                }
                return Json(new
                {
                    code = "1",
                    msg = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = "999999",
                    msg = ex.Message
                });
            }
        }

        /// <summary>
        /// 添加Calendar
        /// </summary>
        /// <returns></returns>
        public JsonResult AddCalendar(DSRWControlViewModel_AddCalendar model)
        {
            if(model.calendarType == "DailyCalendar")
            {
                var list_startTime = model.startTime.Split(':');
                var list_endTime = model.endTime.Split(':');
                if (list_startTime.Length > 1 && list_endTime.Length > 1)
                {
                    var cale = new DailyCalendar(
                       string.Format("{0}:{1}:{2}", list_startTime.ElementAt(0),
                       list_startTime.ElementAt(1),
                       list_startTime.ElementAt(2)),
                       string.Format("{0}:{1}:{2}", list_endTime.ElementAt(0),
                       list_endTime.ElementAt(1),
                       list_endTime.ElementAt(2))
                       );
                    cale.Description = model.description;
                    scheduler.AddCalendar(model.calendarName, cale, true, true);
                    var trigger = scheduler.GetTrigger(new TriggerKey(model.triggerName, model.triggerGroup));
                    if(trigger != null)
                    {
                        //如果触发器存在则，修改触发器，调用修改Calendar方法
                        var newTrigger = trigger.GetTriggerBuilder().ModifiedByCalendar(model.calendarName).Build();
                        //更新Schedule
                        scheduler.RescheduleJob(new TriggerKey(model.triggerName, model.triggerGroup), newTrigger);
                    }
                    return Json(new {
                        code = "1",
                        msg = ""
                    });
                }
                else
                {
                    return Json(new {
                        code = "999999",
                        msg = "请输入起始时间和结束时间"
                    });
                }
            }
            else
            {
                return Json(new {
                    code = "101",
                    msg = "目前只支持DaliyCalendar模式"
                });
            }
        }


        /// <summary>
        /// 修改Calendar
        /// </summary>
        /// <returns></returns>
        public JsonResult EditCalendar(DSRWControlViewModel_AddCalendar model)
        {
            if (model.calendarType == "DailyCalendar")
            {
                var list_startTime = model.startTime.Split(':');
                var list_endTime = model.endTime.Split(':');
                if (list_startTime.Length > 1 && list_endTime.Length > 1)
                {
                    var cale = new DailyCalendar(
                       string.Format("{0}:{1}:{2}", list_startTime.ElementAt(0),
                       list_startTime.ElementAt(1),
                       list_startTime.ElementAt(2)),
                       string.Format("{0}:{1}:{2}", list_endTime.ElementAt(0),
                       list_endTime.ElementAt(1),
                       list_endTime.ElementAt(2))
                       );
                    cale.Description = model.description;
                    scheduler.AddCalendar(model.calendarName, cale, true, true);
                    var trigger = scheduler.GetTrigger(new TriggerKey(model.triggerName, model.triggerGroup));
                    if (trigger != null)
                    {
                        //如果触发器存在则，修改触发器，调用修改Calendar方法
                        var newTrigger = trigger.GetTriggerBuilder().ModifiedByCalendar(model.calendarName).Build();
                        //更新Schedule
                        scheduler.RescheduleJob(new TriggerKey(model.triggerName, model.triggerGroup), newTrigger);
                    }
                    return Json(new
                    {
                        code = "1",
                        msg = ""
                    });
                }
                else
                {
                    return Json(new
                    {
                        code = "999999",
                        msg = "请输入起始时间和结束时间"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    code = "101",
                    msg = "目前只支持DaliyCalendar模式"
                });
            }
        }

        /// <summary>
        /// 删除Calendar
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteCalendar(string calendarName)
        {
            //var trigger = scheduler.GetTrigger(new TriggerKey("", ""));
            var triggerKey = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
            foreach (var item in triggerKey)
            {
                var trigger = scheduler.GetTrigger(item);
                if(calendarName == trigger.CalendarName)
                {
                    var newTrigger = trigger.GetTriggerBuilder().ModifiedByCalendar(null).Build();
                    scheduler.RescheduleJob(item, newTrigger);
                    var flag = scheduler.DeleteCalendar(calendarName);
                    if (flag == true)
                    {
                        return Json(new
                        {
                            code = "1",
                            msg = ""
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            code = "101",
                            msg = "删除失败"
                        });
                    }
                }
            }
            return Json(new
            {
                code = "101",
                msg = "删除失败"
            });
        }

    }

    public class ScheduleObject
    {
        public string Name { get; set; }

        public string ID { get; set; }

        public string IsStarted { get; set; }

        public string IsShutdown { get; set; }

        public string Version { get; set; }

        public string JobNumbers { get; set; }

    }

    public class JobObj
    {
        /// <summary>
        /// Job名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Group名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Job描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否持久化
        /// </summary>
        public bool IsDurable { get; set; }

        /// <summary>
        /// 类型名称(带命名空间)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 与Job绑定的Trigger列表
        /// </summary>
        public string Trigger { get; set; }
    }

    public class TriggerObj
    {      
        /// <summary>
        /// Job名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Group名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Job描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 下一次执行时间
        /// </summary>
        public string NextTime { get; set; }

        /// <summary>
        /// 上一次执行时间
        /// </summary>
        public string PreviousTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
    }

    public class CalendarObj
    {
        /// <summary>
        /// 日历名称
        /// </summary>
        public string calendarName { get; set; }

        /// <summary>
        /// 日历类型
        /// </summary>
        public string calendarType { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 触发器的组名
        /// </summary>
        public string triggerGroup { get; set; }

        /// <summary>
        /// 触发器的名称
        /// </summary>
        public string triggerName { get; set; }
    }

   
}