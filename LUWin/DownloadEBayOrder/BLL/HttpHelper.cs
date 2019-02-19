using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace DownloadEBayOrder
{
    public class HttpHelper 
    {
        //public delegate void LoadRunEventHandler(HttpHelper sender, Watch.LoadRunEventArgs e);
        //public event LoadRunEventHandler Runing;

        //private void OnRuning(Watch.LoadRunEventArgs e)
        //{
        //    if (e != null)
        //        Runing(this, e);
        //}

        public HttpHelper()
        {

        }
    
        /// <summary>
        /// request page info.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string HttpGet(string url)
        {
            //OnRuning(new LUComputers.Watch.LoadRunEventArgs(url));
            //url = "http://www.lucomputers.com/";
         
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            req.ReadWriteTimeout = 600000;
            req.Timeout = 600000;
          
            WebResponse resp = req.GetResponse();
            if (url != resp.ResponseUri.ToString())
            {
                //System.Windows.Forms.MessageBox.Show(resp.ResponseUri.ToString());
                HttpGet(resp.ResponseUri.ToString());
            }
            StreamReader sr = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8);
            string sReturn = sr.ReadToEnd().Trim();
            resp.Close();
            sr.Close();
            return sReturn;
        }
        /// <summary>
        /// post info to remote web site , and get return info.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="ProxyString"></param>
        /// <param name="referer"></param>
        /// <returns></returns>
        public string HttpPost(string url, string parameters, string ProxyString, string referer)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(parameters);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Proxy = new WebProxy(ProxyString, true);
            req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:1.8) Gecko/20051111 Firefox/1.5";
            req.Accept = "text/xml,application/xml,application/xhtml+xml,text/html";
            req.KeepAlive = true;
           // req.Referer = string.Format(referer);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";

            req.ContentLength = bytes.Length;
            Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            if (resp == null)
                return null;
            StreamReader sr = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8);
            string sReturn = sr.ReadToEnd().Trim();
            resp.Close();
            sr.Close();
            return sReturn;
        }

        public string GetHttpHead(string httpContent)
        {
            return string.Format(@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /><title>Compare Reault</title></head><body>{0}</body></html>", httpContent);
        }
    }
    
}
