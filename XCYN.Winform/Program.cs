using log4net.Config;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using XCYN.Winform.CefSharp;
using XCYN.Winform.Demo;
using XCYN.Winform.GridView;
using XCYN.Winform.LogViewer;
using XCYN.Winform.MaskLayer;
using XCYN.Winform.MeiTuan;
using XCYN.Winform.NPOI;
using XCYN.Winform.Quartz;
using XCYN.Winform.Quartz.Views;
using XCYN.Winform.ResumeDownload;
using XCYN.Winform.Spire;
using XCYN.Winform.WebSocket;

namespace XCYN.Winform
{
    public class Program
    {

        [DllImport("User32.dll")]
        public static extern int MessageBox(int h, string m, string c, int type);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //return;
            // 代码摘自：http://www.cnblogs.com/wangshenhe/archive/2012/11/14/2769605.html
            //设置应用程序处理异常方式：ThreadException处理  
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常  
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常  
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            InitLog4Net();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XCYN.Winform.AsyncDemo.MainForm());
            
            //方式1
            //Application app = new Application();
            //MainWindow userControl1 = new MainWindow();
            //app.Run(userControl1);

            //方式2
            //Application app = new Application();
            //MainWindow userControl1 = new MainWindow();
            //app.MainWindow = userControl1;
            //userControl1.Show();
            //app.Run();

            //方式3
            //Application app = new Application();
            //app.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            //app.Run();
        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            System.Windows.Forms.MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            System.Windows.Forms.MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
