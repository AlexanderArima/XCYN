using System;
using System.Windows;

namespace XCYN.Winform
{
    public class Program:Application
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //方式1
            Application app = new Application();
            MainWindow userControl1 = new MainWindow();
            app.Run(userControl1);

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
    }
}
