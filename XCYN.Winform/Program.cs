using log4net.Config;
using System;
using System.IO;
using System.Windows.Forms;
using XCYN.Winform.MeiTuan;

namespace XCYN.Winform
{
    public class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            InitLog4Net();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MeiShi());

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
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }
    }
}
