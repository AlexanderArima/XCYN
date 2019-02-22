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
using XCYN.Winform.Quartz.ViewModel;

namespace XCYN.Winform.Quartz.Views
{
    public partial class TriggerForm :  DevExpress.XtraEditors.XtraForm
    {
        public TriggerForm()
        {
            InitializeComponent();
        }

        private void TimerForm_Load(object sender, EventArgs e)
        {
            TriggerFormViewModel trigger = new TriggerFormViewModel();
            var ds = trigger.GetList();
            gridControl1.DataSource = ds.Tables[0];
        }
        
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TriggerFormViewModel trigger = new TriggerFormViewModel();
            var ds = trigger.GetList();
            gridControl1.DataSource = ds.Tables[0];
        }
        
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTriggerForm form = new AddTriggerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 列表显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceForm form = new ServiceForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void 添加ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddServiceForm form = new AddServiceForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void 执行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取选中的行，将背景色改成绿色，表示待执行，红色表示黄色表示正在执行
            var list = this.gridView1.GetSelectedRows();
        }
    }
}
