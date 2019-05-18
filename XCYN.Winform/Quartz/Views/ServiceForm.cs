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
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            ServiceFormViewModel service = new ServiceFormViewModel();
            var ds = service.GetList();
            gridControl1.DataSource = ds.Tables[0];
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddServiceForm form = new AddServiceForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = this.gridView1.GetSelectedRows();

            if (list.Length == 0)
            {
                MessageBox.Show("请勾选一条数据做修改");
            }
            else if(list.Length == 1)
            {
                var dt = gridControl1.DataSource as DataTable;
                var row = dt.Rows[list[0]];
                int id = Convert.ToInt32(row["ID"]);
                EditServiceForm form = new EditServiceForm(id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    form.Dispose();
                }
            }
            else
            {
                MessageBox.Show("只能勾选一条数据做修改");
            }
        }

        /// <summary>
        /// 点击某一行时勾选这一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //选中这一行
            if (gridView1.IsRowSelected(e.RowHandle))
            {
                gridView1.UnselectRow(e.RowHandle);
            }
            else
            {
                gridView1.SelectRow(e.RowHandle);
            }
        }

        private void 刷新FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceFormViewModel service = new ServiceFormViewModel();
            var ds = service.GetList();
            gridControl1.DataSource = ds.Tables[0];
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定删除吗?","重要提示",MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else
            {
                var list = this.gridView1.GetSelectedRows();
                if (list.Length == 0)
                {
                    MessageBox.Show("请至少勾选一条数据做删除");
                }
                else
                {
                    //保存id的数组
                    List<int> list_id = new List<int>();
                    var dt = gridControl1.DataSource as DataTable;
                    for (int i = 0; i < list.Count(); i++)
                    {
                        var row = dt.Rows[list[i]];
                        int id = Convert.ToInt32(row["ID"]);
                        list_id.Add(id);
                    }
                    ServiceFormViewModel service = new ServiceFormViewModel();
                    if (service.Delete(list_id))
                    {
                        if (MessageBox.Show("删除成功!", "提示") == DialogResult.OK)
                        {
                            var ds = service.GetList();
                            gridControl1.DataSource = ds.Tables[0];
                        }
                    }
                }
            }
        }
    }
}
