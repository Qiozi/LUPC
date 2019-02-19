using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class ChangeEBayStockQuantity
    {
        
        public static void Run(int sku)
        {
            if (sku == 0) return;

            WebClientHelper.GetPage(string.Format("http://webapi.lucomputers.com/api/SeteBayItemQty/Get?t={1}&sku={0}", sku, GenerateWebApiToken. GetToken()));
        }

    }
}
