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

public partial class Q_Admin_product_part_rebate : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
            // close rebate ; showit=0
            //
            if (CMD.ToLower() == "close")
            {
                Config.ExecuteNonQuery("Update tb_sale_promotion set show_it=0 where sale_promotion_serial_no=" + ReqID);
                Response.Write("<script> alert('the part is close.');window.opener.location.reload();window.close();</script>");
                Response.End();
            }

            if (keyword.Trim().Length > 0)
            {
                DataTable dt = Config.ExecuteDataTable(string.Format("select product_serial_no,product_name,manufacturer_part_number from tb_product where product_serial_no='{0}' or manufacturer_part_number='{0}' ", keyword));
                if (dt.Rows.Count > 0)
                {
                    //
                    // save sku on page.
                    //
                    SKU = int.Parse(dt.Rows[0]["product_serial_no"].ToString());

                    this.lbl_mfp.Text = dt.Rows[0]["manufacturer_part_number"].ToString();
                    this.lbl_sku.Text = dt.Rows[0]["product_serial_no"].ToString();

                    this.lbl_part_name.Text = dt.Rows[0]["product_name"].ToString();
                    BindRebateList(SKU);
                }
                else
                {
                    Response.Write("<b>No Match Data.</b>");
                    Response.End();
                }
            }

            DateTime monthFirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            this.txt_begin_date.Text = monthFirstDay.ToString("yyyy-MM-dd");

            DateTime monthLastDay = monthFirstDay.AddMonths(1).AddDays(-1);
            this.txt_end_date.Text = monthLastDay.ToString("yyyy-MM-dd");
        }
    }
    

    private void BindRebateList(int sku)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select date_format(begin_datetime, '%b/%d/%Y') begin_date
,date_format(end_datetime, '%b/%d/%Y') end_date
,end_datetime >=current_date v
,save_cost
,product_serial_no
,sale_promotion_serial_no
,show_it
,pdf_filename from tb_sale_promotion where promotion_or_rebate=2 and product_serial_no='{0}' order by sale_promotion_serial_no desc", sku));

        this.rpt_rebate_list.DataSource = dt;
        this.rpt_rebate_list.DataBind();
    }


    #region preporties

    public int SKU
    {
        get
        {
            object o = ViewState["SKU"];
            if (o != null)
                return int.Parse(o.ToString());
            else
                return -1;
        }
        set {ViewState["SKU"] = value; }
    }

    public string FileName
    {
        get { return ViewState["filename"].ToString(); }
        set { ViewState["filename"] = value; }
    }

    public string CMD
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    public int ReqID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }
    public string keyword
    {
        get { return Util.GetStringSafeFromQueryString(Page, "keyword"); }
    }
    #endregion

    #region Calendar
    /// <summary>
    /// 日期控件 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Calendar c = (Calendar)sender;
        this.txt_begin_date.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    }
    protected void Calendar_SelectionChanged(object sender, EventArgs e)
    {
        Calendar c = (Calendar)sender;
        this.txt_end_date.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    }
    protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsToday)
        {
            e.Cell.ForeColor = System.Drawing.Color.Blue;
            e.Cell.BackColor = System.Drawing.Color.Pink;
        }
    }
    #endregion


    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (this.txt_begin_date.Text == "")
        {
            CH.Alert("please select Begin Date", this.Literal1);
            return;
        }
        if (this.txt_end_date.Text == "")
        {
            CH.Alert("Please select End Date", this.Literal1);
            return;
        }
        if (this.txt_save_price.Text == "")
        {
            CH.Alert("Please input Save Price", this.Literal1);
            return;
        }

        if (this.txt_file_name.Text == "")
        {
            CH.Alert("Please input File Name", this.Literal1);
            return;
        }
        DateTime begin_date;
        DateTime.TryParse(this.txt_begin_date.Text, out begin_date);

        DateTime end_date;
        DateTime.TryParse(this.txt_end_date.Text, out end_date);


        decimal save_price;
        decimal.TryParse(this.txt_save_price.Text, out save_price);
        if (save_price > 0)
        {
            var spm = new tb_sale_promotion();// SalePromotionModel();
            spm.begin_datetime = begin_date;
            spm.end_datetime = end_date;
            spm.create_datetime = DateTime.Now;
            spm.cost = 0M;
            spm.pdf_filename = this.txt_file_name.Text.Trim();
            spm.price = 0M;
            spm.product_serial_no = SKU;
            spm.promotion_or_rebate = 2;
            spm.save_cost = save_price;
            spm.show_it = true;
            spm.comment = this.txt_comment.Text;
            DBContext.tb_sale_promotion.Add(spm);
            DBContext.SaveChanges();

            InsertTraceInfo(DBContext, "add rebate sku:" + SKU.ToString());
            CH.Alert(KeyFields.SaveIsOK, this.Literal1);

            //this.txt_end_date.Text = "";
            //this.txt_begin_date.Text = "";
            //this.txt_file_name.Text = "";
            this.txt_save_price.Text = "";
            this.btn_save.Text = "Save";
            FileName = "";

            BindRebateList(SKU);
        }
    }
    protected void Button_upload_Click(object sender, EventArgs e)
    {
        FileName = "";
        if (this.FileUpload1.PostedFile != null)
        {
            string newFilename = Server.MapPath(string.Format("{0}/{1}",
                Config.StoreProductRebatePdfPath,
                this.FileUpload1.PostedFile.FileName));
            if (System.IO.File.Exists(newFilename))
            {
                CH.Alert("<span style='color:red;'>The file is exist</span>", this.Literal1);
                return;
            }
            else
            {
               
                this.FileUpload1.PostedFile.SaveAs(newFilename);
                FileName = this.FileUpload1.PostedFile.FileName;
                CH.Alert(FileName + " file is upload", this.Literal1);
                this.txt_file_name.Text = FileName;
            }
        }
    }
}
