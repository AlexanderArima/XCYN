using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform
{
    public partial class HtmlTagRemoverForm : Form
    {
        public HtmlTagRemoverForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 业务逻辑：遍历整个字符串，首先获取第一个<的下标，再往后找下一个>标签的下标，
            // 删除这两个标签之间的内容后，再从头开始这个过程
            var source = this.textBox1.Text;
            while (true)
            {
                var start_index = source.IndexOf("<");
                if (start_index == -1)
                {
                    // 结束循环的条件
                    break;
                }

                var end_index = source.IndexOf(">", start_index);
                if (end_index == -1)
                {
                    // 结束循环的条件
                    break;
                }

                source = source.Remove(start_index, end_index - start_index + 1);
            }

            this.textBox2.Text = source;
        }
    }
}
