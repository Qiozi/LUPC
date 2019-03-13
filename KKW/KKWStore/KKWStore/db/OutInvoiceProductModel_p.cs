using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace KKWStore.db
{
    public class OutInvoiceProductModel_p
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outInvoiceID"></param>
        /// <returns></returns>
        public static DataTable GetModelsByOutID(int outInvoiceID)
        {
            return db.SqlExec.ExecuteDataTable(string.Format(@"select p.*, oip.SerialNO from tb_out_invoice_product oip inner join tb_serial_no_and_p_code snp on snp.SerialNO=oip.SerialNO 
inner join tb_product p on p.p_code=snp.p_code where out_invoice_id='{0}'", outInvoiceID));
        }
        /// <summary>
        /// 用SerialNO 取得单个出库纪录
        /// </summary>
        /// <param name="serial_NO"></param>
        /// <returns></returns>
        public static tb_out_invoice_product GetModelBySerialNO(qstoreEntities context, string serial_NO)
        {
            //OutInvoiceProductModel[] oipms = FindAllByProperty("SerialNO", serial_NO);
            //if (oipms.Length > 0)
            //    return oipms[0];
            //return null;
            return context.tb_out_invoice_product.SingleOrDefault(p => p.SerialNO.Equals(serial_NO));
        }
        /// <summary>
        /// 取得产品的出库纪录
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetProductOutHistory(string p_code)
        {
            string sql = @"select oi.staff, oi.input_regdate, oi.invoice_code, oi.summary, oi.note, oi.pay_method, oi.Tid,oi.ReceiverName from tb_serial_no_and_p_code ss inner join tb_out_invoice_product op
on op.SerialNO=ss.SerialNO
inner join tb_out_invoice oi on oi.id=op.out_invoice_id
  where date_format(out_regdate, '%Y')>2000 and IsReturnWholesaler=0";
            if (p_code == null || p_code == "")
            {
                sql = string.Format(@"select oi.staff, oi.input_regdate, oi.invoice_code, oi.summary, oi.note, oi.pay_method, oi.Tid,oi.ReceiverName from tb_serial_no_and_p_code ss inner join tb_out_invoice_product op
on op.SerialNO=ss.SerialNO
inner join tb_out_invoice oi on oi.id=op.out_invoice_id
  where p_code='{0}' and date_format(out_regdate, '%Y')>2000 and IsReturnWholesaler=0", 23424124234);
            }
            return db.SqlExec.ExecuteDataTable(sql);
        }
    }
}
