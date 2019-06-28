using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.MaskLayer
{
    public partial class MaskForm : Form
    {
        public MaskForm(Point point, Size size)
        {
            InitializeComponent();
            this.Opacity = 0.5;
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            //位置和大小跟随主界面
            this.Location = point;
            this.Size = size;
        }

        private void MaskForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
