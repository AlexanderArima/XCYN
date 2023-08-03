using DSkin.Forms;
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
using XCYN.Common;

namespace XCYN.Winform.ImageView
{
    public partial class MakeCircleForm : DSkinForm
    {
        public MakeCircleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 上传原图.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择要上传的图片文件";
            dialog.Filter = "图片文件(*.jpg,*.bmp,*.png)|*.jpg;*.bmp;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = dialog.FileName;
                    this.dSkinPictureBox1.Image = Image.FromFile(fileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Log4NetHelper.Error("MakeCircleForm：button1_Click出错：" + ex.Message, ex);
                    return;
                }
            }
        }

        /// <summary>
        /// 转换.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if(this.dSkinPictureBox1.Image == null)
            {
                return;
            }

            try
            {
                var img = this.dSkinPictureBox1.Image;
                Bitmap b = new Bitmap(img.Width, img.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.FillEllipse(new TextureBrush(img), 0, 0, img.Width, img.Height);
                }

                this.dSkinPictureBox2.Image = (Image)b;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log4NetHelper.Error("MakeCircleForm：button2_Click出错：" + ex.Message, ex);
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 导出图片.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                // 设置保存文件对话框的标题
                sfd.Title = "请选择要保存的文件路径";

                // 初始化保存目录，默认exe文件目录
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                // 设置保存文件的类型
                sfd.Filter = "图片文件(*.jpg,*.bmp,*.png)|*.jpg;*.bmp;*.png";
                sfd.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // 保存文件
                    this.dSkinPictureBox2.Image.Save(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log4NetHelper.Error("MakeCircleForm：button3_Click出错：" + ex.Message, ex);
                return;
            }
        }
    }
}
