using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.Quartz
{
    public partial class TimerForm : Form
    {
        public TimerForm()
        {
            InitializeComponent();
        }

        private void TimerForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dataSet1.T_SimpleTrigger”中。您可以根据需要移动或删除它。
            this.t_SimpleTriggerTableAdapter.Fill(this.dataSet1.T_SimpleTrigger);

        }
        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AddTriggerForm form = new AddTriggerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }
    }
}
