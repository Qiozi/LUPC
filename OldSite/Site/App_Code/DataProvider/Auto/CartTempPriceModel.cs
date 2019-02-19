// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-9-8 15:30:20
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System.Linq;


public class CartTempPriceModel 
{
    //int _tmp_price_serial_no;
    //decimal _sub_total;
    //decimal _shipping_and_handling;
    //decimal _sales_tax;
    //decimal _grand_total;
    //string _order_code;
    //DateTime _create_datetime;
    //decimal _gst;
    //decimal _pst;
    //decimal _hst;
    //decimal _sur_charge_rate;
    //decimal _sub_charge;
    //decimal _gst_rate;
    //decimal _pst_rate;
    //decimal _hst_rate;
    //decimal _sub_total_rate;
    //decimal _shipping_and_handling_rate;
    //decimal _sales_tax_rate;
    //decimal _grand_total_rate;
    //decimal _gst_charge_rate;
    //decimal _pst_charge_rate;
    //decimal _hst_charge_rate;
    //decimal _cost;
    //decimal _taxable_total;
    //string _price_unit;

    //public CartTempPriceModel()
    //{

    //}


    ///// <summary>
    ///// 
    ///// </summary>
    //[PrimaryKey(PrimaryKeyType.Identity)]
    //public int tmp_price_serial_no
    //{
    //    get { return _tmp_price_serial_no; }
    //    set { _tmp_price_serial_no = value; }
    //}
    //public static CartTempPriceModel GetCartTempPriceModel(int _tmp_price_serial_no)
    //{
    //    CartTempPriceModel[] models = CartTempPriceModel.FindAllByProperty("tmp_price_serial_no", _tmp_price_serial_no);
    //    if (models.Length == 1)
    //        return models[0];
    //    else
    //        return new tb_cart_temp_price();
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal sub_total
    //{
    //    get { return _sub_total; }
    //    set { _sub_total = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal shipping_and_handling
    //{
    //    get { return _shipping_and_handling; }
    //    set { _shipping_and_handling = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal sales_tax
    //{
    //    get { return _sales_tax; }
    //    set { _sales_tax = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal grand_total
    //{
    //    get { return _grand_total; }
    //    set { _grand_total = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public string order_code
    //{
    //    get { return _order_code; }
    //    set { _order_code = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public DateTime create_datetime
    //{
    //    get { return _create_datetime; }
    //    set { _create_datetime = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal gst
    //{
    //    get { return _gst; }
    //    set { _gst = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal pst
    //{
    //    get { return _pst; }
    //    set { _pst = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal hst
    //{
    //    get { return _hst; }
    //    set { _hst = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal sur_charge_rate
    //{
    //    get { return _sur_charge_rate; }
    //    set { _sur_charge_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal sur_charge
    //{
    //    get { return _sub_charge; }
    //    set { _sub_charge = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal gst_rate
    //{
    //    get { return _gst_rate; }
    //    set { _gst_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal pst_rate
    //{
    //    get { return _pst_rate; }
    //    set { _pst_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal hst_rate
    //{
    //    get { return _hst_rate; }
    //    set { _hst_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal sub_total_rate
    //{
    //    get { return _sub_total_rate; }
    //    set { _sub_total_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal shipping_and_handling_rate
    //{
    //    get { return _shipping_and_handling_rate; }
    //    set { _shipping_and_handling_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal sales_tax_rate
    //{
    //    get { return _sales_tax_rate; }
    //    set { _sales_tax_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal grand_total_rate
    //{
    //    get { return _grand_total_rate; }
    //    set { _grand_total_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal gst_charge_rate
    //{
    //    get { return _gst_charge_rate; }
    //    set { _gst_charge_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal pst_charge_rate
    //{
    //    get { return _pst_charge_rate; }
    //    set { _pst_charge_rate = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal hst_charge_rate
    //{
    //    get { return _hst_charge_rate; }
    //    set { _hst_charge_rate = value; }
    //}
    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal cost
    //{
    //    get { return _cost; }
    //    set { _cost = value; }
    //}
    //        /// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public decimal taxable_total
    //{
    //    get { return _taxable_total; }
    //    set { _taxable_total = value; }
    //}
    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public string price_unit
    //{
    //    get { return _price_unit; }
    //    set { _price_unit = value; }
    //}
    public static tb_cart_temp_price GetModelsByOrderCode(nicklu2Entities context, string order_code)
    {
        //CartTempPriceModel[] ms = CartTempPriceModel.FindAllByProperty("order_code", order_code);
        //if (ms.Length > 0)
        //    return ms[0];
        //else
        //    return null;

        var query = context.tb_cart_temp_price.FirstOrDefault(me => me.order_code.Equals(order_code));
        return query;
    }

    public static void DeleteByOrderCode(string order_code)
    {
        Config.ExecuteNonQuery("delete from tb_cart_temp_price where order_code='" + order_code + "'");
    }
}
