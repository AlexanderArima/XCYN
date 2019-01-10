using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace XCYN.Common
{
    /// <summary>
    ///     打印，打印预览 
    /// </summary>
    public class PrintHelper
    {
        //以下用户可自定义
        //当前要打印文本的字体及字号
        private const int HeadHeight = 40;
        private static readonly Font TableFont = new Font("Verdana", 10, FontStyle.Regular);
        private readonly SolidBrush _drawBrush = new SolidBrush(Color.Black);
        //表头字体
        private readonly Font _headFont = new Font("Verdana", 20, FontStyle.Bold);
        //表头文字
        private readonly int _yUnit = TableFont.Height * 2;
        public int TotalNum = 0;
        //以下为模块内部使用
        private DataRow _dataRow;
        private DataTable _dataTable;
        private int _firstPrintRecordNumber;
        private string _headText = string.Empty;
        private int _pBottom;
        private int _pHeigh;
        //当前要所要打印的记录行数,由计算得到
        private int _pageLeft;
        private int _pRight;
        private int _pageTop;
        private int _pWidth;
        private int _pageRecordNumber;
        private PrintDocument _printDocument;
        private PageSetupDialog _pageSetupDialog;
        private int _printRecordComplete;
        //每页打印的记录条数
        private int _printRecordNumber;
        private int _printingPageNumber = 1;
        //第一页打印的记录条数
        //与列名无关的统计数据行的类目数（如，总计，小计......）
        private int _totalPage;
        private int[] _xUnit;

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="dt">要打印的DataTable</param>
        /// <param name="title">打印文件的标题</param>
        public void Print(DataTable dt, string title)
        {
            try
            {
                CreatePrintDocument(dt, title).Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印错误，请检查打印设置！");
            }
        }

        /// <summary>
        ///     打印预览
        /// </summary>
        /// <param name="dt">要打印的DataTable</param>
        /// <param name="title">打印文件的标题</param>
        public void PrintPriview(DataTable dt, string title)
        {
            try
            {
                var printPriview = new PrintPreviewDialog
                {
                    Document = CreatePrintDocument(dt, title),
                    WindowState = FormWindowState.Maximized
                };
                printPriview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印错误，请检查打印设置！");
            }
        }
        public void PrintSetting()
        {
            try
            {
                _pageSetupDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印错误，请检查打印设置！");
            }
        }
        /// <summary>
        ///     创建打印文件
        /// </summary>
        private PrintDocument CreatePrintDocument(DataTable dt, string title)
        {
            _dataTable = dt;
            _headText = title;

            var pageSetup = new PageSetupDialog();

            _printDocument = new PrintDocument { DefaultPageSettings = pageSetup.PageSettings };
            _printDocument.DefaultPageSettings.Landscape = true; //设置打印横向还是纵向
            //PLeft = 30; //DataTablePrinter.DefaultPageSettings.Margins.Left;
            _pageTop = _printDocument.DefaultPageSettings.Margins.Top;
            //PRight = DataTablePrinter.DefaultPageSettings.Margins.Right;
            _pBottom = _printDocument.DefaultPageSettings.Margins.Bottom;
            _pWidth = _printDocument.DefaultPageSettings.Bounds.Width;
            _pHeigh = _printDocument.DefaultPageSettings.Bounds.Height;
            _xUnit = new int[_dataTable.Columns.Count];
            _printRecordNumber = Convert.ToInt32((_pHeigh - _pageTop - _pBottom - _yUnit) / _yUnit);
            _firstPrintRecordNumber = Convert.ToInt32((_pHeigh - _pageTop - _pBottom - HeadHeight - _yUnit) / _yUnit);

            if (_dataTable.Rows.Count > _printRecordNumber)
            {
                if ((_dataTable.Rows.Count - _firstPrintRecordNumber) % _printRecordNumber == 0)
                {
                    _totalPage = (_dataTable.Rows.Count - _firstPrintRecordNumber) / _printRecordNumber + 1;
                }
                else
                {
                    _totalPage = (_dataTable.Rows.Count - _firstPrintRecordNumber) / _printRecordNumber + 2;
                }
            }
            else
            {
                _totalPage = 1;
            }

            _printDocument.PrintPage += PrintDocumentPrintPage;
            _printDocument.DocumentName = _headText;

            return _printDocument;
        }

        /// <summary>
        ///     打印当前页
        /// </summary>
        private void PrintDocumentPrintPage(object sende, PrintPageEventArgs @event)
        {
            int tableWith = 0;
            string columnText;
            var font = new StringFormat { Alignment = StringAlignment.Center };
            var pen = new Pen(Brushes.Black, 1);//打印表格线格式

            #region 设置列宽

            foreach (DataRow dr in _dataTable.Rows)
            {
                for (int i = 0; i < _dataTable.Columns.Count; i++)
                {
                    int colwidth = Convert.ToInt32(@event.Graphics.MeasureString(dr[i].ToString().Trim(), TableFont).Width);
                    if (colwidth > _xUnit[i])
                    {
                        _xUnit[i] = colwidth;
                    }
                }
            }

            if (_printingPageNumber == 1)
            {
                for (int cols = 0; cols <= _dataTable.Columns.Count - 1; cols++)
                {
                    columnText = _dataTable.Columns[cols].ColumnName.Trim();
                    int colwidth = Convert.ToInt32(@event.Graphics.MeasureString(columnText, TableFont).Width);
                    if (colwidth > _xUnit[cols])
                    {
                        _xUnit[cols] = colwidth;
                    }
                }
            }
            for (int i = 0; i < _xUnit.Length; i++)
            {
                tableWith += _xUnit[i];
            }

            #endregion

            _pageLeft = (@event.PageBounds.Width - tableWith) / 2;
            int x = _pageLeft;
            int y = _pageTop;
            int stringY = _pageTop + (_yUnit - TableFont.Height) / 2;
            int rowOfTop = _pageTop;

            //第一页
            if (_printingPageNumber == 1)
            {
                //打印表头
                var arr = _headText.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 1)
                {
                    @event.Graphics.DrawString(arr[0],
                        _headFont,
                        _drawBrush,
                        new Point(@event.PageBounds.Width / 2, _pageTop), font);
                }
                //副标题
                var subtitleHeight = 0;
                for (int i = 1; i < arr.Length; i++)
                {
                    @event.Graphics.DrawString(arr[i],
                        new Font("Verdana", 12, FontStyle.Regular),
                        _drawBrush,
                        new Point(@event.PageBounds.Width / 2, _pageTop + _headFont.Height),
                        font);
                    subtitleHeight += new Font("Verdana", 12, FontStyle.Regular).Height;
                }

                //设置为第一页时行数 
                if (_dataTable.Rows.Count < _firstPrintRecordNumber)
                {
                    _pageRecordNumber = _dataTable.Rows.Count;
                }
                else
                {
                    _pageRecordNumber = _firstPrintRecordNumber;
                }

                rowOfTop = y = (_pageTop + _headFont.Height + subtitleHeight + 10);
                stringY = _pageTop + _headFont.Height + subtitleHeight + 10 + (_yUnit - TableFont.Height) / 2;
            }
            else
            {
                //计算,余下的记录条数是否还可以在一页打印,不满一页时为假
                if (_dataTable.Rows.Count - _printRecordComplete >= _printRecordNumber)
                {
                    _pageRecordNumber = _printRecordNumber;
                }
                else
                {
                    _pageRecordNumber = _dataTable.Rows.Count - _printRecordComplete;
                }
            }

            #region 列名

            if (_printingPageNumber == 1 || _pageRecordNumber > TotalNum) //最后一页只打印统计行时不打印列名
            {
                //得到datatable的所有列名
                for (int cols = 0; cols <= _dataTable.Columns.Count - 1; cols++)
                {
                    columnText = _dataTable.Columns[cols].ColumnName.Trim();

                    int colwidth = Convert.ToInt32(@event.Graphics.MeasureString(columnText, TableFont).Width);
                    @event.Graphics.DrawString(columnText, TableFont, _drawBrush, x, stringY);
                    x += _xUnit[cols];
                }
            }

            #endregion

            @event.Graphics.DrawLine(pen, _pageLeft, rowOfTop, x, rowOfTop);
            stringY += _yUnit;
            y += _yUnit;
            @event.Graphics.DrawLine(pen, _pageLeft, y, x, y);

            //当前页面已经打印的记录行数
            int printingLine = 0;
            while (printingLine < _pageRecordNumber)
            {
                x = _pageLeft;
                //确定要当前要打印的记录的行号
                _dataRow = _dataTable.Rows[_printRecordComplete];
                for (int cols = 0; cols <= _dataTable.Columns.Count - 1; cols++)
                {
                    @event.Graphics.DrawString(_dataRow[cols].ToString().Trim(), TableFont, _drawBrush, x, stringY);
                    x += _xUnit[cols];
                }
                stringY += _yUnit;
                y += _yUnit;
                @event.Graphics.DrawLine(pen, _pageLeft, y, x, y);

                printingLine += 1;
                _printRecordComplete += 1;
                if (_printRecordComplete >= _dataTable.Rows.Count)
                {
                    @event.HasMorePages = false;
                    _printRecordComplete = 0;
                }
            }

            @event.Graphics.DrawLine(pen, _pageLeft, rowOfTop, _pageLeft, y);
            x = _pageLeft;
            for (int cols = 0; cols < _dataTable.Columns.Count; cols++)
            {
                x += _xUnit[cols];
                @event.Graphics.DrawLine(pen, x, rowOfTop, x, y);
            }


            _printingPageNumber += 1;

            if (_printingPageNumber > _totalPage)
            {
                @event.HasMorePages = false;
                _printingPageNumber = 1;
                _printRecordComplete = 0;
            }
            else
            {
                @event.HasMorePages = true;
            }
        }
    }
}
