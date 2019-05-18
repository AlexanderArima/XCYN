using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace XCYN.Common
{
    /// <summary>
    /// ConvertHelper 的摘要说明
    /// </summary>
    public class ConvertHelper
    {
        public static string GetString(object obj)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetDecimalString(object obj, int f)
        {
            if (f < 0) f = 0;
            try
            {
                return ToDecimal(obj).ToString("N" + f.ToString());
            }
            catch
            {
                return "0.00";
            }
        }

        public static string GetDoubleString(object obj)
        {
            try
            {
                return ToDouble(obj).ToString("#,##0.00");
            }
            catch
            {
                return "0.00";
            }
        }

        public static string GetDateTime(object obj, string format)
        {
            try
            {
                return Convert.ToDateTime(obj).ToString(format);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static decimal ToDecimal(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal ToDecimal(object obj, decimal err)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return err;
            }
        }

        public static double ToDouble(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static int ToInt(object obj)
        {
            return ToInt(obj, 0);
        }

        public static int ToInt(object obj, int err)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return err;
            }
        }

        public static short ToShort(object obj)
        {
            return ToShort(obj, 0);
        }

        public static short ToShort(object obj, short err)
        {
            try
            {
                return Convert.ToInt16(obj);
            }
            catch
            {
                return err;
            }
        }

        public static bool ToBoolean(object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }

        public static DateTime ToDateTime(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        internal static DateTime ToDateTime(string str, DateTime err)
        {
            try
            {
                return Convert.ToDateTime(str);
            }
            catch
            {
                return err;
            }
        }

        /// <summary>
        /// 将DataTable转成DataRow
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataRow[] ToDataRow(DataTable table)
        {
            DataRow[] row = new DataRow[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row[i] = table.Rows[i];
            }
            return row;
        }

        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中
            return tmp;
        }

        /// <summary>
        /// 将 20170802102816 这样的日期时间格式化，加上分隔符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetTime(string str)
        {
            return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static byte[] Bitmap2Byte(Bitmap bitmap)
        {
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                byte[] array = new byte[memoryStream.Length];
                memoryStream.Seek(0L, SeekOrigin.Begin);
                memoryStream.Read(array, 0, Convert.ToInt32(memoryStream.Length));
                result = array;
            }
            return result;
        }

        /*
        /// <summary>
        /// 返回字符串指定长度
        ///  例如：Substring("10000",3) => 100
        /// </summary>
        public static string SubString(string str, int len) {
            if (string.IsNullOrEmpty(str) || str.Length <= len)
                return str;
            return str.Substring(0, len);
        }
         * */
    }

    public static class ClassEx
    {
        /// <summary>
        /// 返回字符串指定长度
        ///  例如：StringTruncation("10000",3) => 100
        /// </summary>
        public static string StringTruncation(this string str, int len)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= len)
                return str;
            return str.Substring(0, len);
        }

        /// <summary>
        /// 过滤字符串中的特殊字符
        /// </summary>
        public static string StringFilter(this string str)
        {
            //string result = string.Empty;
            //if (string.IsNullOrWhiteSpace(str))
            //    return result;
            //string temp = str.Trim();
            //for (int i = 0; i < temp.Length; i++) {
            //    char c = temp[i];
            //    if (!char.IsLetterOrDigit(c))
            //        result += Strings.StrConv(c.ToString(), VbStrConv.Wide);
            //    else
            //        result += c.ToString();
            //}
            //return result;
            return StringFilter(str, '%', '\'', '\"');
        }

        public static string StringFilter(this string str, params char[] chars)
        {
            string result = str;
            //foreach (char item in chars) {
            //    result = result.Replace(item, ' ');
            //}
            return result.Trim().Replace('%', '％').Replace('\'', '‘').Replace('\"', '“').Replace("--", "——");
        }
    }
}