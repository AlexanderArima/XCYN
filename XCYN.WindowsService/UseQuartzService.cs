using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common;

namespace XCYN.WindowsService
{
    /// <summary>
    /// 使用了Quartz的Windows服务.
    /// </summary>
    partial class UseQuartzService : ServiceBase
    {
        IScheduler _scheduler;

        public UseQuartzService()
        {
            InitializeComponent();
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                _scheduler = factory.GetScheduler();
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("UseQuartzService构造函数，出错：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 启动服务时触发.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try 
            {
                IJobDetail job = JobBuilder
                    .Create<MyJob>()
                    .WithIdentity("1525job", "1525job")
                    .WithDescription("我的任务")
                    .Build();

                //ITrigger trigger = TriggerBuilder.Create()
                //    .StartNow()
                //    .WithIdentity("1505trigger", "1505trigger")
                //    .WithSimpleSchedule(m => m.WithIntervalInSeconds(10).RepeatForever())
                //    .Build();
                var cronString = "0 0/1 * * * ?";    // 每分钟执行一次
                ITrigger trigger = TriggerBuilder.Create()
                    .StartNow()
                    .WithIdentity("1525trigger", "1525trigger")
                    .WithCronSchedule(cronString)
                    .Build();

                _scheduler.ScheduleJob(job, trigger);
                _scheduler.Start();
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("UseQuartzService：OnStart，出错：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 关闭服务时触发.
        /// </summary>
        protected override void OnStop()
        {
            if(_scheduler.IsShutdown == false)
            {
                _scheduler.Shutdown();
            }
        }

        /// <summary>
        /// 暂停时触发.
        /// </summary>
        protected override void OnPause()
        {
            this._scheduler.PauseAll();
            base.OnPause();
        }

        /// <summary>
        /// 恢复时触发.
        /// </summary>
        protected override void OnContinue()
        {
            this._scheduler.ResumeAll();
            base.OnContinue();
        }
    }
}
