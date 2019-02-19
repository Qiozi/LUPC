using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_other_inc_asi_edit : PageBase
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
            DataTable dt = Config.ExecuteDataTable(@"select price cost, description long_name, itmeid mfp, vendor mfp_name, asi_sku, 
(select subCatname from tb_asi_category where subcatcode= ass.sub_category) cate_name
 from  tb_asi_store_new ass where id = '"+ID.ToString()+"'");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txt_cost.Text = dr["cost"].ToString();
                this.txt_long_name.Text = dr["long_name"].ToString();
                this.txt_manufactory_name.Text = dr["mfp_name"].ToString();
                this.txt_mfp_code.Text = dr["mfp"].ToString();
                this.txt_middle_name.Text = dr["mfp_name"].ToString() + " " + dr["cate_name"].ToString() + " " + dr["mfp"].ToString();
                decimal cost;
                decimal price;
                decimal.TryParse(this.txt_cost.Text, out cost);
                price = decimal.Parse((cost * 1.122M).ToString("###.00"));
                this.txt_special_cash_price.Text = price.ToString("###.00");
                this.txt_short_name.Text = this.txt_middle_name.Text;
                this.txt_supercom_sku.Text = dr["asi_sku"].ToString();
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
	( '" + pm.product_serial_no.ToString() + "', '" + this.txt_supercom_sku.Text + "', " + new LtdHelper().LtdHelperValue(Ltd.wholesaler_asi).ToString() + ")");

            InsertTraceInfo("Insert new part to IssueStore: " + pm.product_serial_no.ToString());
            this.CH.RunJavaScript("window.close();", this.Literal1);
        }
    }
}
