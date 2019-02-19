using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace LU.BLL
{
    public class Util
    {
        public static int GetImgSku(int sku, int? otherSku)
        {
            if (otherSku.HasValue)
                return otherSku.Value > 0 && otherSku.Value <= 999999 ? otherSku.Value : sku;
            return sku;
        }

        public static string eBayFont(bool isEmpty = false)
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

        public static string FilterName(string str)
        {
            return string.IsNullOrEmpty(str) ? "" : str.Replace("$", "-")
                .Replace("#", "-")
                .Replace("&", "-")
                .Replace("~", "-")
                .Replace("^", "-")
                .Replace(" ", "")
                .Replace("\"", "-")
                .Replace(" ", "-")
                .Replace("/", "-")
                .Replace("\\", "-")
                .Replace(".", "-")
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
                .Replace("+", "-");
        }

        public static string PartUrl(int sku, string mfp)
        {
            return string.Concat(Config.Host, "computer/", (string.IsNullOrEmpty(FilterName(mfp)) ? "LU-" + sku : FilterName(mfp)), "/", sku, ".html");
        }

        public static string SysUrl(int sku)
        {
            return string.Concat(Config.Host, "computer/system/", sku, ".html");
        }

        public static string CateUrl(int cateId, string cateNameLogogram, int cateType)
        {
            return CateUrl(cateId, cateNameLogogram, (Model.Enums.CateType)Enum.Parse(typeof(Model.Enums.CateType), cateType.ToString()));
        }

        public static string CateUrl(int cateId, string cateNameLogogram, LU.Model.Enums.CateType cateType)
        {
            if (cateType == Model.Enums.CateType.System)
            {
                return string.Concat(Config.Host, "list_sys.aspx?cid=", cateId);
            }
            else
            {
                if (!string.IsNullOrEmpty(cateNameLogogram))
                {
                    return string.Concat(Config.Host, "computers/", cateNameLogogram, ".html");
                }
                else
                {
                    return string.Concat(Config.Host, "list_part.aspx?cid=", cateId);
                }
            }
        }

        public static string BrandUrl(string brand)
        {
            if (string.IsNullOrEmpty(brand.Trim()))
                return Config.Host;
            else
                return string.Concat(Config.Host, "brand/", FilterName(brand.Trim()), ".html");
        }

        public static string eBayUrl(string itemid)
        {
            return string.Concat(LU.BLL.Config.eBayUrl, itemid);
        }


        public static string GetStringSafeFromString(System.Web.UI.Page page, string key)
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
}
