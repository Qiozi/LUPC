using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadEBayOrder.BLL
{
    public class DownEbayOnsaleDetail
    {
        
        public static bool Do()
        {
            string token = GenerateWebApiToken.GetToken();
            string url = string.Concat("http://webapi.lucomputers.com/Api/GetPromotionalSaleDetails/Get?t=", token);
            WebClientHelper.GetPage(url);
            return true;
        }
    }
}
