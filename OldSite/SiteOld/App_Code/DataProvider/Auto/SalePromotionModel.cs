// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-7-5 21:38:42
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_sale_promotion")]
[Serializable]
public class SalePromotionModel : ActiveRecordBase<SalePromotionModel>
{
    int _sale_promotion_serial_no;
    int _product_serial_no;
    DateTime _begin_datetime;
    DateTime _end_datetime;
    bool _show_it;
    decimal _save_cost;
    DateTime _create_datetime;
    decimal _price;
    decimal _cost;
    int _promotion_or_rebate;
    string _comment;
    string _pdf_filename;
    decimal _sale_price;

    public SalePromotionModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int sale_promotion_serial_no
    {
        get { return _sale_promotion_serial_no; }
        set { _sale_promotion_serial_no = value; }
    }
    public static SalePromotionModel GetSalePromotionModel(int _sale_promotion_serial_no)
    {
        SalePromotionModel[] models = SalePromotionModel.FindAllByProperty("sale_promotion_serial_no", _sale_promotion_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new SalePromotionModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime begin_datetime
    {
        get { return _begin_datetime; }
        set { _begin_datetime = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime end_datetime
    {
        get { return _end_datetime; }
        set { _end_datetime = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public bool show_it
    {
        get { return _show_it; }
        set { _show_it = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal save_cost
    {
        get { return _save_cost; }
        set { _save_cost = value; }
    }

    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price
    {
        get { return _price; }
        set { _price = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal cost
    {
        get { return _cost; }
        set { _cost = value; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int promotion_or_rebate
    {
        get { return _promotion_or_rebate; }
        set { _promotion_or_rebate = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string comment
    {
        get { return _comment; }
        set { _comment = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string pdf_filename
    {
        get { return _pdf_filename; }
        set { _pdf_filename = value; }
    }

    
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal sale_price
    {
        get { return _sale_price; }
        set { _sale_price = value; }
    }
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
