using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.ReaderCard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var flag = CardHelper.Fun01(8);
            if (flag)
            {
                this.textBox1.Text = "连接成功";
            }
            else
            {
                this.textBox1.Text = "连接失败";
            }
        }
    }
}
