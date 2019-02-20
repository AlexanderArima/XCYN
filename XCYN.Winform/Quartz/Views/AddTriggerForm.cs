using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common;
using XCYN.Winform.Quartz.Model;

namespace XCYN.Winform.Quartz.Views
{
    public partial class AddTriggerForm : Form
    {

        static Color WARN_COLOR = Color.Pink;
        static Color NORMAL_COLOR = Color.White;
        //DataTable _ServiceList_DataTable = new DataTable();

        public AddTriggerForm()
        {
            InitializeComponent();
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dateTimePicker2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            T_ServiceList service = new T_ServiceList();
            var ds = service.GetList("");
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "ServiceName";
            comboBox1.ValueMember = "ID";
            //_ServiceList_DataTable = ds.Tables[0];
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var row = ds.Tables[0].Rows[i];
                auto.Add(row["ServiceName"].ToString());
            }
            comboBox1.AutoCompleteCustomSource = auto;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddTriggerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 确定按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var startTime = dateTimePicker1.Text;
            var endTime = dateTimePicker2.Text;
            var repeatTime_temp = textBox1.Text;
            var repeatInterval_temp = textBox2.Text;
            int repeatTime = 0;
            int repeatInterval = 0;
            if (!int.TryParse(repeatTime_temp, out repeatTime))
            {
                MessageBox.Show("重复次数只能输入数字");
                textBox1.BackColor = WARN_COLOR;
                return;
            }
            if(repeatTime < 0)
            {
                MessageBox.Show("重复次数只能输入正整数");
                textBox1.BackColor = WARN_COLOR;
                return;
            }
            if (!int.TryParse(repeatInterval_temp, out repeatInterval))
            {
                MessageBox.Show("重复次数只能输入数字");
                textBox2.BackColor = WARN_COLOR;
                return;
            }
            if (repeatInterval < 0)
            {
                MessageBox.Show("重复次数只能输入正整数");
                textBox2.BackColor = WARN_COLOR;
                return;
            }
            T_SimpleTrigger simpleTrigger = new T_SimpleTrigger()
            {
                StartTime = ConvertHelper.ToDateTime(startTime),
                EndTime = ConvertHelper.ToDateTime(endTime),
                RepeatTime = ConvertHelper.ToInt(repeatTime),
                RepeatInterval = ConvertHelper.ToInt(repeatInterval)
            };
            if(simpleTrigger.Add(comboBox1.Text) > 0)
            {
                //添加成功，提示并关闭画面
                if(MessageBox.Show("添加成功","提示") == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                //添加失败
                MessageBox.Show("添加失败，请重试");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = NORMAL_COLOR;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = NORMAL_COLOR;
        }

        /// <summary>
        /// 输入值后可以进行搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {

        }
    }
}
