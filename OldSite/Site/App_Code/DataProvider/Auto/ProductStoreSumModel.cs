// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-08-06 13:03:56
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class ProductStoreSumModel
{
	
    public static tb_product_store_sum GetProductStoreSumModel(nicklu2Entities context, int id)
    {
        //ProductStoreSumModel[] models = ProductStoreSumModel.FindAllByProperty("id", id);
        //if (models.Length == 1)
        //    return models[0];
        //else
        //    return new ProductStoreSumModel();
        var query = context.tb_product_store_sum.FirstOrDefault(me => me.id.Equals(id));
        if (query == null)
        {
            return new tb_product_store_sum();
        }
        else
            return query;
    }

    public static void DeleteAllByLtd(Ltd ltd)
    {
        switch (ltd)
        {
            case Ltd.wholesaler_asi:
                Config.ExecuteNonQuery("delete from tb_product_store_sum where product_store_category = '3'");
                //ProductStoreSumModel.DeleteAll(" product_store_category = '3'");
                break;
            case Ltd.lu:
                Config.ExecuteNonQuery("delete from tb_product_store_sum where product_store_category = '1'");
                //ProductStoreSumModel.DeleteAll(" product_store_category = '1'");
                break;

            case Ltd.wholesaler_supercom:
                Config.ExecuteNonQuery("delete from tb_product_store_sum where product_store_category = '2'");
                //ProductStoreSumModel.DeleteAll(" product_store_category = '2'");
                break;
        }
    }

    public static tb_product_store_sum[] FindBySKUAndLtd(nicklu2Entities context, string sku, int ltd)
    {
        //NHibernate.Expression.EqExpression eq1= new NHibernate.Expression.EqExpression("product_serial_no", sku);
        //NHibernate.Expression.EqExpression eq2= new NHibernate.Expression.EqExpression("product_store_category", ltd);
        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        //ProductStoreSumModel[] psss = ProductStoreSumModel.FindAll(a);
        //return psss;
        var query = context.tb_product_store_sum.Where(me => me.product_serial_no.Equals(sku) && me.product_store_category.Value.Equals(ltd)).ToList();
        return query.ToArray();
    }
    public static tb_product_store_sum[] FindByMenufactureAndLtdID(nicklu2Entities context, string manufacturer_part_number, int ltd_id)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("manufacturer_part_number", manufacturer_part_number);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("product_store_category", ltd_id);
        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        //ProductStoreSumModel[] psss = ProductStoreSumModel.FindAll(a);
        //return psss;
        var query = context.tb_product_store_sum.Where(me => me.manufacturer_part_number.Equals(manufacturer_part_number) && me.product_store_category.Value.Equals(ltd_id)).ToList();
        return query.ToArray();
    }

    public DataTable FindLtdsByLUSKU(int lu_sku)
    {
        return Config.ExecuteDataTable(@"select id, product_serial_no, product_store_category, ifnull(product_store_sum,0) product_store_sum, manufacturer_part_number, 
	ifnull(product_cost, 0) product_cost, 
	regdate, 
	product_name	 
    ,tag
	from 
	tb_product_store_sum where product_store_category >0 and lu_sku='" + lu_sku + "'");
    }

    public DataTable FindModelsByLtdID(int ltd_id)
    {
        return Config.ExecuteDataTable(@"Select product_store_category Ltd_code,
product_serial_no Ltd_sku,
product_cost Ltd_cost,
 product_store_sum Ltd_stock,
manufacturer_part_number Ltd_manufacture_code,
product_name Ltd_part_name
from tb_product_store_sum where product_store_category='" + ltd_id + "'");

    }

    public int FindModelsSUM(int ltd_id)
    {
        return int.Parse(Config.ExecuteScalar("select ifnull(count(id),0) from tb_product_store_sum where product_store_category='" + ltd_id + "'").ToString());
    }


    public DataTable FindStoreStatusByLuSku(int lu_sku)
    {
        return Config.ExecuteDataTable(string.Format(@"select product_store_sum s, product_current_cost c , 1 ltd_id, last_regdate from tb_product where product_serial_no='{0}'
union all
select product_store_sum s, product_cost c, product_store_category ltd_id, regdate last_regdate from tb_product_store_sum where lu_sku='{0}' ", lu_sku));
    }
}
