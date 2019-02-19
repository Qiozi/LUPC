using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AccountCharge2 : System.Web.UI.Page
{

   protected void Page_Load(object sender, EventArgs e)
    {
        Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            accountCharge(ShippingCompany);
        }
        catch { }
    }


    public void accountCharge(int shippingCompany)
    {

            string error_msg = "Service not available.";
            if (TmpCode.ToString().Length != 6)
                return;
            DataTable dt = CartTempModel.GetModelsDTByTmeCode(TmpCode);

            int sale_promotion_count = Config.ExecuteScalarInt32(@"select count(c.cart_temp_serial_no)  from tb_cart_temp  c  inner join tb_product_shipping_fee ps 
on ps.prod_sku=c.product_serial_no and ps.is_system=0 where c.cart_temp_code='" + TmpCode + "' ");

            int ap_real_count = dt.Rows.Count - (Config.sale_promotion_compay_id.IndexOf("[" + shippingCompany + "]") == -1 ? 0 : sale_promotion_count);

            AccountProduct[] ap = new AccountProduct[ap_real_count];
            int _state_shipping = -1;
            decimal _price_sum = 0;
            // decimal _sale_tax = 0;
            // decimal result = 0;

            decimal sale_promotion_charge = 0M;

            int ap_count = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                DataRow dr = dt.Rows[i];

                DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
	 
	from 
	tb_product_shipping_fee 
	where prod_sku='{0}' and is_system='{1}'", dr["product_serial_no"].ToString(), dr["product_serial_no"].ToString().Length == 8 ? 1 : 0));

                 int count = int.Parse(dr["cart_temp_Quantity"].ToString());
                 _state_shipping = int.Parse(dr["state_shipping"].ToString());
                    
                if (sales_promotion_dt.Rows.Count == 0 || Config.sale_promotion_compay_id.IndexOf("["+shippingCompany+"]") == -1)
                {
                  
                    AccountProduct model = new AccountProduct();
                    model.product_id = int.Parse(dr["product_serial_no"].ToString());
                    model.shipping_company_id = shippingCompany;
                    model.price = decimal.Parse(dr["price"].ToString());
                    model.sum = count;
                    
                    if (dr["product_serial_no"].ToString().Length == 8)
                    {
                        model.product_cate = dr["is_noebook"].ToString() == "1" ? product_category.noebooks : product_category.system_product;
                        model.product_size = AccountHelper.GetSystemSize(model.price, product_category.system_product);
                    }
                    else if (dr["is_noebook"].ToString() == "1")
                    {
                        model.product_cate = product_category.noebooks;
                        model.product_size = AccountHelper.GetSystemSize(model.price, product_category.noebooks);
                    }
                    else
                    {
                        ProductModel pm = ProductModel.GetProductModel(int.Parse(dr["product_serial_no"].ToString()));
                        model.product_cate = product_category.part_product;
                        model.product_size = pm.product_size_id;
                    }
                   
                    ap[ap_count] = model;
                    ap_count += 1;

                    _price_sum += model.price * count;
                    // for (int j = 0; j < int.Parse(dr["cart_temp_Quantity"].ToString()); j++)
                    //{}
                }
                else
                {
                    decimal sale_promotion_shipping_charge_single = 0M;

                     DataTable shippingCompDt = Config.ExecuteDataTable(string.Format(@"select system_category from tb_shipping_company where shipping_company_id='{0}'", shippingCompany));
                    if (shippingCompDt.Rows.Count > 0)
                    {
                        if (shippingCompDt.Rows[0][0].ToString() == "1")
                        {
                            // ca
                            decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_ca"].ToString(), out sale_promotion_shipping_charge_single);
                        }
                        else
                        {
                            // us
                            decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_us"].ToString(), out sale_promotion_shipping_charge_single);
                        }
                    }
                    //Response.Write(sale_promotion_charge.ToString());
                    sale_promotion_charge += sale_promotion_shipping_charge_single * count;
                    //Response.Write(ap_real_count.ToString());
                }
            }


            try
            {
                if (_state_shipping == -1 || _state_shipping == 0)
                {
                    error_msg = "";
                    throw new Exception("State is not exist");
                }
                if (ap_real_count>0)
                {
                    Account a = new Account(ap, _state_shipping);

                    Response.Write(Config.ConvertPrice(a.getResult() + sale_promotion_charge));
                    if (ParentRadio.Length > 0)
                        Response.Write("<script> parent.window.document.getElementById('" + this.ParentRadio + "').disabled = false;</script>");
                }
                else
                {
                    Response.Write(Config.ConvertPrice(sale_promotion_charge));
                    if (ParentRadio.Length > 0)
                        Response.Write("<script> parent.window.document.getElementById('" + this.ParentRadio + "').disabled = false;</script>");
                }
            }
            catch
            {
                Response.Write(error_msg);
                if (ParentRadio.Length > 0)
                    Response.Write("<script> parent.window.document.getElementById('" + this.ParentRadio + "').disabled = true;</script>");
            }
            //Response.End();
    }
   
    public int TmpCode
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "tmp_code", -1);
        }
    }

    public int ShippingCompany
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shipping_company", -1); }
    }

    public string ParentRadio
    {
        get { return Util.GetStringSafeFromQueryString(Page, "parent_radio"); }
    }
}
