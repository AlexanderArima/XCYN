using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.Files
{
    /// <summary>
    /// 确定文件是不被占用.
    /// </summary>
    public partial class UseFileForm : Form
    {
        private string _directoryPath = @"";

        private string _filePath = @"";

        private List<string> _list_file = new List<string>();

        public UseFileForm()
        {
            InitializeComponent();
        }

        private bool _timer1_flag = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if(_timer1_flag == true)
                {
                    return;
                }

                _timer1_flag = true;

                 // 占用制定文件夹的所有文件.
                 var list = Directory.GetFiles(this._directoryPath);
                for (int i = 0; i < list.Count(); i++)
                {
                    var item = list.ElementAt(i);
                    if (_list_file.Contains(item))
                    {
                        continue;
                    }

                    var stream = File.Open(item, FileMode.Open, FileAccess.Read, FileShare.Read);
                    _list_file.Add(item);
                    this.textBox1.Text = this.textBox1.Text + string.Format("\r\n 已占用：{0}文件" , item);
                }
            }
            catch(Exception ex)
            {
                this.textBox1.Text = this.textBox1.Text + string.Format("\r\n 发生异常：{0}", ex);
            }
            finally
            {
                _timer1_flag = false;
            }
        }

        private bool _timer2_flag = false;

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_timer2_flag == true)
                {
                    return;
                }

                _timer2_flag = true;
                var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                this.textBox1.Text = this.textBox1.Text + string.Format("\r\n 已占用：{0}文件", _filePath);
            }
            catch (Exception ex)
            {
                this.textBox1.Text = this.textBox1.Text + string.Format("\r\n 发生异常：{0}", ex);
            }
        }
    }
}
