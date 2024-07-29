using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.WinformX64.OCR
{
    public partial class PictureOCRForm : Form
    {
        /// <summary>
        /// 选中图片的路径
        /// </summary>
        private string _pictureFullPath;

        public PictureOCRForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择图片.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                // 选中图片
                _pictureFullPath = dialog.FileName;
                Image image = Image.FromFile(_pictureFullPath);
                pictureBox1.Image = image;
            }
            else
            {
                MessageBox.Show("您本次没有选择任何图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 开始识别.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            FullOcrModel model = LocalFullModels.ChineseV3;
            using (PaddleOcrAll all = new PaddleOcrAll(model, PaddleDevice.Mkldnn())
            {
                AllowRotateDetection = true,    // 允许有角度的文字
                Enable180Classification = false    // 允许旋转角度180度的文字
            })
            {
                using(Mat src = Cv2.ImRead(this._pictureFullPath))
                {
                    // 返回OCR的结果
                    PaddleOcrResult result = all.Run(src);
                    if (string.IsNullOrEmpty(result.Text))
                    {
                        MessageBox.Show("未能识别出文字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.textBox1.Text = result.Text;
                    }
                    else
                    {
                        this.textBox1.Text = result.Text;
                    }
                }
            };
        }
    }
}
