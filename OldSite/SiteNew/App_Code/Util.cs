using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    public Util()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }

    public static string GetStringSafeFromString(Page page, string key)
    {
        string value = page.Request.Form[key];
        return (value == null) ? string.Empty : value.Trim();
    }

    // 从 querystring 集合中安全的取得一个 string. (总是不会有 null，所以叫做 'Safe')
    public static string GetStringSafeFromQueryString(Page page, string key)
    {
        string value = page.Request.QueryString[key];
        return (value == null) ? string.Empty : value.Trim();
    }

    // 在上述基础上，实现几个常用类型的获取方法。
    public static int GetInt32SafeFromQueryString(Page page, string key, int defaultValue)
    {
        string value = GetStringSafeFromQueryString(page, key);
        int i = defaultValue;
        try
        {
            i = int.Parse(value);
        }
        catch { }
        return i;
    }
    public static long GetInt64SafeFromQueryString(Page page, string key, long defaultValue)
    {
        string value = GetStringSafeFromQueryString(page, key);
        long i = defaultValue;
        try
        {
            i = long.Parse(value);
        }
        catch { }
        return i;
    }
    // 在上述基础上，实现几个常用类型的获取方法。
    public static int GetInt32SafeFromString(Page page, string key, int defaultValue)
    {
        string value = GetStringSafeFromString(page, key);
        int i = defaultValue;
        try
        {
            i = int.Parse(value);
        }
        catch { }
        return i;
    }

    // double 的实现
    public static double GetDoubleSafeFromQueryString(Page page,
      string key, double defaultValue)
    {
        string value = GetStringSafeFromQueryString(page, key);
        double d = defaultValue;
        try
        {
            d = double.Parse(value);
        }
        catch { }
        return d;
    }
    // double 的实现
    public static double GetDoubleSafeFromString(Page page,
      string key, double defaultValue)
    {
        string value = GetStringSafeFromString(page, key);
        double d = defaultValue;
        try
        {
            d = double.Parse(value);
        }
        catch { }
        return d;
    }
    //
    public static decimal GetDecimalSafeFromString(Page page, string key, decimal defaultValue)
    {
        string value = GetStringSafeFromString(page, key);
        decimal i = defaultValue;
        try
        {
            i = decimal.Parse(value);
        }
        catch { }
        return i;
    }
    public static decimal GetDecimalSafeFromQueryString(Page page, string key, decimal defaultValue)
    {
        string value = GetStringSafeFromQueryString(page, key);
        decimal i = defaultValue;
        try
        {
            i = decimal.Parse(value);
        }
        catch { }
        return i;
    }
}