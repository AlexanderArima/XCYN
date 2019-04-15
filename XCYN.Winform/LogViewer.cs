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

namespace XCYN.Winform
{
    public partial class LogViewer : Form
    {

        private List<LogLevel> _list_level = new List<LogLevel>();

        private string _file_path = "";

        public LogViewer()
        {
            InitializeComponent();

            #region 异常等级

            var LogLevel = ConfigurationManager.AppSettings["LogLevel"];
            _list_level.Add(new LogLevel() {
                Name = "--全部--",
                Value = ""
            });
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

            dateTimePicker1.Value = DateTime.Now.AddDays(-3);
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
                    level = comboBox1.SelectedValue.ToString();
                }
                if(dateTimePicker1.Text.Length > 0)
                {
                    startTime = Convert.ToDateTime(dateTimePicker1.Text);
                }
                if (dateTimePicker2.Text.Length > 0)
                {
                    endTime = Convert.ToDateTime(dateTimePicker2.Text);
                }
                if (string.IsNullOrWhiteSpace(_file_path))
                {
                    MessageBox.Show("请选择要读取的日志文件路径");
                    return;
                }
                var list = model.GetList(_file_path,message, level, startTime, endTime);
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            var level = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var message = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            var className = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            var methodName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            var exception = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            MessageBox.Show(exception);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _file_path = openFileDialog1.FileName;
                label5.Text = openFileDialog1.FileName;          //显示文件路径
            }
        }
    }
}
