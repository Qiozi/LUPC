using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReplaceHref
/// </summary>
public class ReplaceHref
{
    public ReplaceHref()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string ReplaceHrefText(string sourceText)
    {
        // return sourceText;
        if (string.IsNullOrEmpty(sourceText))
            return "";
        return sourceText.Replace("<a ", "<luComputer ")
            .Replace("</a>", "</luComputer>")
            .Replace("<A ", "<luComputer ")
            .Replace("</A>", "</luComputer>")
            .Replace(" onclick", " lucomputer")
            .Replace("window.open", "lucomputer")
            .Replace("javascript", "lucomputer")
            .Replace("<button ", "<lucomputer ")
            .Replace("</button>", "</lucomputer>")
            .Replace("<BUTTON ", "<lucomputer ")
            .Replace("</BUTTON>", "</lucomputer>")
            .Replace("<script", "<lucomputer")
            .Replace("</script>", "</lucomputer>")
            .Replace("<SCRIPT", "<lucomputer")
            .Replace("</SCRIPT>", "</lucomputer>")
            .Replace("onmousedown", "LUComputer")
            .Replace("onmouseup", "LUComputer")
            .Replace("href", "lucomputer")
            .Replace("http://", "")
            .Replace("Http://", "")
            .Replace("https://", "")
        // .Replace("http://www.msi.com/", "http://www.lucomputers.com/")
        // .Replace("https://www.msi.com/", "http://www.lucomputers.com/")
        .Replace("width=\"565\"", "")
        .Replace("width=\"575\"", "");

    }

    /// <summary>
    /// 警告的关键字
    /// </summary>
    /// <returns></returns>
    public static List<string> Keys()
    {
        var result = new List<string>();
        result.Add("http");
        result.Add("script");
        result.Add("button");
        result.Add("onload");
        result.Add("onunload");
        result.Add("onchange");
        result.Add("onsubmit");
        result.Add("onreset");
        result.Add("onselect");
        result.Add("onblur");
        result.Add("onfocus");
        result.Add("onabort");
        result.Add("onkeydown");
        result.Add("onkeypress");
        result.Add("onkeyup");
        result.Add("onclick");
        result.Add("ondblclick");
        result.Add("onmousedown");
        result.Add("onmousemove");
        result.Add("onmouseout");
        result.Add("onmouseover");
        result.Add("onmouseup");

        return result;
    }

    /// <summary>
    /// 需要排除的
    /// </summary>
    /// <returns></returns>
    public static List<string> KeysNoMatch()
    {
        var result = new List<string>();
        result.Add("https://www.lucomputers.com");
        result.Add("https://cgi.ebay.ca");
        result.Add("description");
        return result;
    }
    /// <summary>
    /// 是否有警告的关键字， 要求全部
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool HaveWarnKeyword(string str)
    {
        var keys = Keys();
        var noMatch = KeysNoMatch();
        str = str.ToLower();
        foreach (var key in noMatch)
        {
            str = str.Replace(key, "");
        }
        //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\workspaces\\aaa.txt"))
        //{
        //    sw.Write(str);
        //    sw.Close();
        //    sw.Dispose();
        //}
        foreach (var key in keys)
        {
            if (str.IndexOf(key) > -1)
                return true;
        }

        return false;
    }

    public static string eBayFont(bool isEmpty)
    {
        if (isEmpty)
        {
            return @"
            <strong>
                <span style=""color:#ccc"">e</span><span style=""color:#ccc"">B</span><span style=""color:#ccc;"">a</span><span style=""color:#ccc;"">y.ca</span>
            </strong>";
        }
        else
            return @"
            <strong>
                <span style=""color:red"">e</span><span style=""color:blue"">B</span><span style=""color:#FF742E;"">a</span><span style=""color:green;"">y</span>.ca
            </strong>";
    }
}