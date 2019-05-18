using System;
using System.Collections.Generic;
using System.Web;

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
            return ToDecimal(obj).ToString("#,##0.0##");
        }
        catch
        {
            return "0.00";
        }
    }

    public static decimal ToDecimal(object obj)
    {
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static decimal ToDecimal(object obj,decimal err)
    {
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch (Exception)
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
        catch (Exception)
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
        catch (Exception)
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
        catch (Exception)
        {
            return false;
        }
    }
}