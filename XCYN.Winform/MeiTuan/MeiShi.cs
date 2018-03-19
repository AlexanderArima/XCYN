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
using log4net;

namespace XCYN.Winform.MeiTuan
{
    
    public partial class MeiShi : Form
    {
        
        HtmlWeb _webClient = new HtmlWeb();
        CancellationTokenSource _cancel_token = new CancellationTokenSource();
        public static int numberOfMeishi = 0;
        
        public MeiShi()
        {
            MyLog.logger.Debug("开启窗口");
            InitializeComponent();
        }

        private void 更新美食模块ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //从数据中取出URL，然后访问网页，解析网页获取到美食菜单的URL，访问并记录该URL，
            //进入美食列表页后，依次访问不同的分类，将美食的名称，评分，地址，人均消费记录到数据库中
            numberOfMeishi = 0;
            listBox1.Items.Add($"{DateTime.Now.ToShortTimeString()} 开始抓取数据...");
            HtmlWeb webClient = new HtmlWeb();
           
            try
            {
                _cancel_token = new CancellationTokenSource(); 
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
                        InsertMeiShi(list_offset);
                    });
                    task.Start();
                    Task task2 = new Task(() => {
                        var list_offset = list.Skip(offset).Take(offset).ToList();
                        InsertMeiShi(list_offset);
                    });
                    task2.Start();
                    Task task3 = new Task(() => {
                        var list_offset = list.Skip(offset * 2).Take(offset).ToList();
                        InsertMeiShi(list_offset);
                    });
                    task3.Start();
                    Task task4 = new Task(() => {
                        var list_offset = list.Skip(offset * 3).ToList();
                        InsertMeiShi(list_offset);
                    });
                    task4.Start();
                }
            }
            catch(Exception ex)
            {
                MyLog.logger.Debug(ex.Message);
            }
        }

        class CityViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string URL { get; set; }
            public string MeiShiURL { get; set; }
        }

        /// <summary>
        /// 导入美食模块的地区，分类，用餐人数，排序规则
        /// </summary>
        /// <param name="list"></param>
        private void InsertMeiShi(List<CityViewModel> list)
        {
            HtmlNodeCollection hrefList = null;
            HtmlAgilityPack.HtmlDocument document = null;
            try
            {
                for (int i = 0; i < list.Count(); i = i + 2)
                {
                    if (_cancel_token.Token.IsCancellationRequested)
                        return;
                    if (list.ElementAt(i).MeiShiURL == null)
                        continue;
                    document = _webClient.Load(list.ElementAt(i).MeiShiURL);
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

                            listBox1.Invoke(new Action(() =>
                            {
                                listBox1.Items.Insert(0, $"导入{obj.cityName}的{count}条数据");
                            }));
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MyLog.logger.Debug(ex.Message);
            }
        }

        private void 更新城市ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Insert(0, $"{DateTime.Now.ToShortTimeString()} 请求数据中...");
            try
            {
                LoadCity();
                IDbConnection conn = null;
                try
                {
                    conn = DapperManager.GetConnection();
                    conn.Open();
                    _cancel_token = new CancellationTokenSource();
                    var list_source = conn.Query<City>("SELECT ID,Name,URL,MeiShiURL FROM T_City WHERE State = @State", new { State = 1 });
                    var offset = list_source.Count() / 3;
                    Task task = new Task(() => {
                        UpdateMeiShi(list_source.Take(offset).ToList(), list_source.Count());
                    });
                    task.Start();
                    Task task2 = new Task(() => {
                        UpdateMeiShi(list_source.Skip(offset).Take(offset).ToList(), list_source.Count());
                    });
                    task2.Start();
                    Task task3 = new Task(() => {
                        UpdateMeiShi(list_source.Skip(offset * 2).ToList(), list_source.Count());
                    });
                    task3.Start();
                }
                catch(Exception ex)
                {
                    MyLog.logger.Error(ex.Message);
                }
                
            }
            catch (Exception ex)
            {
                MyLog.logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// 获取所有城市列表
        /// </summary>
        public void LoadCity()
        {
            HtmlAgilityPack.HtmlDocument doc = null;
            try
            {
                //加载城市列表
                doc = _webClient.Load(Global.uri_city);
            }
            catch (Exception ex)
            {
                MyLog.logger.Error(ex.Message);
            }
            //Dom节点获取列表
            HtmlNodeCollection hrefList = doc.DocumentNode.SelectNodes("//div[@id='app']//a[@href]");
            List<City> list_target = new List<City>();
            for (int i = 0; i < hrefList.Count; i++)
            {
                var url = hrefList[i].Attributes["href"];
                if(i == 0)
                {
                    listBox1.Invoke(new Action(() =>
                    {
                        listBox1.Items.Insert(0, $"导入{i + 1}/{hrefList.Count}条数据");
                    }));
                }
                else
                {
                    listBox1.Items[0] = $"导入{i + 1}/{hrefList.Count}条数据";
                }
                list_target.Add(new City() {
                    URL = url.Value.Insert(0, "http:"),
                    Name = hrefList[i].InnerText
                });
            }

            int count = 0;
            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Insert(0, $"{DateTime.Now.ToShortTimeString()} 导入数据中...");
            }));
            IDbConnection conn = null;
            try
            {
                conn = DapperManager.GetConnection();
                conn.Open();
                var list_source = conn.Query<City>("SELECT Name,URL FROM T_City");
                var list_except = list_target.Except(list_source);
                count = conn.Execute("INSERT INTO T_City(Name,URL) VALUES(@Name,@URL)", list_except);
            }
            catch (Exception ex)
            {
                MyLog.logger.Error(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Insert(0, $"{DateTime.Now.ToShortTimeString()} 解析完毕，导入{count}条数据...");
            }));
        }

        /// <summary>
        /// 更新美食模块的URL
        /// </summary>
        public void UpdateMeiShi(List<City> list_source,int total)
        {
            HtmlAgilityPack.HtmlDocument doc = null;
            IDbConnection conn = null;
            try
            {
                conn = DapperManager.GetConnection();
                
                //一个一个访问URL，并获取美食模块的URL
                for (int i = 0; i < list_source.Count(); i++)
                {
                    if (_cancel_token.IsCancellationRequested)
                    {
                        numberOfMeishi = 0;
                        return;
                    }
                    try
                    {
                        doc = _webClient.Load(list_source.ElementAt(i).URL);
                    }
                    catch (System.Net.WebException ex)
                    {
                        listBox1.Invoke(new Action(() =>
                        {
                            listBox1.Items.Insert(0, $"{DateTime.Now} 更新{list_source.ElementAt(i).Name}的美食地址失败,请查看错误日志,{numberOfMeishi}/{total}");
                        }));
                        MyLog.logger.Error(ex.Message);
                        continue;
                    }
                    finally
                    {
                        listBox1.Invoke(new Action(() =>
                        {
                            listBox1.Items.Insert(0, $"{DateTime.Now} 更新了{list_source.ElementAt(i).Name}的美食地址,{numberOfMeishi}/{total}");
                        }));
                        Interlocked.Increment(ref numberOfMeishi);
                    }
                    var hrefList = doc.DocumentNode.SelectNodes("//div[@class='category-nav-content-wrapper']//a[@href]");
                    foreach (var item in hrefList)
                    {
                        //可能会有很多模块，比如美食，外卖，酒店，电影，机票等，这里只取美食模块的数据
                        if (item.InnerText.Equals("美食"))
                        {
                            var MeiShiURL = item.Attributes["href"].Value;
                            list_source.ElementAt(i).MeiShiURL = MeiShiURL;
                            var count = conn.Execute("UPDATE T_City SET MeiShiURL = @MeiShiURL WHERE ID = @ID", list_source.ElementAt(i));
                            
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyLog.logger.Error(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void 取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cancel_token.Cancel();
            
        }
    }
}
