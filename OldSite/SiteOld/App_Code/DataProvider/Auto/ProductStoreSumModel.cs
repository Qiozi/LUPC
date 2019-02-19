// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-08-06 13:03:56
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System.Data;
using System;

[ActiveRecord("tb_product_store_sum")]
[Serializable]
public class ProductStoreSumModel : ActiveRecordBase<ProductStoreSumModel>
{
	public ProductStoreSumModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    int _id;
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _product_serial_no;
    [Property]
    public string product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }
    int _product_store_category;
    [Property]
    public int product_store_category
    {
        get { return _product_store_category; }
        set { _product_store_category = value; }
    }
    int _product_store_sum;
    [Property]
    public int product_store_sum
    {
        get { return _product_store_sum; }
        set { _product_store_sum = value; }
    }
    string _manufacturer_part_number;
    [Property]
    public string manufacturer_part_number
    {
        get { return _manufacturer_part_number; }
        set { _manufacturer_part_number = value; }
    }
    decimal _product_cost;
    [Property]
    public decimal product_cost
    {
        get { return _product_cost; }
        set { _product_cost = value; }
    }
    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
    int _lu_sku;
    [Property]
    public int lu_sku
    {
        get { return _lu_sku; }
        set { _lu_sku = value; }
    }
    string _product_name;
    [Property]
    public string product_name
    {
        get { return _product_name; }
        set { _product_name = value; }
    }

    bool _tag;
    [Property]
    public bool tag
    {
        get { return _tag; }
        set { _tag = value; }
    }
    public static ProductStoreSumModel GetProductStoreSumModel(int id)
    {
        ProductStoreSumModel[] models = ProductStoreSumModel.FindAllByProperty("id", id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductStoreSumModel();
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

    public static ProductStoreSumModel[] FindBySKUAndLtd(string sku, int ltd)
    {
        NHibernate.Expression.EqExpression eq1= new NHibernate.Expression.EqExpression("product_serial_no", sku);
        NHibernate.Expression.EqExpression eq2= new NHibernate.Expression.EqExpression("product_store_category", ltd);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        ProductStoreSumModel[] psss = ProductStoreSumModel.FindAll(a);
        return psss;
    }
    public static ProductStoreSumModel[] FindByMenufactureAndLtdID(string manufacturer_part_number, int ltd_id)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("manufacturer_part_number", manufacturer_part_number);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("product_store_category", ltd_id);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        ProductStoreSumModel[] psss = ProductStoreSumModel.FindAll(a);
        return psss;
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
