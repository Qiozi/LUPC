// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-7-5 21:38:42
//
// // // // // // // // // // // // // // // //
using System;
using System.Data;

[Serializable]
public class SalePromotionModel
{
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product_id"></param>
    /// <param name="promotion_or_rebates">1. promotion, 2.rebate</param>
    /// <returns></returns>
    public static DataTable GetOne(int product_id, int promotion_or_rebates)
    {
        return Config.ExecuteDataTable("select * from tb_sale_promotion where promotion_or_rebate=" + promotion_or_rebates + " and  product_serial_no=" + product_id + " order by sale_promotion_serial_no desc limit 0,1");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product_id"></param>
    /// <param name="begin_datetime"></param>
    /// <param name="end_datetime"></param>
    /// <param name="save_cost"></param>
    /// <param name="show_it"></param>
    /// <param name="promotion_or_rebates">1. promotion, 2.rebate</param>
    /// <returns></returns>
    public static DataTable GetOneMore(int product_id, DateTime begin_datetime, DateTime end_datetime, decimal sale_price, bool show_it, int promotion_or_rebates, string comments, string pdf_filename)
    {
        //throw new Exception("select * from tb_sale_promotion where product_serial_no=" + product_id + " and begin_datetime='" + begin_datetime.ToString("yyyy-MM-dd") + "' and end_datetime='" + end_datetime.ToString("yyyy-MM-dd") + "' and save_cost=" + save_cost + " order by sale_promotion_serial_no desc ");
        return Config.ExecuteDataTable("select * from tb_sale_promotion where promotion_or_rebate=" + promotion_or_rebates + " and product_serial_no=" + product_id + " and date(begin_datetime)<='" + begin_datetime.ToString("yyyy-MM-dd") + "' and date(end_datetime)>='" + end_datetime.ToString("yyyy-MM-dd") + "' and sale_price=" + sale_price + "  and show_it=" + (show_it == true ? "1" : "0") + " and comment='" + comments + "' and pdf_filename='" + pdf_filename + "' order by sale_promotion_serial_no desc ");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product_id"></param>
    /// <param name="promotion_or_rebates">1. promotion, 2.rebate</param>
    /// <returns></returns>
    public static DataTable GetModelsByProduct(int product_id, int promotion_or_rebates)
    {
        return Config.ExecuteDataTable("select * from tb_sale_promotion where promotion_or_rebate=" + promotion_or_rebates + " and product_serial_no=" + product_id + " order by sale_promotion_serial_no desc");
    }

    public DataTable FindDateTimeByPartID(int part_id)
    {
        
        return Config.ExecuteDataTable(@"select sp.begin_datetime,sp.end_datetime from tb_sale_promotion sp 
inner join tb_product p on p.product_serial_no = sp.product_serial_no 
where sp.show_it=1 and pdf_filename <> '' and p.tag=1 and (TO_DAYS(now()) between TO_DAYS(sp.begin_datetime) and TO_DAYS(sp.end_datetime)+30)
and p.product_serial_no='" + part_id .ToString()+ "' and promotion_or_rebate=2  and sp.show_it=1 order by sp.end_datetime desc ");
    }
}
