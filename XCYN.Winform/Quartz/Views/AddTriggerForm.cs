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
using XCYN.Winform.Quartz.ViewModel;

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
        }

        private void AddTriggerForm_Load(object sender, EventArgs e)
        {
            //dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //dateTimePicker2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            AddTriggerFormViewModel_GetList service = new AddTriggerFormViewModel_GetList();
            var ds = service.GetList();
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

            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";

            this.dateEdit1.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dateEdit1.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dateEdit1.Properties.VistaTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.dateEdit1.Properties.VistaTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.VistaTimeProperties.EditFormat.FormatString = "HH:mm";
            this.dateEdit1.Properties.VistaTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm";

            this.dateEdit2.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";

            this.dateEdit2.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dateEdit2.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dateEdit2.Properties.VistaTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.dateEdit2.Properties.VistaTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.VistaTimeProperties.EditFormat.FormatString = "HH:mm";
            this.dateEdit2.Properties.VistaTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm";

            //为空的时候显示的内容
            lookUpEdit1.Properties.NullText = "";
            lookUpEdit1.Properties.DataSource = new string[]
            {
                "秒","分","小时","天","周","月","年"
            };
            //dropDownButton1.
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
            //var startTime = dateTimePicker1.Text;
            //var endTime = dateTimePicker2.Text;
            var startTime = dateEdit1.Text;
            var endTime = dateEdit2.Text;
            var repeatTime_temp = textEdit1.Text;
            var repeatInterval_temp = textEdit2.Text;
            int repeatTime = 0;
            int repeatInterval = 0;
            var serviceName = comboBox1.Text;
            if (!int.TryParse(repeatTime_temp, out repeatTime))
            {
                MessageBox.Show("重复次数只能输入数字");
                textEdit1.BackColor = WARN_COLOR;
                return;
            }
            if(repeatTime < 0)
            {
                MessageBox.Show("重复次数只能输入正整数");
                textEdit1.BackColor = WARN_COLOR;
                return;
            }
            if (!int.TryParse(repeatInterval_temp, out repeatInterval))
            {
                MessageBox.Show("重复次数只能输入数字");
                textEdit2.BackColor = WARN_COLOR;
                return;
            }
            if (repeatInterval < 0)
            {
                MessageBox.Show("重复次数只能输入正整数");
                textEdit2.BackColor = WARN_COLOR;
                return;
            }
            if (lookUpEdit1.Text.Equals("分"))
            {
                repeatInterval = 60 * repeatInterval;
            }
            else if (lookUpEdit1.Text.Equals("小时"))
            {
                repeatInterval = 60 * 60 * repeatInterval;
            }
            else if (lookUpEdit1.Text.Equals("天"))
            {
                repeatInterval = 24 * 60 * 60 * repeatInterval;
            }
            else if (lookUpEdit1.Text.Equals("周"))
            {
                repeatInterval = 7 * 24 * 60 * 60 * repeatInterval;
            }
            else if (lookUpEdit1.Text.Equals("月"))
            {
                repeatInterval = 31 * 7 * 24 * 60 * 60 * repeatInterval;
            }
            else if (lookUpEdit1.Text.Equals("年"))
            {
                repeatInterval = 12 * 31 * 7 * 24 * 60 * 60 * repeatInterval;
            }
            //插入数据
            AddTriggerFormViewModel_Add simpleTrigger = new AddTriggerFormViewModel_Add()
            {
                StartTime = ConvertHelper.ToDateTime(startTime),
                EndTime = ConvertHelper.ToDateTime(endTime),
                RepeatTime = ConvertHelper.ToInt(repeatTime),
                RepeatInterval = ConvertHelper.ToInt(repeatInterval),
                ServiceName = serviceName
            };
            if(simpleTrigger.Add(simpleTrigger) > 0)
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

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            textEdit1.BackColor = NORMAL_COLOR;
        }

        private void textEdit2_TextChanged(object sender, EventArgs e)
        {
            textEdit2.BackColor = NORMAL_COLOR;
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
