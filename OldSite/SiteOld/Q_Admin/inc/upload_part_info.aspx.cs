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

public partial class Q_Admin_inc_upload_part_info : PageBase
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
    }

    protected void btn_upload_Click(object sender, EventArgs e)
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
                    //, [manufacturer_part_number] , [supplier_sku], [priority],[hot], [new], [split_line]
                    //, [long_name], [img_sum], [keywords], [other_product_sku], [export],[cost],[special_cost_price]
                    OleDbDataAdapter da = new OleDbDataAdapter(@"select * FROM [table$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    conn.Close();

                    // insert data to Database Server
                    ProductModel pmm = new ProductModel();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb_pg = new System.Text.StringBuilder();
                    sb_pg.Append("<table>");
                    int success_sum = 0;
                    int error_sum = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int sku;
                        int.TryParse(dr["sku"].ToString(), out sku);
                        //
                        // if comment file is not exist, then create.
                        //
                        CreateNullCommentFile(sku);

                        bool is_new_part = false;

                        try
                        {
                            decimal cost;
                            if (dr["cost"].ToString() == "")
                                cost = 0M;
                            else
                                decimal.TryParse(dr["cost"].ToString(), out cost);

                            decimal special_cost_price;
                            decimal price;
                            decimal.TryParse(dr["special_cost_price"].ToString(), out special_cost_price);
                            price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cost_price);
                            special_cost_price = ConvertPrice.ChangePriceToNotCard(price);

                            ProductModel pm = ProductModel.GetProductModel(sku);

                            is_new_part = Config.ExecuteScalarInt32("select count(product_serial_no) from tb_product where manufacturer_part_number='" + dr["manufacturer_part_number"].ToString() + "'") == 0;

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

                            // modify group info
                            string checkbox_str = "";
                            for (int j = 0; j < PGM.Length; j++)
                            {
                                if (pm.menu_child_serial_no == PGM[j].product_category)
                                {
                                    checkbox_str += "<div class='part_group_area'><input type='checkbox' name='part_check' value='" + PGM[j].part_group_id.ToString() + "' tag='" + pm.product_serial_no.ToString() + "'><span onclick='pressGroupName(this);'>" + PGM[j].part_group_comment + "</span></div>";
                                }
                            }

                            if (checkbox_str != "")
                            {
                                DataTable exist_pg = Config.ExecuteDataTable("SELECT part_group_id FROM tb_part_group_detail WHERE product_serial_no = '"+ pm.product_serial_no.ToString()+"'");
                                bool is_show = false;
                                if (this.radio_cmd.SelectedValue.ToString() == "2" )
                                {
                                    if (is_new_part)
                                        is_show = true;
                                }
                                else if(exist_pg.Rows.Count==0)
                                {
                                    is_show = true;
                                }

                                if (is_show)
                                {
                                    sb_pg.Append("<tr>");
                                    sb_pg.Append(string.Format("<td rowspan='2'>{1}<br>[<span name='sku'>{0}</span>]</td>", pm.product_serial_no.ToString()
                                        , "<img src='" + Config.part_img_path + (pm.other_product_sku >0 ? pm.other_product_sku : pm.product_serial_no).ToString() + "_t.jpg' width='50'/>"));
                                    sb_pg.Append(string.Format(@"<td style='border-bottom: 1px dotted #cccccc;'>
                                <a href='/site/product_parts_detail.asp?id={1}&cid={2}' target='_blank'>
                                    {0}
                                </a></td>"
                                        ,pm.product_name + "<br/><i class='long_name'>"+ pm.product_name_long_en+"</i>" 
                                        , pm.product_serial_no
                                        ,pm.menu_child_serial_no));
                                    sb_pg.Append("</tr>");
                                    sb_pg.Append("<tr><td>"+ checkbox_str +"</td></tr>");
                                    sb_pg.Append("<tr><td colspan='2'><hr size='1'></td></tr>");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            sb.Append(" :: " + sku + "[" + ex.Message + "]");
                            error_sum += 1;
                        }
                    }

                    sb_pg.Append("</table>");

                    this.literal_part_list.Text = sb_pg.ToString();
                    CH.RunJavaScript("loadStyle();", this.Literal1);
                    //CH.RunJavaScript("$('#btn_cmd_list').css('display','');", this.Literal1);
                    CH.Alert("Upload " + success_sum.ToString() + " success" + (error_sum == 0 ? "" : string.Format("<br/><span style='font-weight:bold; color:blue'>{0}</span> don't upload:<br/><span style='color:red'>{1}</span>", error_sum, sb.ToString())), this.Literal1);
                    CH.CloseParentWatting(this.FileUpload_edit_part);
                   
                }
                catch (Exception ex)
                {
                    CH.Alert(ex.Message, this.Literal1);
                    CH.CloseParentWatting(this.Literal1);
                }
                System.IO.File.Delete(newFilename);
            }
        }

    }


    public PartGroupModel[] PGM
    {
        get
        {
            if (ViewState["PGM"] == null)
            {
                PartGroupModel[] pgm = PartGroupModel.FindAll();
                ViewState["PGM"] = pgm;
                return pgm;
            }

            return (PartGroupModel[])ViewState["PGM"];
        }
    }

    public void CreateNullCommentFile(int sku)
    {
        string file_name = string.Format("{0}{1}_comment.html", Server.MapPath(Config.Part_Comment_Path), sku);
        if (!System.IO.File.Exists(file_name))
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(file_name);
            sw.Write("");
            sw.Close();
            sw.Dispose();
        }
    }
}
