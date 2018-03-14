using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common.Dapper;
using Dapper;
using XCYN.Winform.Model.MeiTuan.EF;
using XCYN.Winform.Model.MeiTuan;
using System.Threading;

namespace XCYN.Winform.MeiTuan
{
    public partial class MeiShi : Form
    {

        Common.Sql.redis.RedisCommand _command = new Common.Sql.redis.RedisCommand();
        HtmlWeb _webClient = new HtmlWeb();

        public static object index = 0;
        
        public MeiShi()
        {
            InitializeComponent();
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //从数据中取出URL，然后访问网页，解析网页获取到美食菜单的URL，访问并记录该URL，
            //进入美食列表页后，依次访问不同的分类，将美食的名称，评分，地址，人均消费记录到数据库中
            listBox1.Items.Add($"{DateTime.Now.ToShortTimeString()} 开始抓取数据...");
            HtmlWeb webClient = new HtmlWeb();
           
            try
            {
                using (MeiTuanEntities db = new MeiTuanEntities())
                {
                    var query = from a in db.T_City
                                where a.State == true
                                select new CityViewModel
                                {
                                    ID = a.ID,
                                    Name = a.Name,
                                    URL = a.URL,
                                    MeiShiURL = a.MeiShiURL
                                };

                    var list = query.ToList();
                    int offset = list.Count / 4;
                    Task task = new Task(() => {
                        var list_offset = list.Take(offset).ToList();
                        InsertData(list_offset);
                    });
                    task.Start();
                    Task task2 = new Task(() => {
                        var list_offset = list.Skip(offset).Take(offset).ToList();
                        InsertData(list_offset);
                    });
                    task2.Start();
                    Task task3 = new Task(() => {
                        var list_offset = list.Skip(offset * 2).Take(offset).ToList();
                        InsertData(list_offset);
                    });
                    task3.Start();
                    Task task4 = new Task(() => {
                        var list_offset = list.Skip(offset * 3).ToList();
                        InsertData(list_offset);
                    });
                    task4.Start();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                //conn.Close();
            }
        }

        class CityViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string URL { get; set; }
            public string MeiShiURL { get; set; }
        }

        void InsertData(List<CityViewModel> list)
        {
            HtmlNodeCollection hrefList = null;
            for (int i = 0; i < list.Count(); i = i + 2)
            {
                if (list.ElementAt(i).MeiShiURL == null) continue;
                var document = _webClient.Load(list.ElementAt(i).MeiShiURL);
                hrefList = document.DocumentNode.SelectNodes("//script");
                for (int j = 0; j < hrefList.Count; j++)
                {
                    var item = hrefList[j].InnerHtml;
                    if (item.Contains("window._appState"))
                    {
                        //获取Json数据
                        var data = item.Substring("window._appState =".Length, item.Length - "window._appState =".Length - 1);
                        var obj = JsonConvert.DeserializeObject<Meta>(data);
                        //插入地区
                        int count = obj.filters.Insert();
                        int T_ID = Thread.CurrentThread.ManagedThreadId;
                        listBox1.Invoke(new Action(() => {
                            listBox1.Items.Insert(0, $"导入{obj.cityName}的{count}条数据,线程ID:{T_ID}");
                        }));
                        break;
                    }
                }
            }
        }


    }
}
