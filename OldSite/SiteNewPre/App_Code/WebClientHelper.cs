using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for WebClientHelper
/// </summary>
public class WebClientHelper
{
	public WebClientHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}
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

    public static string Get(int sku, int w, int h, int i)
    {
        if (sku == 0) return string.Empty;

        return WebClientHelper.GetPage(string.Format("http://webapi.lucomputers.com/api/GetImagePath?sku={0}&w={1}&h={2}&i={3}&t={4}"
            , sku
            , w
            , h
            , i
            , string.Empty)).Replace("\"", "");

    }
}