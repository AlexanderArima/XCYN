using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Winform.Quartz.Model;

namespace XCYN.Winform.Quartz.Views
{
    public partial class TimerForm : Form
    {
        public TimerForm()
        {
            InitializeComponent();
        }

        private void TimerForm_Load(object sender, EventArgs e)
        {
            T_SimpleTrigger trigger = new T_SimpleTrigger();
            var ds = trigger.GetList("");
            gridControl1.DataSource = ds.Tables[0];
        }
        
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            T_SimpleTrigger trigger = new T_SimpleTrigger();
            var ds = trigger.GetList("");
            gridControl1.DataSource = ds.Tables[0];
        }

        private void 添加计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTriggerForm form = new AddTriggerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void 添加服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddServiceForm form = new AddServiceForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void 服务列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceForm form = new ServiceForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }

        }
    }
}
