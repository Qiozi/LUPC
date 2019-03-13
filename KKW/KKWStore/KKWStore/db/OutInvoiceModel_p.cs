using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace KKWStore.db
{
    public class OutInvoiceModel_p
    {
        /// <summary>
        /// 取得订单列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static DataTable GetListBySearch(string keyword, int pageSize, string staff)
        {
            string sql = "";
            string staffSql = "";
            if (staff == null || staff.Trim() == "")
            {
                staffSql = " and staff='" + staff + "'";
            }
            if (keyword.Trim().Length == 0)
            {
                sql = string.Format(@"select o.* from tb_out_invoice o order by id desc limit 0,{0}", pageSize);
            }
            else
                sql = string.Format(@"select o.* from tb_out_invoice o where 
invoice_code='{0}'
or title like '%{0}%'
or pay_total='{0}'
or receiverName like  '%{0}%'
or ReceiverMobile like '%{0}%'
or ReceiverPhone like '%{0}%'
or id in (select out_invoice_id from tb_out_invoice_product where SerialNO='{0}')
or id in (select out_invoice_id from tb_out_invoice_product_shipping where ShippingCode='{0}') limit 0, {1}", keyword, pageSize);
            DataTable dt = db.SqlExec.ExecuteDataTable(sql);
            return dt;
        }
        /// <summary>
        /// 得取各个网站的订单数量
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStoreTotal()
        {
            return db.SqlExec.ExecuteDataTable("select distinct Staff , count(*) c from tb_out_invoice group by Staff order by c desc ");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static tb_out_invoice GetModelByTid(qstoreEntities DB, string tid)
        {
            //OutInvoiceModel[] oims = FindAllByProperty("Tid", tid);
            //if (oims.Length > 0)
            //    return oims[0];
            //return null;

            return DB.tb_out_invoice.SingleOrDefault(s => s.Tid.Equals(tid));
        }
    }
}
