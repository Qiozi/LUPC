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

public partial class Q_Admin_sale_on_sale_settings_ : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOnSaleDV();
        }
    }

    #region Properties
   
    #endregion

    #region Methods


    private void PageNote(string str)
    {
        this.lbl_note.Text = str;

    }
    private void BindOnSaleDV()
    {
        OnSaleModel os = new OnSaleModel();
        this.gv_promotion_list.DataSource = os.FindAllOnSale();
        this.gv_promotion_list.DataBind();
    }

    #endregion

    protected void gv_promotion_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Header &&
            e.Row.RowType != DataControlRowType.Footer &&
            e.Row.RowType != DataControlRowType.Pager)
        {
            TextBox begin_tb = (TextBox)e.Row.Cells[4].FindControl("_txt_begin_time");
            string begin_txt = begin_tb.Text;
            if (begin_txt.Trim() != "")
            {
                begin_tb.Text = (DateTime.Parse(begin_txt)).ToString("yyyy-MM-dd");
            }


            TextBox end_tb = (TextBox)e.Row.Cells[5].FindControl("txt_end_datetime");
            string end_txt = end_tb.Text;
            if (end_txt.Trim() != "")
            {
                end_tb.Text = (DateTime.Parse(end_txt)).ToString("yyyy-MM-dd");
            }
            if ( int.Parse(DateTime.Parse(end_txt).ToString("yyyyMMdd")) < int.Parse( DateTime.Now.ToString("yyyyMMdd")))
                e.Row.BackColor = System.Drawing.Color.FromName("#f2f2f2");


            LinkButton lb = (LinkButton)e.Row.Cells[10].Controls[0];
            lb.Attributes.Add("onclick", "return confirm('Are you sure?');");

            decimal save_price;
            decimal.TryParse(e.Row.Cells[9].Text, out save_price);
            if (save_price < 0)
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            this.PageNote("");
            string sku = this.txt_sku.Text.Trim();

            int _sku = 0;
            int.TryParse(sku, out _sku);
            if (_sku != 0)
            {
                var pm = ProductModel.GetProductModel(DBContext, _sku);
                if (pm != null)
                {

                    var os = new tb_on_sale();// OnSaleModel();
                    if (!new OnSaleModel().IsExist(DBContext, _sku))
                    {
                        os.end_datetime = DateTime.Now;
                        os.begin_datetime = DateTime.Now;
                        os.comment = "";
                        os.cost = pm.product_current_cost;
                        os.modify_datetime = DateTime.Now;
                        os.price = pm.product_current_price;
                        os.product_serial_no = _sku;
                        os.sale_price = pm.product_current_price;
                        os.save_price = 0;
                        DBContext.tb_on_sale.Add(os);
                        DBContext.SaveChanges();
                        this.BindOnSaleDV();
                        this.txt_sku.Text = "";
                    }
                    else
                        this.PageNote("Product on sale is exist");
                }
                else
                {
                    this.PageNote("Didn't find product");
                }
            }
        }
        catch (Exception ex)
        {
            this.PageNote(ex.Message);
        }
    }
    protected void gv_promotion_list_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Del":
                    int id = 0;
                    int.TryParse(this.gv_promotion_list.Rows[index].Cells[0].Text, out id);
                    var os = OnSaleModel.GetOnSaleModel(DBContext, id);
                    
                    var pm = ProductModel.GetProductModel(DBContext, os.product_serial_no.Value);
                    pm.product_current_discount = 0M;
                   
                    DBContext.tb_on_sale.Remove(os);// os.Delete();
                    DBContext.SaveChanges();
                    this.BindOnSaleDV();
                    break;
            }
        }
        catch (Exception ex)
        {
            this.PageNote(ex.Message);
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            this.PageNote("");
            string skus = "";

            for (int i = 0; i < this.gv_promotion_list.Rows.Count; i++)
            {
                GridViewRow row = this.gv_promotion_list.Rows[i];
                int _serial_no = 0;
                int.TryParse(row.Cells[0].Text, out _serial_no);
                var os = OnSaleModel.GetOnSaleModel(DBContext, _serial_no);
                var pm = ProductModel.GetProductModel(DBContext, os.product_serial_no.Value);
                DateTime begin = new DateTime();
                DateTime.TryParse(((TextBox)row.Cells[4].FindControl("_txt_begin_time")).Text, out begin);
                os.begin_datetime = begin;
                DateTime end = new DateTime();
                DateTime.TryParse(((TextBox)row.Cells[5].FindControl("txt_end_datetime")).Text, out end);
                os.end_datetime = end;
                os.cost = pm.product_current_cost;
                os.modify_datetime = DateTime.Now;
                os.price = pm.product_current_price;
                decimal sold_price = 0;
                decimal.TryParse(((TextBox)row.Cells[8].FindControl("_txt_sold_price")).Text, out sold_price);
                os.sale_price = sold_price;
                os.save_price = os.price - os.sale_price;
                DBContext.SaveChanges();

                // 如果是過期， discount 為零+
                decimal  discount = os.save_price.Value;
                if (int.Parse(os.end_datetime.Value.ToString("yyyyMMdd")) < int.Parse(DateTime.Now.ToString("yyyyMMdd")))
                    discount = 0;
                
                pm.product_current_discount = discount;
                DBContext.SaveChanges();
                if (pm.product_current_price <= os.sale_price)
                {
                    skus += pm.product_serial_no.ToString() + ",";
                }
            }
            if (skus != "")
                CH.Alert("Error: " + skus, this.Literal1);
            this.PageNote(KeyFields.SaveIsOK);
            this.BindOnSaleDV();
        }
        catch (Exception ex)
        {
            this.PageNote(ex.Message);
        }
    }
    protected void btn_Change_date_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (this.txt_begin_datetime.Text == "")
            {
                this.PageNote(" begin datetime isn't emtry");
                return;
            }

            if (this.txt_end_datetime.Text == "")
            {
                this.PageNote(" end datetime isn't emtry");
                return;
            }
            OnSaleModel.ChangeOldPrice(this.txt_begin_datetime.Text.Trim(), this.txt_end_datetime.Text.Trim());
            this.PageNote(KeyFields.SaveIsOK);
            this.BindOnSaleDV();
        }
        catch (Exception ex) { this.PageNote(ex.Message); }
    }
}
