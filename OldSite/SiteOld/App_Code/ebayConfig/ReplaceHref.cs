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
        if (string.IsNullOrEmpty(sourceText))
            return "";
        return sourceText.Replace("<a ", "<luComputer ")
            .Replace("</a>", "</luComputer>")
            .Replace("<A ", "<luComputer ")
            .Replace("</A>", "</luComputer>")
            .Replace(" onclick", " lucomputer")
            .Replace("window.open", "lucomputer")
            .Replace("http://www.msi.com/", "http://www.lucomputers.com/")
            .Replace("https://www.msi.com/", "http://www.lucomputers.com/");
            //.Replace("width=\"565\"", "")
            //.Replace("width=\"575\"", ""); 

    }
}