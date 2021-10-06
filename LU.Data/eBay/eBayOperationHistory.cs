using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Data.eBay
{
    public class eBayOperationHistory
    {
        public static bool SaveSendXml(nicklu2Entities context, string sendXml, bool isSys, int sku)
        {
            var esxhm = new tb_ebay_send_xml_history
            {
                Content = sendXml,
                is_sys = isSys,
                SKU = sku,
                comm = string.Empty,
                is_modify = false,
                regdate = DateTime.Now
            };
            context.tb_ebay_send_xml_history.Add(esxhm);
            context.SaveChanges();
            return true;
        }

        public static bool SaveSendXmlResult(nicklu2Entities context, string sendXml, bool isSys, int sku)
        {
            var esxrh = new tb_ebay_send_xml_result_history
            {
                comm = string.Empty,
                Content = sendXml,
                is_modify = true,
                is_sys = isSys,
                regdate = DateTime.Now,
                SKU = sku
            };
            context.tb_ebay_send_xml_result_history.Add(esxrh);
            context.SaveChanges();
            return true;
        }
    }
}
