using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for eBaySysCategory
/// </summary>
public class eBaySysCategory
{
    public eBaySysCategory()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// sys 在ebay上的类值
    /// </summary>
    /// <returns></returns>
    public static string GetSysOnEbayCategory(string title)
    {
        if (title.ToLower().IndexOf("i3") > -1)
        {
            return "4853210017";
        }
        else if (title.ToLower().IndexOf("i5") > -1)
        {
            return "4853211017";
        }
        else if (title.ToLower().IndexOf("i7") > -1)
        {
            return "4853212017";
        }
        else if (title.ToLower().IndexOf("celeron") > -1)
        {
            return "4853208017";
        }
        else if (title.ToLower().IndexOf("pentium") > -1)
        {
            return "4853209017";
        }
        else if (title.ToLower().IndexOf("athlon") > -1)
        {
            return "4853213017";
        }
        else if (title.ToLower().IndexOf("fx") > -1)
        {
            return "4853214017";
        }
        else if (title.ToLower().IndexOf("fm1") > -1)
        {
            return "4853215017";
        }
        else if (title.ToLower().IndexOf("fm2") > -1)
        {
            return "4853216017";
        }
        return string.Empty;
    }
}