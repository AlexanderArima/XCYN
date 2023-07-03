using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.OpenCvSharp
{
    /// <summary>
    /// 视频捕捉.
    /// </summary>
    public partial class CapturingVideoForm : Form
    {
        public CapturingVideoForm()
        {
            InitializeComponent();
        }

        private void CapturingVideoForm_Load(object sender, EventArgs e)
        {
            VideoCapture capture = new VideoCapture(0);
            using (Window window = new Window("Camera"))
            using (Mat image = new Mat()) // Frame image buffer
            {
                // When the movie playback reaches end, Mat.data becomes NULL.
                while (true)
                {
                    capture.Read(image); // same as cvQueryFrame
                    if (image.Empty()) break;
                    window.ShowImage(image);
                    Cv2.WaitKey(30);
                }
            }
        }
    }
}
