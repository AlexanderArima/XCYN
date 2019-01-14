using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Winform.Model.LogViewer;
using System.Linq;

namespace XCYN.Winform
{
    public partial class LogViewer : Form
    {

        private List<LogLevel> _list_level = new List<LogLevel>();

        public LogViewer()
        {
            InitializeComponent();

            #region 读取日志等级

            var LogLevel = ConfigurationManager.AppSettings["LogLevel"];
            var list_level = LogLevel.Split(',');
            for (int i = 0; i < list_level.Count(); i++)
            {
                LogLevel obj = new LogLevel();
                obj.Name = list_level[i];
                obj.Value = list_level[i];
                _list_level.Add(obj);
            }
            comboBox1.DataSource = _list_level;

            #endregion
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        { 
            try
            {
                string message = null;
                string level = null;
                DateTime? startTime = null;
                DateTime? endTime = null;
                LogModel model = new LogModel();
                if(textBox1.Text.Length > 0)
                {
                    message = textBox1.Text;
                }
                if(comboBox1.Text.Length > 0)
                {
                    level = comboBox1.Text;
                }
                if(dateTimePicker1.Text.Length > 0)
                {
                    startTime = Convert.ToDateTime(dateTimePicker1.Text);
                }
                if (dateTimePicker2.Text.Length > 0)
                {
                    endTime = Convert.ToDateTime(dateTimePicker2.Text);
                }
                var list = model.GetList(message, level, startTime, endTime);
                dataGridView1.DataSource = list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }
    }
}
