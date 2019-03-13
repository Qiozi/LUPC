using System;
using System.IO;
using System.Net.Http.Headers;

namespace Web
{
    public class CustomMultipartFormDataStreamProvider : System.Net.Http.MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path, string fileType)
            : base(path)
        {
        }

        //public Coolection<HttpContent> FileContents
        //{

        //}

        /// <summary>
        /// 重写文件名规则
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            //给一个guid为文件名
            var extension = Path.GetExtension(headers.ContentDisposition.FileName.Replace("\"", "")).ToLower();
            //文件名用随机数生成
            //Random ro = new Random();
            //string stro = ro.Next(100000000, 900000000).ToString();//产生一个随机数用于新命名

            //// 前6位是年月，以例放入相应的文件夹中
            //string fileName = DateTime.Now.ToString("yyyyMMmmssff") + stro;
            //return fileName + extension;
            return GenerateNewFilename(extension);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension">扩展名需要带小数点</param>
        /// <param name="lastDirName"></param>
        /// <returns></returns>
        public static string GenerateNewFilename(string extension, string lastDirName = "")
        {
            lastDirName = string.IsNullOrEmpty(lastDirName) ? DateTime.Now.ToString("yyyyMM") : lastDirName;
            Random ro = new Random();
            string stro = ro.Next(100000000, 900000000).ToString();//产生一个随机数用于新命名

            // 前6位是年月，以例放入相应的文件夹中
            string fileName = lastDirName + DateTime.Now.ToString("mmssff") + stro;
            return fileName + extension;
        }
    }
}