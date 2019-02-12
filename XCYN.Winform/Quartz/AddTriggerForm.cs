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

namespace XCYN.Winform.Quartz
{
    public partial class AddTriggerForm : Form
    {

        static Color WARN_COLOR = Color.Pink;
        static Color NORMAL_COLOR = Color.White;

        public AddTriggerForm()
        {
            InitializeComponent();
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dateTimePicker2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
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
            if(simpleTrigger.Add() > 0)
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
    }
}
