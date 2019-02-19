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

public partial class Q_Admin_sale_stat_month_report_sub : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOrderDG(DateTime.Now.Year, DateTime.Now.Month);
            BindYearMonth();
        }
    }
    

    private void BindOrderDG(int year, int month)
    {
        OrderHelperModel ohm = new OrderHelperModel();
        this.rpt_order_list.DataSource = ohm.FindModelsByMontyExport(year, month, false);
        this.rpt_order_list.DataBind();

        GetStatValue();
    }

    private void BindYearMonth()
    {
        this.ddl_month.Items.Clear();
        this.ddl_year.Items.Clear();
        for (int i = 2006; i <= 2020; i++)
        {
            ListItem li = new ListItem(i.ToString(), i.ToString());
            this.ddl_year.Items.Add(li);
        }

        for (int i = 1; i <= 12; i++)
        {
            ListItem li = new ListItem(i.ToString("00"), i.ToString());
            this.ddl_month.Items.Add(li);
        }
        this.ddl_year.SelectedValue = DateTime.Now.Year.ToString();
        this.ddl_month.SelectedValue = DateTime.Now.Month.ToString();
    }

    protected void rpt_order_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        BindOrderDG(int.Parse(this.ddl_year.SelectedValue.ToString()), int.Parse(this.ddl_month.SelectedValue.ToString()));
        GenerateExportInfo();
    }

    private void GetStatValue()
    {
        decimal amnt_total = 0M;
        decimal tax_total = 0M;
        decimal gst_total = 0M;
        decimal pst_total = 0M;
        decimal taxable_total = 0M;
        decimal hst_total = 0M;

        for (int i = 0; i < this.rpt_order_list.Items.Count; i++)
        {
            decimal amnt = 0M;
            decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_grand_total")).Text, out amnt);
            amnt_total += amnt;

            decimal tax = 0M;
            decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_tax_charge")).Text, out tax);
            tax_total += tax;

            decimal gst = 0M;
            decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_gst")).Text, out gst);
            gst_total += gst;

            decimal pst = 0M;
            decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_pst")).Text, out pst);
            pst_total += pst;

            decimal hst = 0M;
            decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_hst")).Text, out hst);
            hst_total += hst;

            decimal taxable = 0M;
            decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_taxable_total")).Text, out taxable);
            taxable_total += taxable;
        }

        this.lbl_amnt_total.Text = amnt_total.ToString("$###,###.00");
        this.lbl_order_count.Text = this.rpt_order_list.Items.Count.ToString();
        this.lbl_tax_total.Text = tax_total.ToString("$###,###.00");
        this.lbl_gst_total.Text = gst_total.ToString("$###,###.00");
        this.lbl_hst_total.Text = hst_total.ToString("$###,###.00");
        this.lbl_pst_total.Text = pst_total.ToString("$###,###.00");
        this.lbl_taxable_total.Text = taxable_total.ToString("$###,###.00");
    }

    protected void btn_export_excel_Click(object sender, EventArgs e)
    {
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


        for (int i = 0; i < this.rpt_order_list.Items.Count; i++)
        {

            CheckBox cb = (CheckBox)this.rpt_order_list.Items[i].FindControl("_cb_export_checked");
            if (cb.Checked)
            {
                DataRow dr = dt.NewRow();

                dr["ID"] = ((Literal)this.rpt_order_list.Items[i].FindControl("_lt_id")).Text;
                dr["Order#"] = ((Literal)this.rpt_order_list.Items[i].FindControl("_lt_order_code")).Text;
                dr["AMNT$"] = decimal.Parse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_grand_total")).Text);
                dr["TAX$"] = decimal.Parse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_tax_charge")).Text);
                dr["PAY"] = ((Literal)this.rpt_order_list.Items[i].FindControl("_lt_pay_method")).Text;
                dr["NAME"] = ((Literal)this.rpt_order_list.Items[i].FindControl("_lt_name")).Text;
                dr["STATE"] = ((Literal)this.rpt_order_list.Items[i].FindControl("_lt_shipping_state")).Text;
                dr["CUT#"] = ((Literal)this.rpt_order_list.Items[i].FindControl("_lt_customer_serial_no")).Text;

                dr["GST$"] = decimal.Parse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_gst")).Text);
                dr["PST$"] = decimal.Parse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_pst")).Text);
                dr["HST$"] = decimal.Parse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_hst")).Text);
                dr["TAXABLE$"] = decimal.Parse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_taxable_total")).Text);
                dt.Rows.Add(dr);
            }

        }
        ExcelHelper eh = new ExcelHelper(dt);
        eh.MaxRecords = 100000;
        eh.FileName = DateTime.Now.ToString("yyyyMMddhhmmss.xls");
        eh.Export();
    }

    private void GenerateExportInfo()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("sum");
        dt.Columns.Add("text");
        dt.Columns.Add("AMNT-TOTAL", typeof(decimal));
        dt.Columns.Add("TAX-TOTAL", typeof(decimal));
        dt.Columns.Add("gst", typeof(decimal));
        dt.Columns.Add("pst", typeof(decimal));
        dt.Columns.Add("hst", typeof(decimal));
        dt.Columns.Add("taxable_total", typeof(decimal));
        dt.Columns.Add("priority", typeof(Int32));



        decimal amnt_total = 0M;
        decimal tax_total = 0M;
        decimal gst_total = 0M;
        decimal pst_total = 0M;
        decimal taxable_total = 0M;
        decimal hst_total = 0M;

        int count = 0;
        bool exist = false;

        for (int i = 0; i < this.rpt_order_list.Items.Count; i++)
        {

            CheckBox cb = (CheckBox)this.rpt_order_list.Items[i].FindControl("_cb_export_checked");
            if (cb.Checked)
            {
                count += 1;
                exist = false;
                string text = ((Literal)this.rpt_order_list.Items[i].FindControl("_lt_shipping_state")).Text;
                string country = ((HiddenField)this.rpt_order_list.Items[i].FindControl("_hf_country")).Value;

                decimal amnt = 0M;
                decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_grand_total")).Text, out amnt);
                decimal tax = 0M;
                decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_tax_charge")).Text, out tax);

                decimal gst = 0M;
                decimal pst = 0M;
                decimal hst = 0M;
                decimal taxable = 0M;
                decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_gst")).Text, out gst);
                decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_pst")).Text, out pst);
                decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_hst")).Text, out hst);
                decimal.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_taxable_total")).Text, out taxable);

                int priority = 0;
                int.TryParse(((HiddenField)this.rpt_order_list.Items[i].FindControl("_hf_priority")).Value, out priority);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow dr = dt.Rows[j];
                    if (dr["text"].ToString().Length < 2)
                    {
                        if (text == "")
                        {
                            exist = true;
                            dr["sum"] = int.Parse(dr["sum"].ToString()) + 1;

                            dr["AMNT-TOTAL"] = decimal.Parse(dr["AMNT-TOTAL"].ToString()) + amnt;
                            dr["TAX-TOTAL"] = decimal.Parse(dr["TAX-TOTAL"].ToString()) + tax;
                            dr["gst"] = decimal.Parse(dr["gst"].ToString()) + gst;
                            dr["pst"] = decimal.Parse(dr["pst"].ToString()) + pst;
                            dr["hst"] = decimal.Parse(dr["hst"].ToString()) + hst;
                            dr["taxable_total"] = decimal.Parse(dr["taxable_total"].ToString()) + taxable;

                            amnt_total += amnt;
                            tax_total += tax;
                            pst_total += pst;
                            gst_total += gst;
                            hst_total += hst;
                            taxable_total += taxable;
                        }
                    }
                    else
                    {
                        if (text == dr["text"].ToString())
                        {
                            exist = true;
                            dr["sum"] = int.Parse(dr["sum"].ToString()) + 1;

                            dr["AMNT-TOTAL"] = decimal.Parse(dr["AMNT-TOTAL"].ToString()) + amnt;
                            dr["TAX-TOTAL"] = decimal.Parse(dr["TAX-TOTAL"].ToString()) + tax;
                            dr["gst"] = decimal.Parse(dr["gst"].ToString()) + gst;
                            dr["pst"] = decimal.Parse(dr["pst"].ToString()) + pst;
                            dr["hst"] = decimal.Parse(dr["hst"].ToString()) + hst;
                            dr["taxable_total"] = decimal.Parse(dr["taxable_total"].ToString()) + taxable;

                            amnt_total += amnt;
                            tax_total += tax;
                            pst_total += pst;
                            gst_total += gst;
                            hst_total += hst;
                            taxable_total += taxable;
                        }
                    }
                }
                if (!exist)
                {
                    DataRow dr = dt.NewRow();
                    dr["sum"] = 1;
                    dr["text"] = text;
                    dr["TAX-TOTAL"] = tax;
                    dr["AMNT-TOTAL"] = amnt;
                    dr["gst"] = gst;
                    dr["pst"] = pst;
                    dr["hst"] = hst;
                    dr["taxable_total"] = taxable;
                    dr["priority"] = priority;
                    dt.Rows.Add(dr);

                    amnt_total += amnt;
                    tax_total += tax;
                    pst_total += pst;
                    gst_total += gst;
                    hst_total += hst;
                    taxable_total += taxable;
                }
            }
        }
        DataRow totalDR = dt.NewRow();
        totalDR["sum"] = count;
        totalDR["text"] = "TOTAL";
        totalDR["TAX-TOTAL"] = tax_total;
        totalDR["AMNT-TOTAL"] = amnt_total;
        totalDR["gst"] = gst_total;
        totalDR["pst"] = pst_total;
        totalDR["hst"] = hst_total;
        totalDR["taxable_total"] = taxable_total;
        totalDR["priority"] = 10000;
        dt.Rows.Add(totalDR);

        dt.DefaultView.Sort = " priority asc ";
        this.gridView_export_info.DataSource = dt;
        this.gridView_export_info.DataBind();

        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            DataRow dr = dt.Rows[i];
        //            sb.Append(string.Format(@"<tr>
        //                            <td style=""background:#ffffff; font-size:8pt"">{0}</td>
        //                            <td style=""background:#ffffff; font-size:8pt; text-align:right"">${1}</td>
        //                            <td style=""background:#ffffff; font-size:8pt; text-align:right"">${2}</td>
        //                            <td style=""background:#ffffff; font-size:8pt; text-align:center"">{3}</td>
        //                        </tr>", dr["text"].ToString(), dr["TAX-TOTAL"].ToString(), dr["AMNT-TOTAL"].ToString(), dr["sum"].ToString()));
        //        }
        //        this.literal_export_info.Text = string.Format(@"<table cellpadding=""2"" cellspacing=""1"" style=""width:100%""  style=""border:1px solid #D1DAF6;"">
        //                        <tr>
        //                            <td colspan=""4"" style=""background:#ffffff"">
        //                                <table width=""200"">
        //                                    <tr>
        //                                        <td>
        //                                        Export Count:
        //                                        </td>
        //                                        <td style=""text-align:right"">
        //                                        {0}
        //                                        </td>
        //                                    </tr>
        //                                    <tr>
        //                                        <td>
        //                                        AMNT TOTAL:
        //                                        </td>
        //                                        <td style=""text-align:right"">
        //                                        ${1}
        //                                        </td>
        //                                    </tr>
        //                                    <tr>
        //                                        <td>
        //                                        TAX TOTAL:
        //                                        </td>
        //                                        <td style=""text-align:right"">
        //                                        ${2}
        //                                        </td>
        //                                    </tr>
        //                                </table>   
        //                            </td>
        //                        </tr>
        //                        <tr>
        //                            <td style=""background:#D1DAF6; text-align:center"">State</td>
        //                            <td style=""background:#D1DAF6; text-align:center"">Tax Total</td>
        //                            <td style=""background:#D1DAF6; text-align:center"">AMNT Total</td>
        //                            <td style=""background:#D1DAF6; text-align:center"">Count</td>
        //                        </tr>
        //                        {3}                        
        //                    </table>", count, amnt_total, tax_total, sb.ToString());


        this.literal_run_script.Text = "<script> parent.document.getElementById('div_ExportInfo').innerHTML = document.getElementById('export_info').innerHTML;</script>";
    }
    protected void rpt_order_list_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.rpt_order_list.Items.Count; i++)
        {
            CheckBox e_cb = (CheckBox)this.rpt_order_list.Items[i].FindControl("_cb_export_checked");
            if (e_cb == cb)
            {
                int id = 0;
                int.TryParse(((Literal)this.rpt_order_list.Items[i].FindControl("_lt_id")).Text, out id);
                OrderHelperModel ohm = new OrderHelperModel();
                ohm.ChangeExportValue(id, e_cb.Checked);

                GenerateExportInfo();
            }

        }
    }
    protected void gridView_export_info_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Footer
            && e.Row.RowType != DataControlRowType.Header
            && e.Row.RowType != DataControlRowType.Pager)
        {
            if (e.Row.Cells[0].Text == "TOTAL")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("SlateGray");
                e.Row.ForeColor = System.Drawing.Color.FromName("white");

            }
        }
    }

}
