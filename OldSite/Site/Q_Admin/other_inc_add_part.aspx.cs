using LU.Data;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_other_inc_add_part : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 
            // load
            //
            if (ReqID != -1)
            {
                //DataTable dt = Config.ExecuteDataTable("select * from tb_supercom_store_2 where id='" + ID.ToString() + "'");
                //if (dt.Rows.Count > 0)
                //{
                //    DataRow dr = dt.Rows[0];
                //    this.txt_cost.Text = dr["f2"].ToString();
                //    this.txt_long_name.Text = dr["f1"].ToString() + dr["f7"].ToString() + dr["f8"].ToString();
                //    this.txt_manufactory_name.Text = dr["f6"].ToString();
                //    this.txt_mfp_code.Text = dr["f0"].ToString();
                //    this.txt_middle_name.Text = dr["f1"].ToString();
                //    decimal cost;
                //    decimal price;
                //    decimal.TryParse(this.txt_cost.Text, out cost);
                //    price = cost * 1.122M;
                //    this.txt_special_cash_price.Text = price.ToString();
                //    this.txt_short_name.Text = dr["f6"].ToString().Trim() + " " + dr["f9"].ToString().Replace("Laptops", "NoteBook").Trim() + " " + dr["f3"].ToString();
                //    this.txt_supercom_sku.Text = dr["f3"].ToString();
                //}
            }

            this.ddl_product_size.DataSource = Config.ExecuteDataTable("Select * from tb_product_size");
            this.ddl_product_size.DataTextField = "product_size_name";
            this.ddl_product_size.DataValueField = "product_size_id";
            this.ddl_product_size.DataBind();

            this.ddl_other_inc.DataSource = new LtdHelper().LtdHelperWholesalerToDT();
            this.ddl_other_inc.DataTextField = "text";
            this.ddl_other_inc.DataValueField = "id";
            this.ddl_other_inc.DataBind();
        }
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

            int other_inc_id;
            int.TryParse(this.ddl_other_inc.SelectedValue.ToString(), out other_inc_id);


            price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cash_price);

            var pm = new tb_product();// ProductModel();
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
            pm.@new = 1;
            pm.other_product_sku = 999999;
            pm.export = true;
            pm.product_current_cost = cost;
            pm.product_current_price = price;
            pm.product_current_special_cash_price = special_cash_price;
            pm.regdate = DateTime.Now;
            pm.split_line = 0;
            pm.tag = 0;
            pm.issue = false;
            pm.product_size_id = int.Parse(this.ddl_product_size.SelectedValue.ToString());
            DBContext.tb_product.Add(pm);
            DBContext.SaveChanges();

            Config.ExecuteNonQuery(@"insert into tb_other_inc_match_lu_sku 
	( lu_sku, other_inc_sku, other_inc_type)
	values
	( '" + pm.product_serial_no.ToString() + "', '" + this.txt_other_inc_sku.Text + "', '" + other_inc_id.ToString() + "')");

            InsertTraceInfo(DBContext, "Insert new part to IssueStore: " + pm.product_serial_no.ToString());
            //this.CH.RunJavaScript("window.close();", this.Literal1);
            this.txt_other_inc_sku.Text = "";
            this.txt_priority.Text = "";
            this.txt_mfp_code.Text = "";
            this.txt_cost.Text = "";
            CH.Alert(pm.product_serial_no.ToString(), this.Literal1);
        }
    }
}
