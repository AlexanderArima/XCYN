using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaperApplication.Common;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace XCYN.Winform.Spire
{
    public partial class InsertImageForm : Form
    {
        public InsertImageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 图片在文字的前面.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Create a Document instance
            Document document = new Document();

            //Load a sample Word document
            document.LoadFromFile(PathHelper.ApplicationPath + "Spire\\Doc\\1.doc");

            //Get the first section 
            Section section = document.Sections[0];

            //Get two specified paragraphs
            Paragraph para1 = section.Paragraphs[1];
            Paragraph para2 = section.Paragraphs[1];

            //Insert images in the specified paragraphs
            DocPicture Pic1 = para1.AppendPicture(Image.FromFile(PathHelper.ApplicationPath + @"Spire\\Doc\\2.jpg"));
            Pic1.Width = 150;
            Pic1.Height = 100;
            // DocPicture Pic2 = para2.AppendPicture(Image.FromFile(@"Doc\\2.jpg"));

            //Set wrapping styles to Square and Inline respectively
            Pic1.TextWrappingStyle = TextWrappingStyle.Square;
            // Pic2.TextWrappingStyle = TextWrappingStyle.Inline;

            //Save the document to file
            document.SaveToFile(string.Format("{0}Spire\\Doc\\{1}.docx", PathHelper.ApplicationPath, DateTime.Now.ToString("yyyyMMddHHmmss")), FileFormat.Docx);
        }

        /// <summary>
        /// 图片跟在文字的后面.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //Create a Document instance
            Document document = new Document();

            //Load a sample Word document
            document.LoadFromFile(PathHelper.ApplicationPath + @"Spire\\Doc\\1.doc");

            //Get the first section 
            Section section = document.Sections[0];

            //Get two specified paragraphs
            Paragraph para1 = section.Paragraphs[1];
            Paragraph para2 = section.Paragraphs[1];

            //Insert images in the specified paragraphs
            // DocPicture Pic1 = para1.AppendPicture(Image.FromFile(@"Doc\\2.jpg"));
            DocPicture Pic2 = para2.AppendPicture(Image.FromFile(PathHelper.ApplicationPath + @"Spire\\Doc\\2.jpg"));
            Pic2.Width = 150;
            Pic2.Height = 100;
            //Set wrapping styles to Square and Inline respectively
            // Pic1.TextWrappingStyle = TextWrappingStyle.Square;
            Pic2.TextWrappingStyle = TextWrappingStyle.Inline;

            //Save the document to file
            document.SaveToFile(string.Format("{0}Spire\\Doc\\{1}.docx", PathHelper.ApplicationPath, DateTime.Now.ToString("yyyyMMddHHmmss")), FileFormat.Docx);
        }

        /// <summary>
        /// 插入的图片悬浮在文字的下面，可自由控制其位置.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //Create a Document instance
            Document document = new Document();

            //Load a sample Word document
            document.LoadFromFile(PathHelper.ApplicationPath + @"Spire\\Doc\\1.doc");

            //Get the first section 
            Section section = document.Sections[0];

            //Load an image and insert it to the document
            DocPicture picture = section.Paragraphs[1].AppendPicture(Image.FromFile(PathHelper.ApplicationPath + @"Spire\\Doc\\2.jpg"));
            picture.Width = 150;
            picture.Height = 100;

            //Set the position of the image 
            picture.HorizontalPosition = 90.0F;
            picture.VerticalPosition = 50.0F;

            //Set the size of the image
            picture.Width = 150;
            picture.Height = 150;

            //Set the wrapping style to Behind
            picture.TextWrappingStyle = TextWrappingStyle.Behind;

            // Save the document to file
            document.SaveToFile(string.Format("{0}Spire\\Doc\\{1}.docx", PathHelper.ApplicationPath, DateTime.Now.ToString("yyyyMMddHHmmss")), FileFormat.Docx);
        }
    }
}
