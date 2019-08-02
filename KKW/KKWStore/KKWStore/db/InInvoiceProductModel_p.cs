using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace KKWStore.db
{
    public class InInvoiceProductModel_p
    {
        /// <summary>
        /// 取得某个产品的所有进货单据号
        /// </summary>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetALLByPCode(string p_code)
        {
            return db.SqlExec.ExecuteDataTable(@"select 	id, in_invoice_id, in_invoice_code, p_id, p_code, quantity, 
	cost, 
	regdate
	 
	from 
	tb_in_invoice_product where p_code='" + p_code + "'");
        }

        /// <summary>
        /// 查找进货历史
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="brand"></param>
        /// <returns></returns>
        public static DataTable GetInStoreHistory(DateTime beginDate, DateTime endDate, string brand, string p_code)
        {
            string sql = string.Format(@"select invoice_code '批次', p.p_code '编号',p.WholesalerCode '批发商编号', p.p_name '商品名称', date_format(ii.input_regdate,'%Y-%m-%d') '进货日期',iip.cost '成本', iip.quantity '数量'
                                        from tb_in_invoice_product iip inner join tb_product p on iip.p_id=p.id  
                                            inner join tb_in_invoice ii on ii.invoice_code=iip.in_invoice_code 
                                        where date_format(ii.input_regdate, '%Y%j') >= date_format('{0}', '%Y%j')
                                            and date_format(ii.input_regdate, '%Y%j')<= date_format('{1}','%Y%j')
                                            {2} {3}"
                                                , beginDate.ToString("yyyy-MM-dd")
                                                , endDate.ToString("yyyy-MM-dd")
                                                , !string.IsNullOrEmpty(brand) ? " and p.brand = '" + brand + "'" : ""
                                                , !string.IsNullOrEmpty(p_code) ? " and p.p_code = '" + p_code + "'" : ""
                                                );
            return db.SqlExec.ExecuteDataTable(sql);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="brand"></param>
        /// <param name="p_code"></param>
        /// <returns></returns>
        public static DataTable GetInStoreHistorySN(DateTime beginDate, DateTime endDate, string p_code)
        {
            string sql = string.Format(@"select s.SerialNo, date_format(s.in_regdate, '%Y-%m-%d') d
                                        from tb_serial_no_and_p_code s 
                                        where date_format(s.in_regdate, '%Y%j') >= date_format('{0}', '%Y%j')
                                            and date_format(s.in_regdate, '%Y%j')<= date_format('{1}','%Y%j')
                                            {2} order by s.in_regdate asc "
                                                , beginDate.ToString("yyyy-MM-dd")
                                                , endDate.ToString("yyyy-MM-dd")
                                                , !string.IsNullOrEmpty(p_code) ? " and s.in_invoice_code = '" + p_code + "'" : ""
                                                );
            return db.SqlExec.ExecuteDataTable(sql);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="in_invoice_code"></param>
        /// <returns></returns>
        public static DataTable GetProductCodeAndNameByInvoiceCode(string in_invoice_code)
        {
            return db.SqlExec.ExecuteDataTable("select p.p_code, p.p_name from tb_product p inner join tb_in_invoice_product iip on iip.p_id=p.id where iip.in_invoice_code = '" + in_invoice_code + "' limit 0,1");

        }

        /// <summary>
        /// 给修改价格时使用。
        /// </summary>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public static DataTable GetInvoiceForModifyCost(int p_id)
        {
            return db.SqlExec.ExecuteDataTable("select in_invoice_code, p_code,  date_format(regdate, '%Y-%m-%d') regdate, cost from tb_in_invoice_product where p_id='" + p_id.ToString() + "' order by  id desc ");
        }

        public static string GetInvoiceCostByInvocieCode(string InvocieCode)
        {
            return db.SqlExec.ExecuteScalar("select cost from tb_in_invoice_product where in_invoice_code='" + InvocieCode + "' order by id desc limit 0,1 ").ToString();

        }
        /// <summary>
        /// @modify 2019-08-01 改为修改所有库存的价格。 为了与云仓同价。
        /// 
        /// </summary>
        /// <param name="invoicdCode"></param>
        /// <param name="newCost"></param>
        /// <param name="p_id"></param>
        /// <param name="isModifyToList"></param>
        /// <returns></returns>
        public static bool ModifyCostByInvoiceCode(string invoicdCode, decimal newCost, string p_id, bool isModifyToList)
        {
            var context = new qstoreEntities();
            var id = int.Parse(p_id);
            var prod = context.tb_product.Single(me => me.id.Equals(id));
            var oldCost = prod.p_cost.Value;

            db.SqlExec.ExecuteNonQuery("Update tb_in_invoice_product set cost='" + newCost.ToString() + "' where in_invoice_code='" + invoicdCode + "'");
            db.SqlExec.ExecuteNonQuery("Update tb_serial_no_and_p_code set in_cost ='" + newCost.ToString() + "' where p_id='" + p_id + "' and out_regdate like '00%' ");

            if (isModifyToList && !string.IsNullOrEmpty(p_id))
            {
                prod.p_cost = newCost;
                db.SqlExec.ExecuteNonQuery("update tb_product set p_cost ='" + newCost.ToString() + "' where id='" + p_id + "'");
            }
            
            var model = new db.tb_prod_cost_modify_record
            {
                new_cost = newCost,
                old_cost = oldCost,
                p_code = prod.p_code,
                p_id = prod.id,
                p_name = prod.p_name,
                regdate = DateTime.Now,
                staff_name = KKWStore.Helper.Config.CurrentUser.user_name
            };
            context.tb_prod_cost_modify_record.Add(model);
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inInvoice_code"></param>
        /// <returns></returns>
        public static DataTable GetInvoiceProductByInvoiceCode(string inInvoice_code)
        {
            return db.SqlExec.ExecuteDataTable("Select * from tb_in_invoice_product where in_invoice_code='" + inInvoice_code.Trim() + "'");
        }
    }
}
