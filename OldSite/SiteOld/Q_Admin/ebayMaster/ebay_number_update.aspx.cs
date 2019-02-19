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
using System.Data.OleDb;

public partial class Q_Admin_ebayMaster_ebay_number_update : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            int improt_count = 0;
            UpdateToDB(ref improt_count);
        }
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.FileUpload1.PostedFile != null)
            {
                string newFilename = Server.MapPath(string.Format("{0}{1}",
                  Config.update_product_data_excel_path,
                  string.Format("e_turbo_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                this.FileUpload1.PostedFile.SaveAs(newFilename);

                //DataTable errDT = new DataTable();
                //errDT.Columns.Add("number");
                //errDT.Columns.Add("err_info");

                using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(newFilename)))
                {
                    conn.Open();
                    // [Ltd_code],[Ltd_sku],[Ltd_cost],[Ltd_stock],[Ltd_manufacture_code],[Ltd_part_name]
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();
                    int success_count = 0;
                    int error_count = 0;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        try
                        {

                            string item_id = dr["item id"].ToString();

                            if (Config.ExecuteScalarInt32("Select count(id) from tb_ebay_turbolist where item_id='" + item_id + "'") == 0)
                            {
                            EbayTurbolistModel etm = new EbayTurbolistModel();

                            if (dr["available quantity"].ToString() != "")
                            {
                                int available_quantity;
                                int.TryParse(dr["available quantity"].ToString(), out available_quantity);
                                etm.available_quantity = available_quantity;
                            }

                            etm.custom_label = dr["custom label"].ToString();
                            etm.format_type = dr["format"].ToString();
                            if (dr["handling cost"].ToString() != "")
                            {
                                string handling_cost = dr["handling cost"].ToString();
                                if (handling_cost.IndexOf("C") != -1)
                                {
                                    decimal handling_cost_p;
                                    decimal.TryParse(handling_cost.Replace("C$", ""), out handling_cost_p);
                                    etm.handling_cost = handling_cost_p;
                                    etm.handling_cost_unit = "CAD";
                                }
                                if (handling_cost.IndexOf("US") != -1)
                                {
                                    decimal handling_cost_p;
                                    decimal.TryParse(handling_cost.Replace("US$", ""), out handling_cost_p);
                                    etm.handling_cost = handling_cost_p;
                                    etm.handling_cost_unit = "USD";
                                }
                            }
                                etm.item_id = item_id;
                            etm.item_title = dr["item title"].ToString();

                            if (dr["sale price"].ToString() != "")
                            {
                                decimal sale_price;
                                decimal.TryParse(dr["sale price"].ToString(), out sale_price);
                                etm.sale_price = sale_price;
                            }

                            if (dr["shipping cost"].ToString() != "")
                            {
                                string shipping_cost = dr["shipping cost"].ToString();
                                if (shipping_cost.IndexOf("C") != -1)
                                {
                                    decimal shipping_cost_p;
                                    decimal.TryParse(shipping_cost.Replace("C$", ""), out shipping_cost_p);
                                    etm.shipping_cost = shipping_cost_p;
                                    etm.shipping_cost_unit = "CAD";
                                }
                                if (shipping_cost.IndexOf("US") != -1)
                                {
                                    decimal shipping_cost_p;
                                    decimal.TryParse(shipping_cost.Replace("US$", ""), out shipping_cost_p);
                                    etm.shipping_cost = shipping_cost_p;
                                    etm.shipping_cost_unit = "USD";
                                }
                            }

                            //etm.shipping_service = dr["shipping service"].ToString();
                            etm.shipping_type = dr["shipping type"].ToString();
                            etm.sold_status = dr["sold status"].ToString();
                            if (dr["start price"].ToString() != "")
                            {
                                string start_price = dr["start price"].ToString();
                                if (start_price.IndexOf("C") != -1)
                                {
                                    decimal start_price_p;
                                    decimal.TryParse(start_price.Replace("C$", ""), out start_price_p);
                                    etm.start_price = start_price_p;
                                    etm.price_unit = "CAD";
                                }
                                if (start_price.IndexOf("US") != -1)
                                {
                                    decimal start_price_p;
                                    decimal.TryParse(start_price.Replace("US$", ""), out start_price_p);
                                    etm.start_price = start_price_p;
                                    etm.price_unit = "USD";
                                }
                            }


                            if (dr["qty"].ToString() != "")
                            {
                                int qty;
                                int.TryParse(dr["qty"].ToString(), out qty);
                                etm.quantity = qty;
                            }
                            etm.sold_status = dr["sold status"].ToString();
                            if (dr["start time"].ToString() != "")
                            {
                                etm.start_time = DateTime.Parse(dr["start time"].ToString());
                            }
                            etm.weight_lbs = dr["weight (lbs)"].ToString();
                            etm.weight_oz = dr["weight (oz)"].ToString();

                            etm.Create();
                            success_count += 1;
                        }
                        }
                        catch (Exception ex)
                        {
                            //CH.Alert(ex.Message, this.literal_comment);
                            sb.Append(string.Format("<tr><td>{0}</td><td>{1}</td></tr>",dr["item_id"].ToString(), ex.Message));
                            return;
                            //error_count += 1;
                        }
                    }
                    int improt_count = 0;
                    System.IO.File.Delete(newFilename);
                    UpdateToDB(ref improt_count);
                    this.literal_comment.Text = string.Format("成功导入: {3} <br/>Success update:    {0}<br/>Error update: {1}<hr><table>{2}</table>", success_count, error_count, sb.ToString(), improt_count);
                    
                }
            }
        }
        catch (Exception ex)
        {
            this.literal_comment.Text = ex.Message;
        }
    }

    private void UpdateToDB(ref int success_count)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select item_id, custom_label, item_number from tb_ebay_turbolist et
	left join tb_ebay_item_number ei on ei.item_number = et.item_id where item_number is null and  sold_status='sold' and custom_label <>''  "));
        for(int i=0; i<dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            string item_number = dr["item_id"].ToString();
            string custom_label = dr["custom_label"].ToString();

            if (custom_label.Length > 1)
            {
                if (custom_label.IndexOf("(") != -1 && custom_label.IndexOf(")") != -1)
                {
                    string str = custom_label.Substring(custom_label.IndexOf("(") + 1, custom_label.IndexOf(")") - custom_label.IndexOf("(") - 1);

                    if (str.Substring(0, 1).ToLower() == "s")
                    {
                        int system_sku;
                        int.TryParse(str.Substring(1), out system_sku);
                        InsertItemNumber(system_sku, item_number,"system", ref  success_count);
                    }
                    else if (str.Substring(0, 1).ToLower() == "p")
                    {
                        int system_sku;
                        int.TryParse(str.Substring(1), out system_sku);
                        InsertItemNumber(system_sku, item_number,"unit", ref  success_count);
                       // Response.Write(system_sku.ToString());
                    }
                }
                else
                {
                    string str = custom_label;
                    if (str.Substring(0, 1).ToLower() == "s")
                    {
                        int system_sku;
                        int.TryParse(str.Substring(1), out system_sku);
                        InsertItemNumber(system_sku, item_number, "system", ref  success_count);
                    }
                    else if (str.Substring(0, 1).ToLower() == "p")
                    {
                        int system_sku;
                        int.TryParse(str.Substring(1), out system_sku);
                        InsertItemNumber(system_sku, item_number, "unit", ref  success_count);
                       // Response.Write(system_sku.ToString());
                    }
                }
            }
        }
    }

    private void InsertItemNumber(int system_sku, string item_number, string item_type, ref int count)
    {
        if (system_sku != 0)
        {
            if (Config.ExecuteScalarInt32("select count(id) from tb_ebay_item_number where item_number='" + system_sku.ToString() + "'") == 0)
            {
                EbayItemNumberModel einm = new EbayItemNumberModel();
                einm.item_number = item_number;
                einm.luc_sku = system_sku;
                einm.luc_type = item_type;
                einm.Create();
                count += 1;
                if (item_type.ToLower() == "system")
                    Config.ExecuteNonQuery("Update tb_ebay_system set ebay_system_current_number='" + item_number + "' where id='" + system_sku.ToString() + "'");
            }
        }
    }
    protected void Button_match_Click(object sender, EventArgs e)
    {
        int success_count = 0;
         UpdateToDB(ref success_count);
        this.literal_comment.Text = success_count.ToString();
    }
}
