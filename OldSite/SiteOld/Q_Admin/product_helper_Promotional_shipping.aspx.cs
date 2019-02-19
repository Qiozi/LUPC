using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_product_helper_Promotional_shipping : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        //
        BindLV();
    }

    #region BindLV
    private void BindLV()
    {
        this.lv_sale_promotion_shipping_charge.DataSource = Config.ExecuteDataTable("select ps.*, p.product_name from tb_product_shipping_fee ps inner join tb_product p on p.product_serial_no=ps.prod_sku and is_system=0");
        this.lv_sale_promotion_shipping_charge.DataBind();

    }
    #endregion
    protected void btn_set_store_Click(object sender, EventArgs e)
    {
        int lu_sku;
        int.TryParse(this.txt_lu_sku.Text.Trim(), out lu_sku);

        if (Config.ExecuteScalarInt32("select count(product_serial_no) from tb_product where product_serial_no='" + lu_sku.ToString() + "'") == 0)
        {
            CH.Alert(lu_sku + "&nbsp; &nbsp;<span style='color:red;'>isn't exist</span>", this.lv_sale_promotion_shipping_charge);
        }
        else if (Config.ExecuteScalarInt32("select count(prod_shipping_fee_id) from tb_product_shipping_fee where prod_sku='" + lu_sku.ToString() + "'") > 0)
        {
            CH.Alert(lu_sku + "&nbsp; &nbsp;is exist", this.lv_sale_promotion_shipping_charge);
        }
        else
        {
            ProductShippingFeeModel ps = new ProductShippingFeeModel();
            ps.prod_Sku = lu_sku;
            ps.is_system = false;
            ps.shipping_fee_ca = 7.99M;
            ps.shipping_fee_us = 7.99M;
            ps.Create();
            CH.Alert(KeyFields.SaveIsOK, this.lv_sale_promotion_shipping_charge);
            this.txt_lu_sku.Text = "";
            BindLV();
        }


    }
    protected void lv_sale_promotion_shipping_charge_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            int id;
            int.TryParse(e.CommandArgument.ToString(), out id);
            //CH.Alert(id.ToString(), lv_sale_promotion_shipping_charge);
            switch (e.CommandName)
            {
                case "DeletePart":
                    Config.ExecuteNonQuery("delete from tb_product_shipping_fee where prod_shipping_fee_id='" + id.ToString() + "'");
                    CH.Alert("delete from tb_product_shipping_fee where prod_shipping_fee_id='" + id.ToString() + "'", this.lv_sale_promotion_shipping_charge);
                    BindLV();
                    break;

                case "SavePart":
                    ProductShippingFeeModel ps = ProductShippingFeeModel.GetProductShippingFeeModel(id);
                    decimal price_ca;
                    decimal.TryParse(((TextBox)e.Item.FindControl("_txt_price_ca")).Text.Trim(), out price_ca);

                    decimal price_us;
                    decimal.TryParse(((TextBox)e.Item.FindControl("_txt_price_us")).Text.Trim(), out price_us);

                    ps.shipping_fee_ca = price_ca;
                    ps.shipping_fee_us = price_us;
                    ps.Update();
                    BindLV();

                    break;
            }
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_sale_promotion_shipping_charge);
        }
      
    }
    protected void btn_save_all_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.lv_sale_promotion_shipping_charge.Items.Count; i++)
        {
            int id;
            int.TryParse(((HiddenField)this.lv_sale_promotion_shipping_charge.Items[i].FindControl("_hf_id")).Value, out id);
            if (id > 0)
            {
                ProductShippingFeeModel ps = ProductShippingFeeModel.GetProductShippingFeeModel(id);
                decimal price_ca;
                decimal.TryParse(((TextBox)this.lv_sale_promotion_shipping_charge.Items[i].FindControl("_txt_price_ca")).Text.Trim(), out price_ca);

                decimal price_us;
                decimal.TryParse(((TextBox)this.lv_sale_promotion_shipping_charge.Items[i].FindControl("_txt_price_us")).Text.Trim(), out price_us);

                ps.shipping_fee_ca = price_ca;
                ps.shipping_fee_us = price_us;
                ps.Update();
                
            }
        }
        CH.Alert(KeyFields.SaveIsOK, this.lv_sale_promotion_shipping_charge);
        BindLV();
    }
    protected void btn_remove_all_Click(object sender, EventArgs e)
    {
        Config.ExecuteNonQuery("delete from tb_product_shipping_fee");
        BindLV();
    }
}
