using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.ResumeDownload
{
    public partial class InputURLForm : Form
    {
        /// <summary>
        /// 获取下载地址.
        /// </summary>
        public Action<string> GetURLAction { get; set; }

        public InputURLForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("请输入下载地址", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(this.GetURLAction != null)
            {
                this.GetURLAction.Invoke(this.textBox1.Text);
                this.Close();
            }
        }
    }
}
