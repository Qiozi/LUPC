using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_other_inc_supercom_edit : PageBase
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
        // load
        //
        if (ReqID != -1)
        {
            DataTable dt = Config.ExecuteDataTable("select * from tb_supercom_store_2 where id='" + ReqID.ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txt_cost.Text = dr["f2"].ToString();
                this.txt_long_name.Text = dr["f1"].ToString() + dr["f7"].ToString() + dr["f8"].ToString();
                this.txt_manufactory_name.Text = dr["f6"].ToString();
                this.txt_mfp_code.Text = dr["f0"].ToString();
                this.txt_middle_name.Text = dr["f1"].ToString();
                decimal cost;
                decimal price;
                decimal.TryParse(this.txt_cost.Text, out cost);
                price = decimal.Parse((cost * 1.122M).ToString("###.00"));
                this.txt_special_cash_price.Text = price.ToString();
                this.txt_short_name.Text = dr["f6"].ToString().Trim() + " " + dr["f9"].ToString().Replace("Laptops", "NoteBook").Trim() +" " + dr["f3"].ToString();
                this.txt_supercom_sku.Text = dr["f3"].ToString();
            }
        }

        this.ddl_product_size.DataSource = Config.ExecuteDataTable("Select * from tb_product_size");
        this.ddl_product_size.DataTextField = "product_size_name";
        this.ddl_product_size.DataValueField = "product_size_id";
        this.ddl_product_size.DataBind();
    }


    public int ReqID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (this.CategoryDropDownLoad1.categoryId > 0)
        {

            string mfp_code = this.txt_mfp_code.Text.Trim();
            if (mfp_code.Length > 0)
            {
                DataTable dt = Config.ExecuteDataTable("select product_serial_no c from tb_product where manufacturer_part_number='" + mfp_code + "'");
                if (dt.Rows.Count > 0)
                {
                    CH.Alert("it is exist DB.<br/>luc sku: " + dt.Rows[0][0].ToString(), this.Literal1);
                    return;
                }                
            }

            decimal cost;
            decimal price;
            decimal special_cash_price;
            decimal.TryParse(this.txt_cost.Text, out cost);
            int order;
            int.TryParse(this.txt_priority.Text.Trim(), out order);
            decimal.TryParse(this.txt_special_cash_price.Text, out special_cash_price);
            price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cash_price);

            ProductModel pm = new ProductModel();
            pm.menu_child_serial_no = this.CategoryDropDownLoad1.categoryId;
            pm.product_short_name = this.txt_short_name.Text.Trim();
            pm.product_name = this.txt_middle_name.Text.Trim();
            pm.product_img_sum = 1;
            pm.product_name_long_en = this.txt_long_name.Text.Trim();
            pm.product_order = order;
            pm.is_non = 0;
            pm.last_regdate = DateTime.Now;
            pm.manufacturer_part_number = this.txt_mfp_code.Text.Trim();
            pm.producter_serial_no = this.txt_manufactory_name.Text.Trim();
            pm.new_product = 1;
            pm.other_product_sku = 999999;
            pm.export = true;
            pm.product_current_cost = cost;
            pm.product_current_price = price;
            pm.product_current_special_cash_price = ConvertPrice.ChangePriceToNotCard(price);
            pm.regdate = DateTime.Now;
            pm.split_line = 0;
            pm.tag = 0;
            pm.issue = false;
            pm.product_size_id = int.Parse(this.ddl_product_size.SelectedValue.ToString());
            pm.Create();
            Config.ExecuteNonQuery(@"insert into tb_other_inc_match_lu_sku 
	( lu_sku, other_inc_sku, other_inc_type)
	values
	( '" + pm.product_serial_no.ToString() + "', '" + this.txt_supercom_sku.Text + "', 2)");

            InsertTraceInfo("Insert new part to IssueStore: " + pm.product_serial_no.ToString());
            this.CH.RunJavaScript("window.close();", this.Literal1);
        }
    }
}
