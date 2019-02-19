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

public partial class Q_Admin_sale_stat_month_report_2_sub : PageBase
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
        if (OrderHelperID != -1)
        {
            SavePartChecked(OrderHelperID, OrderHelperChecked);
            //Page.RegisterClientScriptBlock("Script1", "<script>alert('Y');</script>");
            
        }
        else
        {
            if (ParentElement == string.Empty)
                ExportToFile(Year, Month);
            else
                GenerateListString();
        }
    }

    private void ExportToFile(int year, int month)
    {


        OrderHelperModel ohm = new OrderHelperModel();
        DataTable dt2 = ohm.FindModelsByMontyExport(Year, Month, true);

        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Order#");
        dt.Columns.Add("AMNT$", typeof(decimal));
        dt.Columns.Add("TAX$", typeof(decimal));
        dt.Columns.Add("PAY");
        dt.Columns.Add("NAME");
        dt.Columns.Add("STATE");
        dt.Columns.Add("CUT#");
        dt.Columns.Add("GST$", typeof(decimal));
        dt.Columns.Add("PST$", typeof(decimal));
        dt.Columns.Add("HST$", typeof(decimal));
        dt.Columns.Add("TAXABLE$", typeof(decimal));


        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            DataRow dr2 = dt2.Rows[i];
            DataRow dr = dt.NewRow();

            dr["ID"] = dr2["order_helper_serial_no"].ToString();
            dr["Order#"] = dr2["order_code"].ToString();
            dr["AMNT$"] = decimal.Parse(dr2["grand_total"].ToString());
            dr["TAX$"] = decimal.Parse(dr2["tax_charge"].ToString());
            dr["PAY"] = dr2["pay_method"].ToString();
            dr["NAME"] = dr2["name"].ToString();
            dr["STATE"] = dr2["customer_shipping_state"].ToString();
            dr["CUT#"] = dr2["customer_serial_no"].ToString();
            dr["GST$"] = decimal.Parse(dr2["gst"].ToString());
            dr["PST$"] = decimal.Parse(dr2["pst"].ToString());
            dr["HST$"] = decimal.Parse(dr2["hst"].ToString());
            dr["TAXABLE$"] = decimal.Parse(dr2["taxable_total"].ToString());
            dt.Rows.Add(dr);           
        }


        ExcelHelper eh = new ExcelHelper(dt);
        eh.MaxRecords = 100000;
        eh.FileName = DateTime.Now.ToString("yyyyMMddhhmmss.xls");
        eh.Export();
    }

    private void SavePartChecked(int id, bool partChecked)
    {
        OrderHelperModel ohm = new OrderHelperModel();
        ohm.ChangeExportValue(id, partChecked);

    }

    private void GenerateListString()
    {
        OrderHelperModel ohm = new OrderHelperModel();
        DataTable dt = ohm.FindModelsByMontyExport(Year, Month, false);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<ul style=\"width: 1300px\" class=\"ul_table_heard2\">");
        sb.Append(@"<li style=""width: 100%; clear: left;"">
                            <ul class=""ul_row"">
                                <li style=""width: 40px;background-color:#DAB5A2;"">ID</li>
                                <li style=""width:60px;background-color:#DAB5A2;"">ORDER#</li>      
                                <li style=""width:60px;background-color:#DAB5A2;"">DATE</li>  
                                <li style=""width: 180px;text-align:center;background-color:#DAB5A2;"">PAY</li>
                                <li style=""width:180px;background-color:#DAB5A2;"">NAME</li>
                                <li style=""width:120px; text-align:center;background-color:#DAB5A2;"">SHIPPING STATE</li>
                                <li style=""width: 40px;text-align:center;background-color:#DAB5A2;"">CUT#</li>
                                <li style=""width:80px; text-align:right;background-color:#DAB5A2;"">AMNT$</li>
                                <li style=""width:80px; text-align:right;background-color:#DAB5A2;"">TAX$</li>
                                <li style=""width:60px; text-align:right;background-color:#DAB5A2;"">GST$</li>
                                <li style=""width:60px; text-align:right;background-color:#DAB5A2;"">PST$</li>
                                <li style=""width:60px; text-align:right;background-color:#DAB5A2;"">HST$</li>
                                <li style=""width:80px; text-align:right;background-color:#DAB5A2;"">TAXABLE$</li>
                                <li style=""width: 140px;text-align:center;background-color:#DAB5A2;"">Back Status</li>
                                <li style=""width:60px;text-align:center; background:DarkRed"" ><div class='tdChangeBgColor' onclick=""exportToFile('"+ Year +"','"+ Month+@"');"" style=""cursor:pointer;"">EXPORT</div></li>
                            </ul>
                        </li>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            sb.Append(string.Format(@"<li>
                            <ul >
                                <li style=""width: 40px""> 
                                
                                    
                                    
                                    <div onclick=""OpenOrderDetail('{0}')"" style=""cursor:pointer"">{1}</div> </li>
                                <li style=""width:60px"">&nbsp;{0} </li>
                                 <li style=""width:60px; text-align:center;"">{3}</li>  
                                <li style=""width:180px"">&nbsp;{2}</li>
                                <li style=""width:180px""><div style=""cursor:pointer"" onclick=""winOpen('sales_customer_history.aspx?customer_id={4}','order_history', 1000, 600, 300, 300)"">&nbsp;{5}</div></li>
                                <li style=""width:120px"">&nbsp;{6}</li>
                                <li style=""width: 40px"" >&nbsp;{4}</li>
                                <li style=""width: 80px;text-align:right; "" class=""gard"">{7}</li>
                                <li style=""width: 80px;text-align:right; "" class=""gard"">{8}</li>
                                <li style=""width:60px; text-align:right; "" class=""Wheat"">{9}</li>
                                <li style=""width:60px; text-align:right; "" class=""Wheat"">{10}</li>
                                <li style=""width:60px; text-align:right; "" class=""Wheat"">{11}</li>
                                <li style=""width:80px; text-align:right; "" class=""Gold"">{12}</li>
                                <li style=""width: 140px;text-align:center;"">{13}</li>
                                <li style=""width: 60px""><input type='CheckBox' {14} onclick=""setPartExportValue('{1}', this.checked);"" /></li>
                           </ul>                
                        </li> ", dr["order_code"].ToString()
                               , dr["order_helper_serial_no"].ToString()
                               , dr["pay_method"].ToString()
                               , dr["order_date"].ToString()
                               , dr["customer_serial_no"].ToString()
                               , dr["name"].ToString()
                               , dr["customer_shipping_state"].ToString()
                               , decimal.Parse(dr["grand_total"].ToString()).ToString("###,###.00")
                               , decimal.Parse(dr["tax_charge"].ToString()).ToString("###,###.00")
                               , dr["GST"].ToString() == "0.00" ? "" : decimal.Parse( dr["GST"].ToString()).ToString("###,###.00")
                               , dr["pst"].ToString() == "0.00" ? "" : decimal.Parse( dr["pst"].ToString()).ToString("###,###.00")
                               , dr["hst"].ToString() == "0.00" ? "" : decimal.Parse( dr["hst"].ToString()).ToString("###,###.00")
                               , dr["TAXABLE_total"].ToString() == "0.00" ? "" : decimal.Parse(dr["TAXABLE_total"].ToString()).ToString("###,###.00")
                               , dr["out_status_name"].ToString()
                               , dr["tax_export"].ToString() == "1" ? "checked='true'" : ""
                               ));
        }

        sb.Append("</ul>");
        this.literal_order_list.Text = sb.ToString();
        this.literal_run_string.Text = "<script>parent.document.getElementById('" + ParentElement + "').innerHTML = document.getElementById('page_main').innerHTML;</script>";
   
   
    }

    #region properites

    public int OrderHelperID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }

    public bool OrderHelperChecked
    {
        get { return 1 == Util.GetInt32SafeFromQueryString(Page, "OrderHelperChecked", -1); }
    }


    public string ParentElement
    {
        get { return Util.GetStringSafeFromQueryString(Page, "ParentElement"); }
    }
    public int Year
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "year", 0); }
    }
    public int Month
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "month", 0); }
    }
    #endregion
}
