using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Toolkit
{
    public class DescriptionFilter
    {
        public static string Done(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            if (str.IndexOf("width=\"565\"") > 1)
                str = str.Replace("width=\"565\"", "width='100%'");

            if (str.IndexOf("width=\"575\"") > 1)
                str = str.Replace("width=\"575\"", "width='100%'");

            if (str.IndexOf("itemtype=\"http://schema.org/ProductFeature\"") > -1)
            {
                str = str.Replace("itemtype=\"http://schema.org/ProductFeature\"", "");
            }
            str = str.Replace("<head>", "")
                .Replace("</head>", "")
                .Replace("<body>", "")
                .Replace("</body>", "")
                .Replace("<html>", "")
                .Replace("</html>", "");
            return str;
        }
    }
}
