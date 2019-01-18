using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using System.IO;

public class ExcelHelper
{
    /// <summary>
    /// 读取 Excel 2003 文件的第一个 Sheet
    ///  成功： 返回一个 DataTable
    ///  失败： 返回 null
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static DataTable ReadExcel(string fileName)
    {
        //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
        //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
        using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            int columnMax = 0;
            while (rows.MoveNext())
            {
                IRow row = (HSSFRow)rows.Current;
                if (columnMax < row.LastCellNum)
                    columnMax = row.LastCellNum;
            }

            if (columnMax > 0)
            {
                DataTable dt = new DataTable();
                for (int j = 0; j < columnMax; j++)
                {
                    //dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                    dt.Columns.Add("A" + j.ToString());
                }

                rows.Reset();
                while (rows.MoveNext())
                {
                    IRow row = (HSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        ICell cell = row.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            return null;
        }
    }

    /// <summary>
    /// 不带格式的导出
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="dt"></param>
    public static void ToExcel(string fileName, DataTable dt)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();

        ////create a entry of DocumentSummaryInformation
        DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        dsi.Company = "NPOI Team";
        hssfworkbook.DocumentSummaryInformation = dsi;

        ////create a entry of SummaryInformation
        SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        si.Subject = "NPOI SDK Example";
        hssfworkbook.SummaryInformation = si;

        ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
        IRow row;

        //sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
        row = sheet1.CreateRow(0);
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            row = sheet1.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
            }
        }
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            sheet1.AutoSizeColumn(j);
        }

        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            //Write the stream data of workbook to the root directory
            //MemoryStream file = new MemoryStream();
            hssfworkbook.Write(fs);
        }
    }

    /// <summary>
    /// Excel导入成Datable
    /// </summary>
    /// <param name="file">导入路径(包含文件名与扩展名)</param>
    /// <returns></returns>
    public static DataTable ExcelToTable(string file)
    {
        DataTable dt = new DataTable();
        IWorkbook workbook;
        string fileExt = Path.GetExtension(file).ToLower();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
            if (workbook == null) { return null; }
            ISheet sheet = workbook.GetSheetAt(0);
            //表头  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueType(header.GetCell(i));
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                }
                else
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    if (sheet.GetRow(i) == null)
                    {
                        //如果整行为空，则跳出这次循环
                        goto JumpFor;
                    }
                    ICell cell = sheet.GetRow(i).GetCell(j);
                    if (cell == null)
                    {
                        //如果cell中值为null就必须判断一下否则会报错
                        cell = sheet.GetRow(i).CreateCell(100);
                    }
                    if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                    {
                        dr[j] = cell.DateCellValue.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        //dr[j] = cell.ToString();
                        dr[j] = GetValueType(cell);
                    }
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
            JumpFor:
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    /// <summary>
    /// 获取单元格类型
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private static object GetValueType(ICell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank: //BLANK:  
                return null;
            case CellType.Boolean: //BOOLEAN:  
                return cell.BooleanCellValue;
            case CellType.Numeric: //NUMERIC:  
                return cell.NumericCellValue;
            case CellType.String: //STRING:  
                return cell.StringCellValue;
            case CellType.Error: //ERROR:  
                return cell.ErrorCellValue;
            case CellType.Formula: //FORMULA:  
            default:
                return "=" + cell.CellFormula;
        }
    }
}
