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

namespace XCYN.Winform.LogViewer
{
    public partial class LogMain : Form
    {
        #region 控件缩放

        double formWidth;    // 窗体原始宽度
        double formHeight;    // 窗体原始高度
        double scaleX;    // 水平缩放比例
        double scaleY;    // 垂直缩放比例
        Dictionary<string, string> ControlsInfo = new Dictionary<string, string>();    // 控件中心Left,Top,控件Width,控件Height,控件字体Size

        protected void GetAllInitInfo(Control ctrlContainer)
        {
            if (ctrlContainer.Parent == this)//获取窗体的高度和宽度
            {
                formWidth = Convert.ToDouble(ctrlContainer.Width);
                formHeight = Convert.ToDouble(ctrlContainer.Height);
            }
            foreach (Control item in ctrlContainer.Controls)
            {
                if (item.Name.Trim() != "")
                {
                    //添加信息：键值：控件名，内容：据左边距离，距顶部距离，控件宽度，控件高度，控件字体。
                    ControlsInfo.Add(item.Name, (item.Left + item.Width / 2) + "," + (item.Top + item.Height / 2) + "," + item.Width + "," + item.Height + "," + item.Font.Size);
                }
                if ((item as UserControl) == null && item.Controls.Count > 0)
                {
                    GetAllInitInfo(item);
                }
            }

        }

        private void ControlsChangeInit(Control ctrlContainer)
        {
            scaleX = (Convert.ToDouble(ctrlContainer.Width) / formWidth);
            scaleY = (Convert.ToDouble(ctrlContainer.Height) / formHeight);
        }


        /// <summary>
        /// 改变控件大小
        /// </summary>
        /// <param name="ctrlContainer"></param>
        private void ControlsChange(Control ctrlContainer)
        {
            double[] pos = new double[5];//pos数组保存当前控件中心Left,Top,控件Width,控件Height,控件字体Size
            foreach (Control item in ctrlContainer.Controls)//遍历控件
            {
                if (item.Name.Trim() != "")//如果控件名不是空，则执行
                {
                    if ((item as UserControl) == null && item.Controls.Count > 0)//如果不是自定义控件
                    {
                        ControlsChange(item);//循环执行
                    }
                    string[] strs = ControlsInfo[item.Name].Split(',');//从字典中查出的数据，以‘，’分割成字符串组

                    for (int i = 0; i < 5; i++)
                    {
                        pos[i] = Convert.ToDouble(strs[i]);//添加到临时数组
                    }
                    double itemWidth = pos[2] * scaleX;     //计算控件宽度，double类型
                    double itemHeight = pos[3] * scaleY;    //计算控件高度
                    item.Left = Convert.ToInt32(pos[0] * scaleX - itemWidth / 2);//计算控件距离左边距离
                    item.Top = Convert.ToInt32(pos[1] * scaleY - itemHeight / 2);//计算控件距离顶部距离
                    item.Width = Convert.ToInt32(itemWidth);//控件宽度，int类型
                    item.Height = Convert.ToInt32(itemHeight);//控件高度
                    item.Font = new Font(item.Font.Name, float.Parse((pos[4] * Math.Min(scaleX, scaleY)).ToString()));//字体
                }
            }
        }

        #endregion

        private List<LogLevel> _list_level = new List<LogLevel>();

        private string _file_path = "";

        public LogMain()
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

            GetAllInitInfo(this.Controls[0]);
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
                list.Reverse();
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
            LogDetail form = new LogDetail(exception);
            if(form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //读取上次打开文件夹的路径，如果读取不到则默认从C盘读取，然后再用户重新选择了一个路径之后刷新里面的内容
            string path = string.Format("{0}\\Config\\LogFilePath.txt",System.IO.Directory.GetCurrentDirectory());
            var str = File.ReadAllText(path);
            var dict = "C:\\";
            if(str.Length > 0)
            {
                dict = str;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            openFileDialog1.InitialDirectory = dict;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _file_path = openFileDialog1.FileName;
                FileInfo info = new FileInfo(_file_path);
                File.WriteAllText(path,info.DirectoryName);
                label5.Text = openFileDialog1.FileName;          //显示文件路径
            }
        }

        private void LogMain_SizeChanged(object sender, EventArgs e)
        {
            if (ControlsInfo.Count > 0)//如果字典中有数据，即窗体改变
            {
                ControlsChangeInit(this.Controls[0]);//表示pannel控件
                ControlsChange(this.Controls[0]);
            }
        }
    }
}
