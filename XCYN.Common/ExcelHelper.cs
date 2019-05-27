using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common
{
    public class ExcelHelper
    {
        public void Fun1()
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Workbooks.Add();
            excelApp.Visible = true;

            var myFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting1;

            excelApp.get_Range("A1", "B4").AutoFormat(myFormat, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelApp.get_Range("A1", "B4").AutoFormat(myFormat,Width:100);
        }

        public void Fun2(int a = 0,string s = "",Object o = null)
        {

        }
    }
}
