using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace DownloadEBayOrder.BLL
{
    public class WebClientHelper
    {
        public static string GetPage(string uri)
        {
            WebClient client = new WebClient();

           // string uri = BLL.WebUrl.GeteBayPriceUrl(cost, screen, adjustment, sku);
            Stream stream = client.OpenRead(uri);
            StreamReader sr = new StreamReader(stream);
            string priceContent = sr.ReadToEnd();
            sr.Close();
            return priceContent;
        }
    }
}
