using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XCYN.WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private object _lockList = new object();

        public MainWindow()
        {
            InitializeComponent();
            //BindingOperations.EnableCollectionSynchronization(listbox1, _lockList);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //基于事件的异步模式定义了一个带有“Async”后缀的方法
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.DownloadStringCompleted += (sender1, e1) =>
            {
                var resp = e1.Result;

            };
            client.DownloadStringAsync(new Uri("http://www.baidu.com"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //异步模式定义了BeginXXX和EndXXX方法
            WebRequest request = WebRequest.Create("http://www.baidu.com");
            var result = request.BeginGetResponse((obj) => {
            }, null);
            var resp = request.EndGetResponse(result);
            const int buffer = 4096;
            var content = string.Empty;
            using (var stream = resp.GetResponseStream())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bool flag = false;
                    byte[] b = new byte[buffer];
                    while (!flag)
                    {
                        //读取网络流
                        var index = stream.Read(b, 0, buffer);
                        if (index == 0)
                        {
                            flag = true;
                        }
                        //写入内存流中
                        memoryStream.Write(b, 0, index);

                        content += Encoding.UTF8.GetString(b);
                    }
                }
                var ss = content;
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //基于任务的异步模式
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string resp = await client.DownloadStringTaskAsync("http://www.baidu.com");
            
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //基于任务的异步模式
            var client = new HttpClient();
            var response = await client.GetAsync("http://www.baidu.com");
            string resp = await response.Content.ReadAsStringAsync();
            await Task.Run(() => {
                //在子线程中不能对UI进行修改!
                Thread.Sleep(2000);
            });
            //只能在任务外执行
            for (int i = 0; i < 10000; i++)
            {
                listbox1.Items.Add(i);
            }
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //调用同步方法，会出现UI卡死
            //var msg = Greeting("Cheng");
            //调用异步方法，不会出现UI卡死
            string msg = await GreetingAsync("Cheng");
            MessageBox.Show(msg);
        }

        public string Greeting(string name)
        {
            //同步任务
            Task.Delay(2000).Wait();//等同于Thread.Sleep()
            return "hello " + name;
        }

        public string Greeting2(string name)
        {
            //同步任务
            Task.Delay(6000).Wait();//等同于Thread.Sleep()
            return "hello " + name;
        }

        public Task<string> GreetingAsync(string name)
        {
            //异步任务
            return Task.Run<string>(() => {
                return Greeting(name);
            });
        }

        public Task<string> GreetingAsync2(string name)
        {
            //异步任务
            return Task.Run<string>(() => {
                return Greeting2(name);
            });
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //延续任务
            Task<string> task = GreetingAsync("Xie");
            task.ContinueWith(t => {
                var msg = t.Result + " !";
                MessageBox.Show(msg);
            });
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Task<string> task = GreetingAsync("Wang");//耗时2秒
            Task<string> task2 = GreetingAsync2("Bai");//耗时6秒
            //其中一个线程执行完毕就会继续执行
            //var task3 = await Task.WhenAny(task, task2);
            //MessageBox.Show(task3.Result);
            //当所有线程执行完才会继续执行
            var task3 = await Task.WhenAll(task, task2);
            MessageBox.Show(task3[0] + "," + task3[1]);
        }
    }
}
