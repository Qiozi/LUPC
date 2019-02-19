using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Util
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
