using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace KKWStore.db
{
    public class InInvoiceModel_p
    {
        /// <summary>
        /// 判断单据编码是否存在
        /// </summary>
        /// <param name="invoiceCode"></param>
        /// <returns></returns>
        public static bool ExistInvoiceCode(string invoiceCode)
        {
            return db.SqlExec.ExecuteScalarInt("Select count(*) from tb_in_invoice where invoice_code='" + invoiceCode + "'") == 1;
        }
        /// <summary>
        /// 取得新的单据编码
        /// </summary>
        /// <returns></returns>
        public static string NewInvoiceCode()
        {
            if (db.SqlExec.ExecuteScalarInt("Select count(*) from tb_in_invoice") > 0)
            {
                string serial = "00000" + (db.SqlExec.ExecuteScalarInt("Select max(id) from tb_in_invoice") + 1).ToString();
                return DateTime.Now.ToString("yyMMdd") + serial.Substring(serial.Length -6);
            }
            else
                return DateTime.Now.ToString("yyMMdd") + "000001";
        }
        /// <summary>
        /// 取得已用过的付款帐户
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPayMethodList()
        {
            List<string> pay = new List<string>();
            DataTable dt = db.SqlExec.ExecuteDataTable("select distinct pay_method from tb_in_invoice");
            foreach (DataRow dr in dt.Rows)
            {
                pay.Add(dr[0].ToString());
            }
            return pay;
        }
        /// <summary>
        /// 取得已用过的供货单位
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> GetSupplierList()
        {
            List<Supplier> pay = new List<Supplier>();
            DataTable dt = db.SqlExec.ExecuteDataTable("select distinct Supplier, sum(pay_total) total from tb_in_invoice group by Supplier");
            foreach (DataRow dr in dt.Rows)
            {
                pay.Add(new Supplier()
                {
                    Name = dr[0].ToString(),
                    Cost = dr[1].ToString()
                });
            }
            return pay;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetInIvoiceList(DateTime beginDate, DateTime endDate)
        {
            return db.SqlExec.ExecuteDataTable(string.Format(@"
select i.* 
,(select count(id) from tb_in_invoice_product where in_invoice_code=i.invoice_code) c
,(select sum(quantity) from tb_in_invoice_product where in_invoice_code=i.invoice_code) s
from tb_in_invoice i

where 
id>0
and date_format(regdate, '%Y%j')>= date_format('{0}', '%Y%j') 
and date_format(regdate, '%Y%j')<=  date_format('{1}', '%Y%j') order by id desc "
                , beginDate.ToString("yyyy-MM-dd")
                , endDate .ToString("yyyy-MM-dd")));
        }

        /// <summary>
        /// 条码如果没有被使用， 将可以删除整批
        /// 
        /// </summary>
        /// <param name="in_invoice_code"></param>
        /// <returns></returns>
        public static bool SerialnoNoUsed(string in_invoice_code)
        {
            int allCount = db.SqlExec.ExecuteScalarInt("select count(id) from tb_serial_no_and_p_code where in_invoice_code='" + in_invoice_code.Trim() + "' ");
            int count = db.SqlExec.ExecuteScalarInt("select count(id) from tb_serial_no_and_p_code where in_invoice_code='" + in_invoice_code.Trim() + "'  and serialno='" + Helper.Config.TmpSNCode + "'");
            return allCount == count;

        }

        /// <summary>
        /// 删除整批次的条码，并返回数量说明
        /// </summary>
        /// <param name="in_invoice_code"></param>
        /// <returns></returns>
        public static string DelALLSerialNO(string in_invoice_code)
        {
            string result = "删除的数据:";

            DataTable dt = db.InInvoiceProductModel_p.GetInvoiceProductByInvoiceCode(in_invoice_code);
            foreach (DataRow dr in dt.Rows)
            {
                int count = db.SqlExec.ExecuteScalarInt("select count(id) from tb_serial_no_and_p_code where p_code='" + dr["p_code"].ToString() + "' and in_invoice_code='" + in_invoice_code.Trim() + "'  and serialno='" + Helper.Config.TmpSNCode + "'");
                result += " (编号：" + dr["p_code"].ToString() + "  数量: " + count.ToString() +")";
                db.SqlExec.ExecuteNonQuery("delete from tb_serial_no_and_p_code where p_code='" + dr["p_code"].ToString() + "' and in_invoice_code='" + in_invoice_code.Trim() + "'  and serialno='" + Helper.Config.TmpSNCode + "'");
            }
            db.SqlExec.ExecuteNonQuery("Update tb_in_invoice_product set quantity='0' where in_invoice_code='" + in_invoice_code.Trim() + "'");
            return result;
        }
    }
}
