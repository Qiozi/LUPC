using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace KKWStore.db
{
    public class ReturnHistoryModel_p 
    {
        /// <summary>
        /// 取得列表，
        /// 按sn查訽，
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public static DataTable GetModelsBySearch(string sn)
        {
           string sql = string.Format(@"select rh.*, oi.receiverName, p.p_name from tb_return_history  rh left join tb_out_invoice oi on oi.id=rh.out_invoice_id
left join tb_serial_no_and_p_code snap on snap.SerialNO=rh.SerialNO 
left join tb_product p on p.p_code=snap.p_code");
           if (sn.Length > 0)
           {
               sql += " where rh.SerialNO like '%" + sn + "%' or rh.p_code like '%" + sn + "%' or p.p_name like '%" + sn + "%' ";
           }
           sql += " order by rh.id desc ";
           return db.SqlExec.ExecuteDataTable(sql);
        }
    }
}
