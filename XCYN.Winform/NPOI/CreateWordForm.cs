using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
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

namespace XCYN.Winform.NPOI
{
    public partial class CreateWordForm : Form
    {
        public CreateWordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var title = this.textBox1.Text;
            var content = this.textBox2.Text;

            // 创建对象
            XWPFDocument doc = new XWPFDocument();
            CT_SectPr m_sectPr = new CT_SectPr();

            // 设置页面的宽和高
            m_sectPr.pgSz.w = (ulong)11906;
            m_sectPr.pgSz.h = (ulong)16838;
            doc.Document.body.sectPr = m_sectPr;

            // 设置页面边距
            m_sectPr.pgMar.left = (ulong)800;   // 左边距
            m_sectPr.pgMar.right = (ulong)800;   // 右边距
            m_sectPr.pgMar.top = "850";   // 上边距
            m_sectPr.pgMar.bottom = "850";   // 下边距

            // 处理段落
            XWPFParagraph p1 = doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.CENTER;
            p1.VerticalAlignment = TextAlignment.CENTER;
            var run1 = p1.CreateRun();
            run1.SetText("关于同意 XXX");
            run1.FontSize = 20;
            run1.IsBold = true;

            XWPFParagraph p2 = doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.CENTER;
            p2.VerticalAlignment = TextAlignment.CENTER;
            var run2 = p2.CreateRun();
            run2.SetText("申办YYY证件的函");
            run2.FontSize = 20;
            run2.IsBold = true;

            XWPFParagraph p3 = doc.CreateParagraph();
            p3.Alignment = ParagraphAlignment.LEFT;
            p3.VerticalAlignment = TextAlignment.CENTER;
            var run3 = p3.CreateRun();
            run3.SetText("武汉市公安局 出入境管理部门：");
            run3.FontSize = 12;
            run3.AddBreak(BreakType.TEXTWRAPPING);

            XWPFParagraph p4 = doc.CreateParagraph();
            p4.Alignment = ParagraphAlignment.LEFT;
            p4.VerticalAlignment = TextAlignment.CENTER;
            var run4 = p3.CreateRun();
            run4.SetText("                                   李四 同志（身份证号码：110000198009091234）");
            run4.FontSize = 12;
            run4.AddBreak(BreakType.TEXTWRAPPING);

            XWPFParagraph p5 = doc.CreateParagraph();
            p5.Alignment = ParagraphAlignment.LEFT;
            p5.VerticalAlignment = TextAlignment.CENTER;
            var run5 = p3.CreateRun();
            run5.SetText("系                 AAA有限责任公司              （填写单位名称）");
            run5.FontSize = 12;
            run5.AddBreak(BreakType.TEXTWRAPPING);

            using (FileStream out1 = new FileStream("simple.docx", FileMode.Create))
            {
                doc.Write(out1);
            }
        }
    }
}
