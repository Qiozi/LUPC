using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KKWStore.db
{
    public class OutInvoiceProductShippingModel_p
    {
        /// <summary>
        /// 取得所有的运单号码
        /// </summary>
        /// <param name="outID"></param>
        /// <returns></returns>
        public static string GetShippingSN(db.qstoreEntities db, int outID)
        {
            StringBuilder sb = new StringBuilder();
            //OutInvoiceProductShippingModel[] outips = FindAllByProperty("out_invoice_id", outID);
            var outips = db.tb_out_invoice_product_shipping.Where(p => p.out_invoice_id.HasValue &&
                p.out_invoice_id.Value.Equals(outID)).ToList();
            foreach (var o in outips)
            {
                sb.Append("," + o.ShippingCode);

            }
            if (sb.Length > 0)
                return sb.ToString().Substring(1);
            return "";
        }
        /// <summary>
        /// 取得当前订单的运单
        /// </summary>
        /// <param name="outInvoiceId"></param>
        /// <returns></returns>
        public static List<tb_out_invoice_product_shipping> GetModelsByInvoiceCode(db.qstoreEntities context, int outInvoiceId)
        {
            //return FindAllByProperty("out_invoice_id", outInvoiceId);
            return context.tb_out_invoice_product_shipping.Where(o => o.out_invoice_id.HasValue &&
                o.out_invoice_id.Value.Equals(outInvoiceId)).ToList();
        }
    }
}
