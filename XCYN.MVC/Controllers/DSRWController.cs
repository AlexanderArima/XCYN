using Quartz;
using Quartz.Impl;
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
            var list = scheduler.GetJobGroupNames();
            var jobs = scheduler.GetJobKeys(Quartz.Impl.Matchers.GroupMatcher<JobKey>.GroupContains(""));
            List<JobObj> list_job = new List<JobObj>();
            foreach (var item in jobs)
            {
                list_job.Add(new JobObj {
                    Name = item.Name,
                    GroupName = item.Group
                });
            }
            ViewData["jobObj"] = list_job;
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
    }
}