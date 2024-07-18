using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.WindowsService
{
    public partial class MyService : ServiceBase
    {
        System.Timers.Timer myTimer;
        public MyService()
        {
            InitializeComponent();

            // 初始化定时器
            myTimer = new System.Timers.Timer();
            myTimer.Interval = 10000; //设置计时器事件间隔执行时间
            myTimer.Elapsed += timer1_Tick;
            this.ServiceName = "我的服务";
            this.AutoLog = true;    // 是否自行写入系统的事件日志
            this.CanHandlePowerEvent = true;    // 是否接受电源事件
            this.CanPauseAndContinue = true;    // 是否能暂停或继续
            this.CanShutdown = true;    // 是否在计算机关闭时收到通知
            this.CanStop = true;    // 是否接受停止运行的请求
        }

        protected override void OnStart(string[] args)
        {
            this.myTimer.Enabled = true;
            File.AppendAllText("C:\\1.txt", "Service Started，启动定时器\r\n");
        }

        protected override void OnStop()
        {
            this.myTimer.Enabled = false;
            File.AppendAllText("C:\\1.txt", "Service Stoped，关闭定时器\r\n");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            File.AppendAllText("C:\\1.txt", "定时执行任务\r\n");
        }
    }
}
