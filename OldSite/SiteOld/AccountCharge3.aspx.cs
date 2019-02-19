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

public partial class AccountCharge3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            accountCharge();
        }
        catch { }
    }


    public void accountCharge()
    {

        string error_msg = "Service not available.";
      
        AccountProduct[] ap = new AccountProduct[1] ;
        int _state_shipping = -1;
        decimal _price_sum = 0;
        //decimal _sale_tax = 0;
        //decimal result = 0;
        int count = 1;

        if (product_type == product_category.part_product || product_type == product_category.noebooks)
        {

            ProductModel p = ProductModel.GetProductModel(this.product_id);

           
            AccountProduct model = new AccountProduct();
            model.product_id = this.product_id;
            model.shipping_company_id = ShippingCompany;
            model.price = p.product_current_price * Config.is_card_rate;
            model.sum = count;

            model.product_cate = IsNoebook == 1 ? product_category.noebooks : product_category.part_product;
            if (IsNoebook == 1)
            {
                model.product_size = AccountHelper.GetSystemSize(model.price, product_category.noebooks); ;
            }
            else
            {
                model.product_size = p.product_size_id;
            }
           
            ap[0] = model;
            _state_shipping = ShippingState;
            _price_sum += model.price * count;

        }
        if (product_type == product_category.system_product)
        {
          // system 产品暂时不做


            //if (dr["product_serial_no"].ToString().Length == 8)
            //{
            //    model.product_cate = dr["is_noebook"].ToString() == "1" ? product_category.noebooks : product_category.system_product;
            //    model.product_size = AccountHelper.GetSystemSize(model.price, product_category.system_product);
            //}
        }

        try
        {
            DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
	 
	from 
	tb_product_shipping_fee 
	where prod_sku='{0}' and is_system='{1}'", product_id, product_type == product_category.system_product ? 1 : 0));
            if (sales_promotion_dt.Rows.Count > 0)
            {
                //Account a = new Account(ap, ShippingState);
                this.lbl_price.Text = Config.ConvertPrice(decimal.Parse(sales_promotion_dt.Rows[0]["shipping_fee_ca"].ToString())).ToString();
            }
            else
            {

                Account a = new Account(ap, ShippingState);
                this.lbl_price.Text = Config.ConvertPrice(a.getResult()).ToString();
            }
        }
        catch
        {
            this.lbl_price.Text = error_msg;
        }
  

    }

    public int product_id
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "product_id", -1);
        }
    }

    public int ShippingCompany
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shipping_company", -1); }
    }

    public product_category product_type
    {
        get { return Product_category_helper.GetProductCategoryByValue(Util.GetInt32SafeFromQueryString(Page, "product_type", 999)); }
    }

    public int IsNoebook
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "IsNoebook", -1); }
    }

    public int ShippingState
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "ShippingState", -1); }
    }
}
