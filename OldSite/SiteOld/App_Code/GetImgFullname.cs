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
        if (sku == 0) return string.Empty;
        HttpHelper hh = new HttpHelper();

        return hh.HttpGet(string.Format("http://webapi.lucomputers.com/api/GetImagePath?sku={0}&w={1}&h={2}&i={3}"
            , sku
            , w
            , h
            , i
            )).Replace("\"", "");

    }
}