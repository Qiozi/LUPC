﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KKWStore.Helper
{
    public class CharacterHelper
    {
        /// <summary>
        /// 转全角的函数(SBC case) 
        /// 任意字符串 
        /// 全角字符串
        /// 全角空格为12288,半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TOSBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288; continue;
                }
                if (c[i] < 127) c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 转半角的函数(DBC case) 
        /// 任意字符串 
        /// 全角字符串
        /// 全角空格为12288,半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32; continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
    }
}
