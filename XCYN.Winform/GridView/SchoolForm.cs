using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.GridView
{
    public partial class SchoolForm : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SchoolForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SchoolForm_Load(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            var list = School.Query();
            this.dataGridView1.DataSource = list;
        }

        /// <summary>
        /// 排序规则
        /// </summary>
        private bool _BH_ASC = false;

        /// <summary>
        /// 点击列头部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var list = this.dataGridView1.DataSource as List<School>;
                if(list != null)
                {
                    if(_BH_ASC == false)
                    {
                        list = list.OrderBy(m => m.BH).ToList();
                    }
                    else
                    {
                        list = list.OrderByDescending(m => m.BH).ToList();
                    }
                    _BH_ASC = !_BH_ASC;
                    this.dataGridView1.DataSource = list;
                }
            }
        }
    }
}
