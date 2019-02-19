using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_download_up : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            // 
            // load paymethod

            this.gv_pre_method.DataSource = Config.ExecuteDataTable(@"select 	 pay_method_short_name, 
		AD	 
	from 
	tb_pay_method_new where tag=1 order by taxis asc ");
            this.gv_pre_method.DataBind();

            //
            // load out status 
            this.gv_back_status.DataSource = Config.ExecuteDataTable(@"select 	 facture_state_name back_status_name,  AD 
	from 
	tb_facture_state where showit=1 order by priority asc 
");
            this.gv_back_status.DataBind();

            //
            // load pre status
            this.gv_pre_status.DataSource = Config.ExecuteDataTable(@"select 	 pre_status_name, 
	AD
	 
	from 
	tb_pre_status where showit =1 order by priority asc 
");
            this.gv_pre_status.DataBind();
        }
    }
    

    #region Download
    protected void btn_download_Click(object sender, EventArgs e)
    {
        if (this.txt_end_date.Text.Length > 0 && this.txt_begin_date.Text.Length > 0)
        {
            DateTime begin = DateTime.Parse(this.txt_begin_date.Text);
            DateTime end = DateTime.Parse(this.txt_end_date.Text);

            DataTable dt = Config.ExecuteDataTable(string.Format(@"
select order_invoice invoice, oh.order_code , date_format(oh.create_datetime, '%b-%d-%Y') date ,customer_shipping_first_name first_name , customer_shipping_last_name last_name, state_short_name Province,
grand_total,taxable_total,
 round(taxable_total * oh.gst_rate/100, 3) gst 
,round(taxable_total * oh.pst_rate/100, 3) pst
,round(taxable_total * oh.hst_rate/100, 3) hst
,oh.shipping_charge
,(select pay_method_short_name from tb_pay_method_new  where  pay_method_serial_no=cs.pay_method) pay_method
,(select ad from tb_pay_method_new  where  pay_method_serial_no=cs.pay_method) pay_method_AD
,(select ad from tb_facture_state where facture_state_serial_no = oh.out_status) back_status_AD
,(select ad from tb_pre_status where pre_status_serial_no = oh.pre_status_serial_no) pre_status_AD

 from tb_order_helper oh inner join tb_customer_store cs 
on oh.order_code=cs.order_code 
left join tb_state_shipping ss on ss.state_serial_no=cs.customer_shipping_state
 where date_format(oh.create_datetime, '%Y%m%d')>='{0}' and date_format(oh.create_datetime, '%Y%m%d')<='{1}' and oh.tag=1 and oh.is_ok=1 


union all 
select 'Grand Total', '', '', '', '', '',  sum(grand_total), sum(taxable_total), sum(gst), sum(pst),sum(hst), sum(shipping_charge) , '', '', '','' from (
select order_invoice invoice, oh.order_code , date_format(oh.create_datetime, '%b-%d-%Y') date ,customer_shipping_first_name first_name , customer_shipping_last_name last_name, state_name Province_State,
grand_total,taxable_total,
 round(taxable_total * oh.gst_rate/100, 3) gst 
,round(taxable_total * oh.pst_rate/100, 3) pst
,round(taxable_total * oh.hst_rate/100, 3) hst
,oh.shipping_charge
,(select pay_method_short_name from tb_pay_method_new  where  pay_method_serial_no=cs.pay_method) pay_method
,(select ad from tb_pay_method_new  where  pay_method_serial_no=cs.pay_method) pay_method_AD
,(select ad from tb_facture_state where facture_state_serial_no = oh.out_status) back_status_AD
,(select ad from tb_pre_status where pre_status_serial_no = oh.pre_status_serial_no) pre_status_AD

 from tb_order_helper oh inner join tb_customer_store cs 
on oh.order_code=cs.order_code 
left join tb_state_shipping ss on ss.state_serial_no=cs.customer_shipping_state
 where date_format(oh.create_datetime, '%Y%m%d')>='{0}' and date_format(oh.create_datetime, '%Y%m%d')<='{1}' and oh.tag=1 and oh.is_ok=1 

) d
", begin.ToString("yyyyMMdd"), end.ToString("yyyyMMdd")));

            ExcelHelper EH = new ExcelHelper(dt);
            EH.MaxRecords = 20000;
            EH.FileName = string.Format("orders_{0}.xls", begin.ToString("yyyyMMdd-") + end.ToString("yyyyMMdd"));
            EH.Export();
        }
        else
        {
            CH.Alert("Please input Date area.", this.btn_upload);
        }
    }
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
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.PostedFile != null)
        {
            try
            {
                int error_count = 0;
                int success_count = 0;
                string newFilename = Server.MapPath(string.Format("{0}{1}",
                  Config.update_product_data_excel_path,
                  string.Format("order_lists_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                this.FileUpload1.PostedFile.SaveAs(newFilename);
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
                            string order_code = dr["order_code"].ToString();
                            string paymethod_ad = dr["pay_method_AD"].ToString();
                            string out_status_ad = dr["back_status_AD"].ToString();
                            string pre_status_ad = dr["pre_status_AD"].ToString();

                            DataTable single_dt = Config.ExecuteDataTable(string.Format(@"select AD from tb_pay_method_new pm 
inner join tb_order_helper oh on oh.pay_method=pm.pay_method_serial_no where order_code='{0}'", order_code));
                            if (single_dt.Rows.Count == 1)
                            {
                                if (single_dt.Rows[0][0].ToString() != paymethod_ad)
                                {

                                }
                            }

//                            Config.ExecuteNonQuery(string.Format(@"update tb_order_helper set out_status=(select max(facture_state_serial_no) from tb_facture_state where ad='{1}')
//, pre_status_serial_no=(select max(pre_status_serial_no) from tb_pre_status where ad='{2}')
//, pay_method=(select max(pay_method_serial_no) from tb_pay_method_new where ad='{3}')
//where order_code='{0}'", order_code, out_status_ad, pre_status_ad, paymethod_ad));
//                            Config.ExecuteNonQuery(string.Format(@"update tb_customer_store set pay_method=(select max(pay_method_serial_no) from tb_pay_method_new where ad='{1}')
//where order_code='{0}'", order_code, paymethod_ad));
                            Config.ExecuteNonQuery(string.Format(@"update tb_order_helper set out_status=(select max(facture_state_serial_no) from tb_facture_state where ad='{1}')
, pre_status_serial_no=(select max(pre_status_serial_no) from tb_pre_status where ad='{2}')
,Is_Modify=1
where order_code='{0}'", order_code, out_status_ad, pre_status_ad));
//                            Config.ExecuteNonQuery(string.Format(@"update tb_customer_store set pay_method=(select max(pay_method_serial_no) from tb_pay_method_new where ad='{1}')
//where order_code='{0}'", order_code, paymethod_ad));
                            success_count += 1;
                            InsertTraceInfo(DBContext, string.Format("Change Order({0})'s back status /pre status", order_code));
                        }
                        catch { error_count += 1; }
                    }

                    CH.Alert(success_count + " Upload Success " + (error_count != 0 ? " <br> " + error_count + " Create Success" : ""), this.btn_upload);

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

    private void Amount(string order_code)
    {

    }
}
