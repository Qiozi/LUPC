using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_product_helper_part_import_export : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            CH.CloseParentWatting(this.btn_export_part);
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        this.MenuChildName1.menu_child_serial_no = CategoryID;

        this.literal_modify_price.Text = string.Format("<a href='product_helper_category_coefficient.aspx?categoryID={0}'>Modify</a>", CategoryID);

        ProductCoefficientCategory pccs =new ProductCoefficientCategory();
        ProductCoefficientCategory[] pccss = pccs.FindModelsByCategoryID(CategoryID);
        if (pccss.Length == 1)
            this.lbl_price_coefficient.Text = pccss[0].coefficient.ToString();
        else
            this.lbl_price_coefficient.Text = "0";
    }

    #region porperties
    public int CategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryid", -1); }
    }

    #endregion
    protected void btn_export_part_Click(object sender, EventArgs e)
    {
        try
        {
            ProductModel pm = new ProductModel();
            DateTime datetime = new DateTime();
            int day = 0;
            try
            {
                if (this.txt_regdate.Text.Trim().Length > 0)
                {
                    datetime = DateTime.Parse(this.txt_regdate.Text);
                    DateTime d2 = new DateTime(datetime.Year, 1, 1);
                    day = (datetime.Date - d2.Date).Days + 1;
                }

            }
            catch { throw new Exception("date format is falid"); }
            DataTable dt = pm.ExportTOExcel(CategoryID, this.CheckBox_showit.Checked ? 1 : 0,
               this.txt_regdate.Text.Trim().Length > 0 ? string.Format("{0}{1}", datetime.Year, day) : string.Empty);
            ExcelHelper eh = new ExcelHelper(dt);
            
            eh.FileName = "table.xls";

            eh.MaxRecords = 10000;
            eh.Export();
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.btn_export_part);
        }
    }
    protected void btn_save_new_part_Click(object sender, EventArgs e)
    {
        decimal price_coefficient;
        decimal.TryParse(this.lbl_price_coefficient.Text, out price_coefficient);
        if (price_coefficient == 0M)
        {
            CH.Alert("价格系数错误, 请先修改价格系数再上传", this.FileUpload_edit_part);
            return;
        }
        if (this.fileupload_new_part.PostedFile != null)
        {

            string newFilename = Server.MapPath(string.Format("{0}/{1}",
                Config.update_product_data_excel_path,
                string.Format("new_part_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
            this.fileupload_new_part.PostedFile.SaveAs(newFilename);

            using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(newFilename)))
            {
                try
                {
                    conn.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT [manufacture_number],[short_name],[middle_name],[long_name],[cost],[store_sum],[showit] FROM [table$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    conn.Close();

                    // insert data to Database Server
                    ProductModel pmm = new ProductModel();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    int success_sum = 0;
                    int error_sum = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        string menufacture = dr["manufacture_number"].ToString();

                        if (!pmm.FindPartIsExistByManufacture(menufacture))
                        {
                            ProductModel pm = new ProductModel();
                            pm.export = true;
                            pm.hot = 0;
                            pm.is_non = 0;
                            pm.last_regdate = DateTime.Now;
                            pm.manufacturer_part_number = menufacture;
                            pm.menu_child_serial_no = CategoryID;
                            pm.new_product = 1;
                            pm.product_current_cost = decimal.Parse(dr["cost"].ToString());
                            pm.product_current_price = decimal.Parse(dr["cost"].ToString()) * price_coefficient;
                            pm.product_name = dr["middle_name"].ToString();
                            pm.product_name_long_en = dr["long_name"].ToString();
                            pm.product_order = 1000;//int.Parse(dr["priority"].ToString());
                            pm.product_short_name = dr["short_name"].ToString();
                            pm.product_size_id = 1;// Config.product_size;
                            pm.product_store_sum = int.Parse(dr["store_sum"].ToString());
                            pm.regdate = DateTime.Now;
                            pm.product_img_sum = 1;
                            pm.last_regdate = DateTime.Now;
                            pm.Create();
                            success_sum += 1;
                        }
                        else
                        {
                            sb.Append(" :: " + menufacture);
                            error_sum += 1;
                        }
                    }

                    CH.Alert("Upload " + success_sum.ToString() + " success" + (error_sum  == 0 ? "" : string.Format("<br/><span style='font-weight:bold; color:blue'>{0}</span> is exist, don't upload:<br/><span style='color:red'>{1}</span>",error_sum, sb.ToString())), this.fileupload_new_part);
                }
                catch (Exception ex)
                {
                    CH.Alert(ex.Message, this.fileupload_new_part);
                }
            }

        }
        
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.FileUpload_edit_part.PostedFile != null)
            {
                string newFilename = Server.MapPath(string.Format("{0}/{1}",
                    Config.update_product_data_excel_path,
                    string.Format("edit_part_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                this.FileUpload_edit_part.PostedFile.SaveAs(newFilename);

                using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(newFilename)))
                {
                    try
                    {
                        conn.Open();
                        // [sku], [middle_name], [short_name], [showit], [manufacturer], [manufacturer_url]
    //, [manufacturer_part_number] , [supplier_sku], [priority],[hot], [new], [split_line], [long_name], [img_sum], [keywords], [other_product_sku], [export],[cost],[special_cost_price]
                        OleDbDataAdapter da = new OleDbDataAdapter(@"select * FROM [table$]", conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        conn.Close();

                        // insert data to Database Server
                        ProductModel pmm = new ProductModel();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        int success_sum = 0;
                        int error_sum = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            int sku;
                            int.TryParse(dr["sku"].ToString(), out sku);
                            try
                            {
                                decimal cost ;
                                if (dr["cost"].ToString() == "")
                                    cost = 0M;
                                else
                                    decimal.TryParse(dr["cost"].ToString(), out cost);

                                decimal special_cost_price;
                                decimal price ;
                                decimal.TryParse(dr["special_cost_price"].ToString(), out special_cost_price);
                                price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cost_price);
                                special_cost_price = ConvertPrice.ChangePriceToNotCard(price);

                                ProductModel pm = ProductModel.GetProductModel(sku);
                                pm.product_name = dr["middle_name"].ToString();
                                pm.product_short_name = dr["short_name"].ToString();
                                pm.tag = byte.Parse(dr["showit"].ToString());
                                pm.producter_serial_no = dr["manufacturer"].ToString();
                                pm.producter_url = dr["manufacturer_url"].ToString();
                                pm.manufacturer_part_number = dr["manufacturer_part_number"].ToString();
                                pm.supplier_sku = dr["supplier_sku"].ToString();
                                pm.product_order = int.Parse(dr["priority"].ToString());
                                pm.hot = byte.Parse(dr["hot"].ToString());
                                pm.new_product = byte.Parse(dr["new"].ToString());
                                pm.split_line = byte.Parse(dr["split_line"].ToString());
                                pm.product_name_long_en = dr["long_name"].ToString();
                                pm.product_img_sum = byte.Parse(dr["img_sum"].ToString());
                                pm.keywords = dr["keywords"].ToString();
                                if (dr["other_product_sku"].ToString() == "")
                                    pm.other_product_sku = 999999;
                                else
                                    pm.other_product_sku = int.Parse(dr["other_product_sku"].ToString());
                                pm.export = dr["export"].ToString() == "1";
                                pm.product_current_cost = cost;
                                pm.product_current_special_cash_price = special_cost_price;
                                pm.product_current_price = price;
                                pm.last_regdate = DateTime.Now;
                                if (dr["store_sum"].ToString() != "")
                                {
                                    int store_sum;
                                    int.TryParse(dr["store_sum"].ToString(), out store_sum);
                                    pm.product_store_sum = store_sum;
                                }
                                pm.model = dr["model"].ToString();
                                pm.Update();
                                success_sum += 1;
                            }
                            catch(Exception ex)
                            {
                                sb.Append(" :: " + sku+"["+ex.Message+"]");
                                error_sum += 1;
                            }
                        }

                        CH.Alert("Upload " + success_sum.ToString() + " success" + (error_sum == 0 ? "" : string.Format("<br/><span style='font-weight:bold; color:blue'>{0}</span> don't upload:<br/><span style='color:red'>{1}</span>", error_sum, sb.ToString())), this.fileupload_new_part);
                        CH.CloseParentWatting(this.FileUpload_edit_part);
                    }
                    catch (Exception ex)
                    {
                        CH.Alert(ex.Message, this.fileupload_new_part);
                        CH.CloseParentWatting(this.FileUpload_edit_part);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.FileUpload_edit_part);
            CH.CloseParentWatting(this.FileUpload_edit_part);
        }
    }
    protected void btn_create_new_part_Click(object sender, EventArgs e)
    {
        int part_quantity;
        int.TryParse(this.txt_part_quantity.Text, out part_quantity);
        string skus = "<br/>";
        for (int i = 0; i < part_quantity; i++)
        {
            DataTable dt =  Config.ExecuteDataTable(string.Format(@"Insert into tb_product (tag,other_product_sku,export,new,product_name, issue, split_line, is_non, regdate, last_regdate, menu_child_serial_no)
values 
(0,999999,1,1,'new part.....', 0, 0, 0, now(),now(),'{0}');
select last_insert_id();", CategoryID));
            if (i == 0 && dt.Rows.Count ==1)
            {
                skus = dt.Rows[0][0].ToString() + " ~~";
            }
            if (i == part_quantity - 1 && dt.Rows.Count ==1)
            {
                skus += dt.Rows[0][0].ToString();
            }

        }
        this.literal_new_part.Text = skus;
        CH.Alert(string.Format("OK"), this.Literal1);
    }
}
