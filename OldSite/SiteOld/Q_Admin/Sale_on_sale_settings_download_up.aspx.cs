using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_Sale_on_sale_settings_download_up : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Write("此模块关闭.");
            Response.End();
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
    }

    #region date controls
    protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsToday)
        {
            e.Cell.ForeColor = System.Drawing.Color.Blue;
            e.Cell.BackColor = System.Drawing.Color.Pink;
        }
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Calendar c = (Calendar)sender;
        this.txt_begin_date.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsToday)
        {
            e.Cell.ForeColor = System.Drawing.Color.Blue;
            e.Cell.BackColor = System.Drawing.Color.Pink;
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Calendar c = (Calendar)sender;
        this.txt_end_date.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    }
    #endregion
    protected void btn_download_Click(object sender, EventArgs e)
    {
        if (txt_begin_date.Text.Length > 0 && txt_end_date.Text.Length > 0
            && this.CategoryDropDownLoad1.categoryId > 0)
        {
            DateTime begin_date;
            DateTime.TryParse(this.txt_begin_date.Text, out begin_date);

            DateTime end_date;
            DateTime.TryParse(this.txt_end_date.Text, out end_date);
            LtdHelper lh = new LtdHelper();
            DataTable dt = Config.ExecuteDataTable(string.Format(@"select product_serial_no lu_sku, 
case when split_line=1 then product_short_name 
else product_name end as middle_name 
, product_order priority
, case when split_line=1 then 'Y' 
else 'N' end as split_line ,product_name_long_en long_name, {3}, {4}, product_current_cost cost, product_current_price price
,(product_current_special_cash_price-product_current_discount) on_sale,product_current_special_cash_price Special_Cash_Price, {1}, {2} from tb_product p where tag=1 and is_non=0
and menu_child_serial_no='{0}' order by product_order asc ", this.CategoryDropDownLoad1.categoryId
                                                           , "'"+ begin_date.ToString("yyyy-MM-dd") + "' begin_date"
                                                           , "'"+ end_date.ToString("yyyy-MM-dd")+"' end_date"
                                                           , @"
(select max(oi.other_inc_sku) supercom_cost from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi on oi.other_inc_id=ol.other_inc_type and oi.other_inc_sku=ol.other_inc_sku where lu_sku=p.product_serial_no and oi.other_inc_id='"+lh.LtdHelperValue(Ltd.wholesaler_supercom)+@"' ) supercom_sku 
,
(select  max(other_inc_price) supercom_cost from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi on oi.other_inc_id=ol.other_inc_type and oi.other_inc_sku=ol.other_inc_sku where lu_sku=p.product_serial_no and oi.other_inc_id='" + lh.LtdHelperValue(Ltd.wholesaler_supercom) + "' ) supercom_cost "
                                                           , "(select max( other_inc_store_sum) supercom_sum from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi on oi.other_inc_id=ol.other_inc_type and oi.other_inc_sku=ol.other_inc_sku where  lu_sku=p.product_serial_no and  oi.other_inc_id='" + lh.LtdHelperValue(Ltd.wholesaler_supercom) + "' ) supercom_sum "
                                                           ));
//            Response.Write(string.Format(@"select product_serial_no lu_sku,product_name middle_name, product_order priority, product_name_long_en long_name, {3}, {4}, product_current_cost cost, product_current_price price
//,(product_current_price-product_current_discount) on_sale,0 Special_Cash_Price, {1}, {2} from tb_product p where tag=1 and split_line=0 and is_non=0
//and menu_child_serial_no='{0}' order by product_order asc ", this.CategoryDropDownLoad1.categoryId
//                                                           , "'" + begin_date.ToString("yyyy-MM-dd") + "' begin_date"
//                                                           , "'" + end_date.ToString("yyyy-MM-dd") + "' end_date"
//                                                           , "(select  max(other_inc_price) supercom_cost from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi on oi.other_inc_id=ol.other_inc_type and oi.other_inc_sku=ol.other_inc_sku where lu_sku=p.product_serial_no) supercom_cost "
//                                                           , "(select max( other_inc_store_sum) supercom_sum from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi on oi.other_inc_id=ol.other_inc_type and oi.other_inc_sku=ol.other_inc_sku where  lu_sku=p.product_serial_no) supercom_sum "
//                                                           ));
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{               
            //    DataRow dr = dt.Rows[i];
            //     decimal sale_price;
            //    decimal.TryParse(dr["on_sale"].ToString(), out sale_price);
            //    dr["Special_Cash_Price"] = ConvertPrice.ChangePriceToNotCard(sale_price);
            //}
            ExcelHelper eh = new ExcelHelper(dt);
            eh.MaxRecords = 50000;
            eh.FileName = Config.ExecuteScalar("select menu_child_name from tb_product_category where menu_child_serial_no='" + this.CategoryDropDownLoad1.categoryId.ToString() + "'").ToString()+ ".xls";
            eh.Export();
        }
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (this.fileupload1.PostedFile != null)
        {
            try
            {
                int error_count = 0;
                int success_count = 0;
                string newFilename = Server.MapPath(string.Format("{0}{1}",
                  Config.update_product_data_excel_path,
                  string.Format("cost_store_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                this.fileupload1.PostedFile.SaveAs(newFilename);
                using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(newFilename)))
                {
                    conn.Open();
                    // 
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            DataRow dr = dt.Rows[i];
                            if (dr["split_line"].ToString() == "N")
                            {
                                int lu_sku;
                                decimal Special_Cash_Price;
                                decimal cost;
                                decimal on_sale;

                                decimal.TryParse(dr["Special_Cash_Price"].ToString(), out Special_Cash_Price);

                                decimal price;
                                price = ConvertPrice.SpecialCashPriceConvertToCardPrice(Special_Cash_Price);

                                decimal.TryParse(dr["cost"].ToString(), out cost);
                                decimal.TryParse(dr["on_sale"].ToString(), out on_sale);
                                on_sale = ConvertPrice.SpecialCashPriceConvertToCardPrice(on_sale);
                                int.TryParse(dr["lu_sku"].ToString(), out lu_sku);

                                int priority;
                                int.TryParse(dr["priority"].ToString(), out priority);

                                DateTime begin_date;
                                DateTime end_date;
                                DateTime.TryParse(dr["begin_date"].ToString(), out begin_date);
                                DateTime.TryParse(dr["end_date"].ToString(), out end_date);

                                decimal discount = price - on_sale;

                                OnSaleModel osm = new OnSaleModel();
                                osm.begin_datetime = begin_date;
                                osm.end_datetime = end_date;
                                osm.modify_datetime = DateTime.Now;
                                osm.product_serial_no = lu_sku;



                                if (on_sale < Special_Cash_Price )
                                {
                                    Config.ExecuteNonQuery(string.Format(@"
delete  from tb_on_sale where product_serial_no='{0}'; 
", lu_sku));
                                    osm.price = price;
                                    osm.sale_price = on_sale;
                                    osm.save_price = discount;
                                    osm.cost = cost;
                                    osm.Create();

                                    ProductModel pm = ProductModel.GetProductModel(lu_sku);
                                    pm.product_current_special_cash_price = Special_Cash_Price;
                                    pm.product_current_cost = cost;
                                    pm.product_name = dr["middle_name"].ToString();
                                    pm.product_current_discount = discount;
                                    pm.product_current_price = price;
                                    pm.product_order = priority;
                                    pm.Update();
                                }
                                else
                                {
                                    ProductModel pm = ProductModel.GetProductModel(lu_sku);
                                    pm.product_current_special_cash_price = Special_Cash_Price;
                                    pm.product_current_cost = cost;
                                    pm.product_name = dr["middle_name"].ToString();
                                    pm.product_current_discount = 0;
                                    pm.product_current_price = price;
                                    pm.product_order = priority;
                                    pm.Update();
                                    Config.ExecuteNonQuery(string.Format(@"
delete  from tb_on_sale where product_serial_no='{0}'; 
", lu_sku));
                                }

                                success_count += 1;
                            }
                        }
                        catch { error_count += 1; }

                       
                    }
                    InsertTraceInfo(string.Format("Upadte on sale file : {0}", this.fileupload1.FileName));
                    CH.Alert(success_count + " Upload Success " + (error_count != 0 ? " <br> " + error_count + " Create Success" : ""), this.Literal1);

                    //this.GridView1.DataSource = dt;
                    //this.DataBind();
                }
                System.IO.FileInfo fi = new System.IO.FileInfo(newFilename);
                fi.Delete();
            }
            catch (Exception ex)
            {
                CH.Alert(ex.Message, this.Literal1);
            }
        }
    }
}
