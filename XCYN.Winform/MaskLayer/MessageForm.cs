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
    public partial class MessageForm : Form
    {

        public Action HideMaskAction { get; set; }

        public MessageForm()
        {
            InitializeComponent();
        }

        private void MessageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.HideMaskAction();
        }
    }
}
