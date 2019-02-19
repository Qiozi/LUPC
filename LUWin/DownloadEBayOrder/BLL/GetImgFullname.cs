using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadEBayOrder.BLL
{
    public class GetImgFullname
    {
        public static string Get(int sku, int w, int h, int i)
        {
            if (sku == 0) return string.Empty;

            return WebClientHelper.GetPage(string.Format("http://webapi.lucomputers.com/api/GetImagePath?sku={0}&w={1}&h={2}&i={3}&t={4}"
                , sku
                , w
                , h
                , i
                , GenerateWebApiToken.GetToken())).Replace("\"", "");

        }
    }
}
