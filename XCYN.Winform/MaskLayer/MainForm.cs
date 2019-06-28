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
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageForm form = new MessageForm();
            form.HideMaskAction += () => {
                //点击按钮关闭遮罩层
                this.HideMask();
            };
            this.ShowMask();
            if(form.ShowDialog() == DialogResult.OK)
            {
                //正常关闭
                this.HideMask();
                form.Dispose();
            }
        }

        private MaskForm _maskform;

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        private void ShowMask()
        {
            _maskform = new MaskForm(this.Location,this.Size);
            _maskform.Show();
        }

        /// <summary>
        /// 关闭遮罩层
        /// </summary>
        private void HideMask()
        {
            if(_maskform != null)
            {
                _maskform.Close();
            }
        }

    }
}
