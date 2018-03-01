using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.MeiTuan
{
    public partial class ChangeCity : Form
    {
        public ChangeCity()
        {
            InitializeComponent();
        }

        static string _url = "http://www.meituan.com/changecity/";

        private void 启动SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            List<City> list = new List<City>();
            listBox1.Items.Insert(0,$"{DateTime.Now.ToShortTimeString()} 请求数据中...");
            var webClient = new HtmlWeb();
            HtmlNodeCollection hrefList = null;
            var command = new Common.Sql.redis.RedisCommand();
            
            try
            {
                Task task = new Task(() => {
                    Thread.Sleep(5000);
                    var doc = webClient.Load(_url);
                    hrefList = doc.DocumentNode.SelectNodes("//div[@id='app']//a[@href]");
                    foreach (var item in hrefList)
                    {
                        var url = item.Attributes["href"];
                        listBox1.Invoke(new Action(() => {
                            listBox1.Items.Insert(0, url.Value);
                        }));
                        var name = item.InnerText;
                        list.Add(new City() { URL = url.Value.Insert(0, "http:"), Name = name });
                    }
                });
                task.Start();
                task.ContinueWith((obj) =>
                {
                    listBox1.Invoke(new Action(() => {
                        listBox1.Items.Insert(0, $"{DateTime.Now.ToShortTimeString()} 导入数据中...");
                    }));
                    Thread.Sleep(5000);
                    foreach (var item in list)
                    {
                        command.HashSet("City", item.Name, item.URL);
                    }
                    listBox1.Invoke(new Action(()=> {
                        listBox1.Items.Insert(0, $"{DateTime.Now.ToShortTimeString()} 解析完毕...");
                    }));
                    
                });
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void 清空LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
