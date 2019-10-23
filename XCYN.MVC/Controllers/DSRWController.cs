using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

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
                list_job.Add(new JobObj {
                    Name = item.Name,
                    GroupName = item.Group,
                    Description = detail.Description,
                    IsDurable = detail.Durable,
                    Type = detail.JobType.FullName
                });
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
                    StartTime = detail.StartTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = detail.EndTimeUtc.HasValue? detail.EndTimeUtc.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    LastTime = detail.FinalFireTimeUtc.HasValue ? detail.FinalFireTimeUtc.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                });
            }
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
        /// 最近一次执行时间
        /// </summary>
        public string LastTime { get; set; }
    }
}