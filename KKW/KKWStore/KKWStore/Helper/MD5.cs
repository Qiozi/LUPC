using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace KKWStore.Helper
{
    public class MD5
    {
        public static string Encode(string str)
        {
            string mds = "";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5String = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] b = md5.ComputeHash(md5String);

            mds = BitConverter.ToString(b);
            return mds;
        }
    }
}
