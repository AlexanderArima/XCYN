using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common;
using XCYN.Winform.Model.Report;
using XCYN.Winform.Report;

namespace XCYN.Winform
{
    public partial class ReportForm : Form
    {

        private SortedList<string, string> _dict_config = new SortedList<string, string>();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            _dict_config = ReportConfig.GetInstance().GetList();//初始化配置文件
            
            for (int i = 0; i < _dict_config.Count; i++)
            {
                toolStripDropDownButton1.DropDownItems.Add(_dict_config.ElementAt(i).Value);
                toolStripDropDownButton1.DropDownItems[i].Tag = _dict_config.ElementAt(i).Key;
            }
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Query();
        }
        
        /// <summary>
        /// 切换报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Query(e.ClickedItem.Tag.ToString());
        }

        private void Query(string index = "")
        {
            if (string.IsNullOrEmpty(index) || index == "CrystalReport1.rpt")
            {
                Query1();
            }
            else if (index == "CrystalReport2.rpt")
            {
                Query2();
            }
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name">名称</param>
        private async void Query1()
        {
            var table = new DataTable();
            try
            {
                table = await Task.Run(() =>
                {
                    table = DbHelperSQL.GetDataTable(@"
                            SELECT [ID],[Name],[Standard],[BatchNumber],[VolumeNumber],
                            [Brand],[Licence],[Size],[Weight],[PrintDate],[AddTime],[State] 
                            FROM T_Wire");

                    return table;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CrystalReport1 c = new CrystalReport1();
            c.SetDataSource(table);
            crystalReportViewer1.ReportSource = c;
            this.crystalReportViewer1.Refresh();
        }


        private async void Query2()
        {
            var table = new DataTable();
            try
            {
                table = await Task.Run(() =>
                {
                    table = DbHelperSQL.GetDataTable(@"
                            SELECT TOP 1000 [ID] ,[Name] ,[Author] ,[Price]
                            FROM [Book].[dbo].[MyBook]");
                    return table;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CrystalReport2 c = new CrystalReport2();
            c.SetDataSource(table);
            crystalReportViewer1.ReportSource = c;
            this.crystalReportViewer1.Refresh();

        }
        
    }
}
