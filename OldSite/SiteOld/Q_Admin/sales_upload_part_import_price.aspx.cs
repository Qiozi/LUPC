using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.IO;

using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_sales_upload_part_import_price : PageBase
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
        
        BindPartLV();
    }

    private void BindPartLV()
    {
        DataTable dt = Config.ExecuteDataTable(@"select pi.*,ifnull(p.product_current_price-product_current_discount, 0) current_sell, ifnull(p.product_current_cost,0) current_cost, 
ifnull(pi.part_real_cost-p.product_current_cost, 0) price_difference, p.menu_child_serial_no from tb_product_import_price  pi left join tb_product p on p.product_serial_no=pi.luc_sku where date_format(part_real_regdate, '%b-%d-%Y') = date_format(now(), '%b-%d-%Y')");
        this.lv_part_list.DataSource = dt;
        this.lv_part_list.DataBind();
        
    }

    private void UploadPartList(bool is_order)
    {
        FileUpload fu = is_order ? this.FileUpload1 : this.FileUpload2;
        if (fu.PostedFile != null)
        {
            string newFilename = Server.MapPath(string.Format("{0}{1}",
              Config.update_product_data_excel_path,
              string.Format("cost_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
            fu.PostedFile.SaveAs(newFilename);

            using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(newFilename)))
            {
                conn.Open();
                // [Ltd_code],[Ltd_sku],[Ltd_cost],[Ltd_stock],[Ltd_manufacture_code],[Ltd_part_name]
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow dr = dt.Rows[i];
                    DateTime part_real_regdate = DateTime.Now;
                    string part_name = dr["name"].ToString().Trim();
                    string invoices = is_order ? dr["vendor_invoice"].ToString() : System.DBNull .Value.ToString();
                    string order_codes = is_order ? dr["order_codes"].ToString() : System.DBNull.Value.ToString();
                    //Response.Write(dr["invoices"].ToString() + "<br>");
                    string note = dr["note"].ToString();
                    string other_inc_name = dr["inc"].ToString();
                    int part_quantity;
                    int.TryParse(dr["quantity"].ToString(), out part_quantity);
                    decimal part_sell;
                    decimal.TryParse(dr["sell"].ToString(), out part_sell);
                    decimal part_real_cost;
                    decimal.TryParse(dr["cost"].ToString(), out part_real_cost);
                    int luc_sku;
                    int.TryParse(dr["sku"].ToString(), out luc_sku);

                    if (part_name.Length > 1)
                    {
                        ProductImportPriceModel pipm = new ProductImportPriceModel();
                        pipm.vendor_invoice = invoices;
                        pipm.luc_sku = luc_sku;
                        pipm.note = note;
                        pipm.order_codes = order_codes;
                        pipm.other_inc_name = other_inc_name;
                        pipm.part_name = part_name;
                        pipm.part_quantity = part_quantity;
                        pipm.part_real_cost = part_real_cost;
                        pipm.part_real_regdate = part_real_regdate;
                        pipm.part_sell = part_sell;
                        pipm.is_order = is_order;
                        pipm.Create();

                        if (luc_sku > 0)
                        {
                            Config.ExecuteNonQuery("update tb_product set product_current_real_cost = '" + part_real_cost.ToString() + "' , real_cost_regdate=now() where product_serial_no='" + luc_sku.ToString() + "'");
                        }
                    }
                }
            }

            File.Delete(newFilename);
            BindPartLV();
        }

    }

    protected void btn_upload_order_code_Click(object sender, EventArgs e)
    {
        UploadPartList(true);

    }
    protected void btn_upload_no_order_Click(object sender, EventArgs e)
    {
        UploadPartList(false);
    }
    protected void lv_part_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteRecord":
                Config.ExecuteNonQuery("delete from tb_product_import_price   where id='" + e.CommandArgument.ToString() + "'");
                BindPartLV();
                break;
        }
    }
    protected void lv_part_list_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label _lbl_luc_sku =(Label) e.Item.FindControl("_lbl_luc_sku");
        if (_lbl_luc_sku.Text == "0")
            _lbl_luc_sku.Text = "";

        Label _lbl_current_cost = (Label)e.Item.FindControl("_lbl_current_cost");
        decimal current_cost;
        decimal.TryParse(_lbl_current_cost.Text, out current_cost);
        if (current_cost == 0M)
            _lbl_current_cost.Text = "";

        Label _lbl_current_sell = (Label)e.Item.FindControl("_lbl_current_sell");
        decimal current_sell;
        decimal.TryParse(_lbl_current_sell.Text, out current_sell);
        if (current_sell == 0M)
            _lbl_current_sell.Text = "";

        Label _lbl_price_defference= (Label)e.Item.FindControl("_lbl_price_defference");
        decimal price_defference;
        decimal.TryParse(_lbl_price_defference.Text, out price_defference);

        if (price_defference > 10M)
        {
            _lbl_price_defference.ForeColor = System.Drawing.Color.Green;
            _lbl_price_defference.Font.Bold = true;
        }
        else if (price_defference == 0M)
            _lbl_price_defference.Text = "";
        else
        {
            _lbl_price_defference.ForeColor = System.Drawing.Color.Red;
            _lbl_price_defference.Font.Bold = true;
        }

        HiddenField _hf_menu_child_serial_no = (HiddenField)e.Item.FindControl("_hf_menu_child_serial_no");
        int menu_child_serial_no;
        int.TryParse(_hf_menu_child_serial_no.Value, out menu_child_serial_no);

        int luc_sku;
        int.TryParse(_lbl_luc_sku.Text, out luc_sku);
        if(luc_sku >0)
           ( (Literal)e.Item.FindControl("_lb_shopbot")).Text = string.Format(@"<a href=""/q_admin/other_inc_view_compare.aspx?categoryid={0}&id={1}""  
onclick=""winOpen(this.href, 'shopbotView', 880, 800, 120, 200);return false;"" title=""Modify Detail"">Shopbot</a>", menu_child_serial_no, luc_sku);
    }
    protected void btn_change_price_Click(object sender, EventArgs e)
    {
        //try
        {
            object o = Config.ExecuteScalar(@"insert into nicklu2.tb_other_inc_bind_price_tmp 
	( product_serial_no, category_id,  
	other_inc_price, 
	other_inc_store_sum, 	 
	product_current_price, 
	product_current_price_tmp, 
	product_current_cost, 
	bind_type
	)
select p.product_serial_no, p.menu_child_serial_no,pip.part_real_cost,pip.part_quantity
,p.product_current_price-p.product_current_discount
, round((p.product_current_price-p.product_current_discount)  * ( pip.part_real_cost/p.product_current_cost), 0)-0.01
,p.product_current_cost, 4   from tb_product p inner join  tb_product_import_price pip 
on pip.luc_sku=p.product_serial_no and pip.part_real_cost<>p.product_current_cost 
 and p.product_current_cost>0 and pip.part_real_cost>0 order by pip.part_real_cost desc;

update tb_product p, tb_product_import_price pip 
set p.product_current_price=round((p.product_current_price-p.product_current_discount)  * ( pip.part_real_cost/p.product_current_cost), 0)-0.01
, p.product_current_cost = pip.part_real_cost 
, p.product_current_special_cash_price = (round((p.product_current_price-p.product_current_discount)  * ( pip.part_real_cost/p.product_current_cost), 0)-0.01) / 1.022
where p.product_serial_no=pip.luc_sku  and p.product_current_cost>0 and pip.part_real_cost>0 ;
select ROW_COUNT();");

            CH.Alert("更新数量: "+o.ToString(), this.Literal1);
        }
    }
}
