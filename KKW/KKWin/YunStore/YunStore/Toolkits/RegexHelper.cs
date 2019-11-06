using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YunStore.Toolkits
{
    public class RegexHelper
    {
        public static bool IsEmail(string text)
        {
            string pattern = @"^[\w-]+@[\w-]+\.(com|net|org|edu|mil|tv|biz|info)$";
            Regex rx = new Regex(pattern);
            return rx.Match(text).Success;
        }

        public static bool IsMobilePhone(string text)
        {
            string pattern = @"(86)*0*1\d{10}";
            Regex rx = new Regex(pattern);
            return rx.Match(text).Success;
        }

        public static bool IsTelphone(string text)
        {
            string pattern = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)";
            Regex rx = new Regex(pattern);
            return rx.Match(text).Success;
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNumber(string text)
        {
            string pattern = "^[0-9]+$";
            Regex rx = new Regex(pattern);
            return rx.Match(text).Success;
        }

        /// <summary>
        /// 匹配 十进制小数
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsDecimal(string text)
        {
            string pattern = "^[0-9]+[.]?[0-9]+$";
            Regex rx = new Regex(pattern);
            return rx.Match(text).Success;
        }

        /// <summary>
        /// 匹配正负数字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNumberSign(string text)
        {
            string pattern = "^[+-]?[0-9]+$";
            Regex rx = new Regex(pattern);
            return rx.Match(text).Success;
        }

        public static bool IsDecimalSign(string text)
        {
            string pattern = "^[+-]?[0-9]+[.]?[0-9]+$";
            Regex rx = new Regex(pattern);
            return rx.Match(text).Success;
        }
    }
}
