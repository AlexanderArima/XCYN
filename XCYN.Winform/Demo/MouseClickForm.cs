using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Demo.Winform;

namespace XCYN.Winform.Demo
{
    public partial class MouseClickForm : Form
    {
        public MouseClickForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MouseClickChildForm form = new MouseClickChildForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Dispose();
            }
        }
    }
}
