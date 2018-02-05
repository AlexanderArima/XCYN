using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using XCYN.Common;
using XCYN.Winform.Model.Spider;

namespace XCYN.Winform
{
    public partial class Spider : Form
    {
        public Spider()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 导入T_Category表
        /// </summary>
        private void ImportCategory()
        {
            var s = File.ReadAllText(@"D:\MyProject\XCYN\XCYN.Winform\Model\Spider\meishi.html");
            var list = Regex.Matches(s, @"<a class="""" href=""\S*c\d*/"" data-reactid=""\d*"">\S*</a>", RegexOptions.IgnoreCase);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        List<T_Category> list_category = new List<T_Category>();
                        foreach (Match item in list)
                        {
                            var name = Utils.SubStr(item.Value, item.Value.IndexOf('>') + ">".Length, item.Value.IndexOf('<', 10));
                            var url = Utils.SubStr(item.Value, item.Value.IndexOf(@"href=""") + @"href=""".Length, item.Value.IndexOf(@""" data-reactid"));
                            var model = new T_Category()
                            {
                                name = name,
                                url = url
                            };
                            list_category.Add(model);

                        }
                        db.T_Category.InsertAllOnSubmit(list_category);
                        db.SubmitChanges();
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public CookieContainer _CookieContainer { get; set; }//定义cookie容器
        public string _PageSource { get; set; } //网页源代码

        private void SpiderWebSite(string url)
        {
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "*/*";
                request.ContentType = "text/html;charset=UTF-8";//定义文档类型及编码
                request.AllowAutoRedirect = false;//禁止自动跳转
                                                  //设置User-agent，伪装成QQ浏览器
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.4549.400 QQBrowser/9.7.12900.400";
                request.Timeout = 50000;
                request.KeepAlive = true;
                request.Method = "GET";
                request.CookieContainer = this._CookieContainer;//附加Cookie容器
                request.ServicePoint.ConnectionLimit = int.MaxValue;//请求最大连接数
                var response = (HttpWebResponse)request.GetResponse();
                foreach (Cookie item in response.Cookies)
                {
                    this._CookieContainer.Add(item);//将Cookie加入容器，保持登陆状态
                }
                var stream = response.GetResponseStream();//获取响应流
                var reader = new StreamReader(stream, Encoding.UTF8);//以UTF8的形式读取响应流
                _PageSource = reader.ReadToEnd();
                watch.Stop();
                reader.Close();
                stream.Close();
                request.Abort();
                response.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SpiderWebSite(string url,WebProxy proxy)
        {
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "*/*";
                request.ContentType = "text/html;charset=UTF-8";//定义文档类型及编码
                request.AllowAutoRedirect = false;//禁止自动跳转
                                                  //设置User-agent，伪装成QQ浏览器
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.4549.400 QQBrowser/9.7.12900.400";
                request.Timeout = 50000;
                request.KeepAlive = true;
                request.Method = "GET";
                if (proxy != null)
                {
                    //设置代理服务器IP，伪装请求地址
                    request.Proxy = proxy;
                }
                request.CookieContainer = this._CookieContainer;//附加Cookie容器
                request.ServicePoint.ConnectionLimit = int.MaxValue;//请求最大连接数
                var response = (HttpWebResponse)request.GetResponse();
                foreach (Cookie item in response.Cookies)
                {
                    this._CookieContainer.Add(item);//将Cookie加入容器，保持登陆状态
                }
                var stream = response.GetResponseStream();//获取响应流
                var reader = new StreamReader(stream, Encoding.UTF8);//以UTF8的形式读取响应流
                _PageSource = reader.ReadToEnd();
                watch.Stop();
                reader.Close();
                stream.Close();
                request.Abort();
                response.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
