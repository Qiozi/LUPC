using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ViewDateFormat
/// </summary>
public class ViewDateFormat
{
	public ViewDateFormat()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 界面显示的日期格式
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string View(DateTime dt)
    {
        return string.Format("{0:t},{0:D}", dt);
    }
}