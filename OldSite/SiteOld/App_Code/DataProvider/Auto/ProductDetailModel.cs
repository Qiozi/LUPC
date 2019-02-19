// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-27 14:11:38
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_product_detail")]
[Serializable]
public class ProductDetailModel : ActiveRecordBase<ProductDetailModel>
{
    int _product_detail_serial_no;
    int _product_in_serial_no;
    int _product_serial_no;
    int _purchase_serial_no;
    byte _product_detail_is_sale;
    byte _tag;
    DateTime _product_detail_create_date;
    string _product_sn;
    DateTime _sale_create_date;

    public ProductDetailModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int product_detail_serial_no
    {
        get { return _product_detail_serial_no; }
        set { _product_detail_serial_no = value; }
    }
    public static ProductDetailModel GetProductDetailModel(int _product_detail_serial_no)
    {
        ProductDetailModel[] models = ProductDetailModel.FindAllByProperty("product_detail_serial_no", _product_detail_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductDetailModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_in_serial_no
    {
        get { return _product_in_serial_no; }
        set { _product_in_serial_no = value; }
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
    public int purchase_serial_no
    {
        get { return _purchase_serial_no; }
        set { _purchase_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte product_detail_is_sale
    {
        get { return _product_detail_is_sale; }
        set { _product_detail_is_sale = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime product_detail_create_date
    {
        get { return _product_detail_create_date; }
        set { _product_detail_create_date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_sn
    {
        get { return _product_sn; }
        set { _product_sn = value; }
    }
     [Property]
    public DateTime sale_create_date
    {
        get { return _sale_create_date; }
        set { _sale_create_date = value; }
    }

    public static ProductDetailModel[] GetModelsByProductInSerial(int product_in_serial_no)
    {
        return ProductDetailModel.FindAllByProperty("product_in_serial_no", product_in_serial_no);
    }

    public static void DeleteByProductInSerial(int product_in_serial_no)
    {
        ProductDetailModel[] models = GetModelsByProductInSerial(product_in_serial_no);
        for (int i = 0; i < models.Length; i++)
        {
            models[i].Delete();
        }
    }

    public static int GetCountByProduct(int product_id)
    {
        return ProductDetailModel.FindAllByProperty("product_serial_no", product_id).Length;
    }

    public static ProductDetailModel[] GetModelsBySN(string product_sn)
    {
        return ProductDetailModel.FindAllByProperty("product_sn", product_sn);
    }

    public static string  GetSNByProductID(int product_serial_no)
    {
        DataTable dt = Config.ExecuteDataTable("select product_sn from tb_product_detail where product_detail_is_sale=0 and product_serial_no=" + product_serial_no + " order by product_detail_serial_no asc limit 1,1 ");
        if (dt.Rows.Count == 1)
            return dt.Rows[0][0].ToString();
        return "未找到此产品SN";
    }
}
