using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetImgFullname
/// </summary>
public class GetImgFullname
{
    public static string Get(int sku, int w, int h, int i)
    {
        if (w < 50)
        {
            w = 300;
        }
        if (h < 50)
        {
            h = 300;
        }

        //if (sku == 0) return string.Empty;
        //HttpHelper hh = new HttpHelper();

        //return hh.HttpGet(string.Format("http://webapi.lucomputers.com/api/GetImagePath?sku={0}&w={1}&h={2}&i={3}"
        //    , sku
        //    , w
        //    , h
        //    , i
        //    )).Replace("\"", "");
        //public string Get(int sku, int w = 300, int h = 300, int i = 0)
        // {
        var host = "https://www.lucomputers.com/";
        if (w == 300)
        {
            return string.Format(@"{1}pro_img/components/{0}_list_1.jpg", sku, host);
        }
        if (w > 300)
        {
            if (i == 0)
            {
                return string.Format(@"{1}pro_img/source_components/{0}.jpg", sku, host);
            }
            else
            {
                return string.Format(@"{1}pro_img/components/{0}_g_{2}.jpg", sku, host, i);
            }
        }
        return string.Format("https://o9ozc36tl.qnssl.com/{0}{1}.jpg?imageView/3/w/{2}/h/{3}"
            , sku
            , i == 0 ? "" : "_" + i
            , w
            , h);
        // }
    }
}