using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DownloadEBayOrder.BLL
{
    public class Variable
    {
        public static string storePath = Application.StartupPath + "\\eBayOrders";

        public static string itemDescriptionPath = Application.StartupPath + "\\ItemDescriptions";

        public static string ebayPartPricePath = Application.StartupPath + "\\eBayPartPrice";
    }
}
