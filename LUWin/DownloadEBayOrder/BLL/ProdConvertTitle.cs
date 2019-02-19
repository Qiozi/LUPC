using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class ProdConvertTitle
    {
        public static string ChangeName(string title)
        {
            return string.IsNullOrEmpty(title) ? "" : title.Trim()
                .Replace("\"", "-")
                .Replace(" ", "-")
                .Replace("/", "-")
                .Replace("\\", "-")
                .Replace(".", "-")
                .Replace("#", "-")
                .Replace("&", "-")
                .Replace("'", "-")
                .Replace("@", "-")
                .Replace("“", "-")
                .Replace("?", "-")
                .Replace("\\\"", "")
                .Replace("*", "-")
                .Replace(",", "-")
                .Replace("(", "-")
                .Replace(")", "-")
                .Replace("[", "-")
                .Replace("]", "-")
                .Replace(":", "-")
                .Replace("\t", "-")
                .Replace("\n", "-")
                .Replace("\r", "-")
                .Replace("+","-");
        }
    }
}
