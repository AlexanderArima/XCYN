using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.AutoScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void GetRealScreenSize()
        {
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;
            var img = new Bitmap(width * 3, height * 3);
            var grp = Graphics.FromImage(img);
            grp.CopyFromScreen(new Point(0, 0), new Point(0, 0), img.Size);
            grp.Dispose();
            GC.Collect();
            for (int i = 0; i < i * 3 - 1; i++)
            {
                Color pointColor = img.GetPixel(i, 0);
            }
        }
    }
}
