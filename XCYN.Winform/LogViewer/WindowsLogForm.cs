using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.LogViewer
{
    /// <summary>
    /// Windows日志的帮助类.
    /// </summary>
    public partial class WindowsLogForm : Form
    {
        /// <summary>
        /// 搜索关键字.
        /// </summary>
        private string _keyWord = "";

        public WindowsLogForm()
        {
            InitializeComponent();
        }

        private async void WindowsLogForm_Load(object sender, EventArgs e)
        {
            EventLog eventLog = new EventLog();    // 创建日志实例
            eventLog.Log = "Application";    // 应用程序日志为Application,系统日志为System
            EventLogEntryCollection collection = eventLog.Entries; //获取可遍历collection
            List<string> list_result = new List<string>();
            await Task.Run(() =>
            {
                foreach (EventLogEntry item in collection)
                {
                    if(item.EntryType == EventLogEntryType.Error &&
                       item.Message.Contains(_keyWord))
                    {
                        // 只显示Error级别
                        list_result.Add($"\r\n日期：{item.TimeGenerated}\r\n来源：{item.Source}\r\n日志内容： {item.Message}");
                    }
                }
            });

            list_result.Reverse();
            textBox1.AppendText(string.Join($"\r\n", list_result.ToArray()));
        }
    }
}
