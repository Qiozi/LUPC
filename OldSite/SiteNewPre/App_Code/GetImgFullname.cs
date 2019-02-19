using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for GetImgFullname
/// </summary>
public class GetImgFullname
{
    public GetImgFullname()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string Get(int sku, int w, int h, int i)
    {
        if (sku == 0) return string.Empty;

        WebClient wc = new WebClient();
        var uri = string.Format("http://webapi.lucomputers.com/api/GetImagePath?sku={0}&w={1}&h={2}&i={3}"
                                                                                                            , sku
                                                                                                            , w
                                                                                                            , h
                                                                                                            , i
                                                                                                            );
        Stream stream = wc.OpenRead(uri);
        StreamReader sr = new StreamReader(stream);
        string strLine = "";
        strLine = sr.ReadToEnd().Replace("\"", "");
        sr.Close();
        return strLine;

    }
}