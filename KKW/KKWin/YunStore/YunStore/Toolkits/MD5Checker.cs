using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YunStore.Toolkits
{
    public class MD5Checker
    {
        /// <summary>
        /// 获取文件md5值。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMD5ByMD5CryptoService(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException(string.Format("<{0}>, 不存在", path));
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] buffer = md5Provider.ComputeHash(fs);
            string result = BitConverter.ToString(buffer);
            result = result.Replace("-", "");
            md5Provider.Clear();
            fs.Close();
            return result;
        }

        public static string GetMD5ByFileContent(string content)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] cont = Encoding.UTF8.GetBytes(content);
            byte[] hs = md5.ComputeHash(cont);

            string result = BitConverter.ToString(hs);
            result = result.Replace("-", "");
            md5.Clear();

            return result;
        }

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
